using LiveSplit.Model;
using LiveSplit.UI.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace LiveSplit.UI.Components
{
    public class SummaryTrackerComponent : IComponent
    {
        public static SummaryTrackerComponent instance { get; set; }
        protected InfoTextComponent InternalComponent { get; set; }

        public SummaryTrackerSettings Settings { get; set; }

        protected LiveSplitState CurrentState { get; set; }

        private MemoryReader MemoryReader { get; set; }

        private Color statusColor = Color.White;

        private bool closed;

        private bool runComplete;

        private bool missedQuest;

        private bool connectedToServer;

        private RunState runState = RunState.GAMENOTSTARTED;

        private RunDetails runDetails;

        public string ComponentName => "The Hobbit - Summary Tracker";

        public float HorizontalWidth => InternalComponent.HorizontalWidth;
        public float MinimumWidth => InternalComponent.MinimumWidth;
        public float VerticalHeight => InternalComponent.VerticalHeight;
        public float MinimumHeight => InternalComponent.MinimumHeight;

        public float PaddingTop => InternalComponent.PaddingTop;
        public float PaddingLeft => InternalComponent.PaddingLeft;
        public float PaddingBottom => InternalComponent.PaddingBottom;
        public float PaddingRight => InternalComponent.PaddingRight;

        public IDictionary<string, Action> ContextMenuControls => null;

        public SummaryTrackerComponent(LiveSplitState state)
        {
            instance = this;
            Settings = new SummaryTrackerSettings();
            InternalComponent = new InfoTextComponent("", null);

            MemoryReader = new MemoryReader();
            runDetails = new RunDetails("");

            CurrentState = state;

            state.OnStart += state_OnStart;
            state.OnReset += state_OnReset;
            state.OnSplit += state_OnSplit;
            

            Task.Factory.StartNew(() => ConnectToServer());
        }

        private async void ConnectToServer()
        {
            connectedToServer = await Task.Factory.StartNew(() => WebClient.CheckServerAlive()).Result;
            if (!connectedToServer) MessageBox.Show("Can't connect to server, continuing in offline mode.");

        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            InternalComponent.NameLabel.HasShadow
                = InternalComponent.ValueLabel.HasShadow
                = state.LayoutSettings.DropShadows;

            InternalComponent.NameLabel.ForeColor = state.LayoutSettings.TextColor;
            InternalComponent.ValueLabel.ForeColor = statusColor;

            InternalComponent.DrawHorizontal(g, state, height, clipRegion);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            InternalComponent.DisplayTwoRows = !Settings.LiteMode;

            InternalComponent.NameLabel.HasShadow
                = InternalComponent.ValueLabel.HasShadow
                = state.LayoutSettings.DropShadows;

            InternalComponent.NameLabel.HorizontalAlignment = Settings.LiteMode ? StringAlignment.Near : StringAlignment.Center;
            InternalComponent.ValueLabel.HorizontalAlignment = Settings.LiteMode ? StringAlignment.Far : StringAlignment.Center;
            InternalComponent.NameLabel.VerticalAlignment = StringAlignment.Center;
            InternalComponent.ValueLabel.VerticalAlignment = StringAlignment.Center;

            InternalComponent.NameLabel.ForeColor = state.LayoutSettings.TextColor;
            InternalComponent.ValueLabel.ForeColor = statusColor;

            InternalComponent.NameLabel.Text = Settings.LiteMode ? "Summary Tracker" : "The Hobbit - Summary Tracker";
            InternalComponent.NameLabel.Text = connectedToServer ? InternalComponent.NameLabel.Text : $"OM {InternalComponent.NameLabel.Text}";

            runDetails.player = Settings.Username;

            InternalComponent.DrawVertical(g, state, width, clipRegion);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public void SetSettings(System.Xml.XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            InternalComponent.InformationValue = SetInformationText();
            InternalComponent.Update(invalidator, state, width, height, mode);

            Process[] processes = Process.GetProcessesByName("meridian");
            if (closed && processes.Length > 0)
            {
                runComplete = false;
                closed = false;
                if (runState != RunState.CRASHED)
                {
                    statusColor = Color.White;
                    SetRunState(RunState.WAITING);
                }
            }
            else
            {
                if (runState == RunState.GAMENOTSTARTED) runState = RunState.WAITING;
                else if (runState == RunState.CRASHED)
                {
                    byte[] levelMem = MemoryReader.ReadMemory("meridian", MemoryReader.ConstructPointer(Constants.CurrentLevelAddress), true);
                    if (levelMem != null)
                    {
                        int level = MemReaderUtil.ConvertMemory(levelMem, MemType.INT);
                        if (level > -1)
                        {
                            statusColor = Color.Gold;
                            SetRunState(RunState.RUNNING);
                        }
                    }
                    SetRunState(RunState.RUNNING);
                }

                if (processes.Length == 0)
                {
                    closed = true;
                    if (state.CurrentPhase == TimerPhase.Running)
                    {
                        statusColor = Color.OrangeRed;
                        SetRunState(RunState.CRASHED);
                    }
                    else
                    {
                        statusColor = Color.White;
                        SetRunState(RunState.GAMENOTSTARTED);
                    }
                }

                if (runComplete)
                {
                    EndOfRunQuestCheck();
                }
            }
        }

        private void EndOfRunQuestCheck()
        {
            byte[] stateMem = MemoryReader.ReadMemory("meridian", MemoryReader.ConstructPointer(Constants.OolStateAddress), true);
            if (stateMem != null)
            {
                int oolState = MemReaderUtil.ConvertMemory(stateMem, MemType.INT);
                if (oolState == 12)
                {
                    runComplete = false;
                    if (!missedQuest)
                    {
                        byte[] questMem = MemoryReader.ReadMemory("meridian", MemoryReader.ConstructPointer(Constants.MissedQuestsAddress), true);
                        if (questMem != null)
                        {
                            int missedQuestsCount = MemReaderUtil.ConvertMemory(questMem, MemType.FLOAT);
                            missedQuest = missedQuestsCount > 0 ? true : false;
                            if (runDetails.missedLevel == Level.None && missedQuest) runDetails.missedLevel = Level.CloudsBurst;
                        }
                    }

                    if (!WebClient.SendRunToServer(runDetails))
                    {
                        runState = RunState.CANTVERIFY;
                        statusColor = Color.Red;
                    }
                    else
                    {
                        runState = RunState.COUNTED;
                        if (missedQuest) statusColor = Color.Red;
                        else statusColor = Color.LimeGreen;
                    }
                }
            }
        }

        private void EndOfLevelQuestCheck()
        {
            if (runDetails.missedLevel == Level.None)
            {
                byte[] questMem = MemoryReader.ReadMemory("meridian", MemoryReader.ConstructPointer(Constants.MissedQuestsAddress), true);
                if (questMem != null)
                {
                    int missedQuestsCount = MemReaderUtil.ConvertMemory(questMem, MemType.FLOAT);
                    missedQuest = missedQuestsCount > 0 ? true : false;
                    if (missedQuest)
                    {
                        byte[] levelMem = MemoryReader.ReadMemory("meridian", MemoryReader.ConstructPointer(Constants.CurrentLevelAddress), true);
                        int levelID = MemReaderUtil.ConvertMemory(levelMem, MemType.INT);
                        runDetails.missedLevel = (Level)(levelID - 1);
                    }
                }
            }
        }

        private void state_OnStart(object sender, EventArgs e)
        {
            if (CurrentState.Run.CategoryName != "All Quests") return;
            statusColor = Color.Gold;
            SetRunState(RunState.RUNNING);
        }

        private void state_OnReset(object sender, TimerPhase e)
        {
            missedQuest = false;
            statusColor = Color.White;
            SetRunState(RunState.WAITING);
            runDetails.Clear();
        }

        private void state_OnSplit(Object sender, EventArgs e)
        {
            if (runState != RunState.RUNNING) return;
            if (CurrentState.CurrentPhase == TimerPhase.Ended && CurrentState.CurrentSplitIndex >= CurrentState.Run.Count && !runComplete)
            {
                runDetails.completionDate = DateTime.Now;
                runDetails.runTime = CurrentState.CurrentTime;
                statusColor = Color.CadetBlue;
                SetRunState(RunState.FINISHED);
                runComplete = true;
            }
            else EndOfLevelQuestCheck();
        }

        private void SetRunState(RunState state)
        {
            if (runState == RunState.GAMENOTSTARTED && state != RunState.STARTED) return;
            runState = state;
        }

        private string SetInformationText()
        {
            switch (runState)
            {
                case RunState.GAMENOTSTARTED:
                    return "-";
                case RunState.WAITING:
                case RunState.STARTED:
                    if (Settings.LiteMode) return "Waiting...";
                    else return "Waiting for run to start...";
                case RunState.CRASHED:
                    if (Settings.LiteMode) return "In progress...";
                    else return "Game crash detected, run still in progress...";
                case RunState.RUNNING:
                    if (Settings.LiteMode) return "In progress...";
                    else return "Run currently in progress...";
                case RunState.FINISHED:
                    if (Settings.LiteMode) return "Complete! Counting Quests...";
                    else return "Run Complete! Skip end cinema for final count.";
                case RunState.CANTVERIFY:
                    if (Settings.LiteMode) return "Can't verify run!";
                    else return "Can't verify run! Server unresponsive.";
                case RunState.COUNTED:
                    if (missedQuest)
                    {
                        if (Settings.LiteMode) return $"Missed quest in {runDetails.missedLevel.ToString()}";
                        else return $"Run Invalid, missed quest in {runDetails.missedLevel.ToString()}";
                    }
                    else
                    {
                        if (Settings.LiteMode) return "All Quests Finished!";
                        else return "Congratulations, All Quests have been successfully finished!";
                    }
                default:
                    return "-";
            }
        }

        public void Dispose()
        {
            CurrentState.OnStart -= state_OnStart;
            CurrentState.OnReset -= state_OnReset;
            CurrentState.OnSplit -= state_OnSplit;
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}