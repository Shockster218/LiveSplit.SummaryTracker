using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LiveSplit.UI.Components
{
    public class SummaryTrackerComponent : IComponent
    {
        public static SummaryTrackerComponent instance { get; set; }

        protected InfoTextComponent InternalComponent { get; set; }

        public SummaryTrackerSettings Settings { get; set; }

        protected LiveSplitState CurrentState { get; set; }

        private MemoryReader memReader { get; set; }

        public SimpleLabel InformationText { get; set; }

        private Color statusColor = Color.White;

        private bool setPlaying;

        private bool summaryFail;

        private bool categoryMismatch;

        private bool hundredPercent;

        private bool updateInformationText = true;

        private int levelID = -1;

        private RunState runState = RunState.GAMENOTSTARTED;

        private string runStatusText = String.Empty;

        private Process game = null;

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
        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();

        public SummaryTrackerComponent(LiveSplitState state)
        {
            instance = this;
            Settings = new SummaryTrackerSettings();
            InternalComponent = new InfoTextComponent("", null);

            CurrentState = state;

            state.OnStart += state_OnStart;
            state.OnReset += state_OnReset;
            state.OnSplit += state_OnSplit;
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
            InternalComponent.DisplayTwoRows = true;

            InternalComponent.NameLabel.HasShadow
                = InternalComponent.ValueLabel.HasShadow
                = state.LayoutSettings.DropShadows;

            InternalComponent.NameLabel.HorizontalAlignment = StringAlignment.Center;
            InternalComponent.ValueLabel.HorizontalAlignment = StringAlignment.Center;
            InternalComponent.NameLabel.VerticalAlignment = StringAlignment.Center;
            InternalComponent.ValueLabel.VerticalAlignment = StringAlignment.Center;

            InternalComponent.NameLabel.ForeColor = state.LayoutSettings.TextColor;
            InternalComponent.ValueLabel.ForeColor = statusColor;

            InternalComponent.NameLabel.Text = "The Hobbit - Summary Tracker";

            Font labelFont = InternalComponent.ValueLabel.Font;
            Font font = new Font(labelFont.FontFamily, 15f, labelFont.Style, labelFont.Unit);

            InternalComponent.ValueLabel.Font = font;

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
            if (updateInformationText) 
            {
                InternalComponent.InformationValue = GetInformationText();
                InternalComponent.Update(invalidator, state, width, height, mode);
                updateInformationText = false;

                if (setPlaying)
                {
                    Task.Run(async () => 
                    { 
                        await Task.Delay(2000);
                        SetRunState(RunState.PLAYING);
                    });
                }
            }

            if(game == null)
            {
                if (runState != RunState.GAMENOTSTARTED) SetRunState(RunState.GAMENOTSTARTED);
                Process[] processes = Process.GetProcessesByName("meridian");
                game = processes.Length > 0 ? processes[0] : null;
                return;
            }

            memReader = new MemoryReader("meridian");

            if (CurrentState.CurrentPhase == TimerPhase.Running && memReader.ReadAddressInt(MemoryAddress.LevelQueued) != -1 && GetLevelIndex() < levelID)
            {
                Settings.SetDetails(String.Empty);
                SetRunState(RunState.LOADED);
                levelID = GetLevelIndex();
            }

            if (runState == RunState.FAILED) return;

            if (runState == RunState.MISMATCH) return;

            if (runState == RunState.GAMENOTSTARTED) SetRunState(RunState.WAITING);
            
            if (runState == RunState.CLOSED && GetLevelIndex() == levelID)
            {
                SetRunState(RunState.LOADED);
            }
            
            if (!game.Responding || game.HasExited || game == null)
            {
                game = null;
                if (state.CurrentPhase == TimerPhase.Running)
                {
                    SetRunState(RunState.CLOSED);
                }
                else
                {
                    SetRunState(RunState.GAMENOTSTARTED);
                }
            }
        }

        private int GetLevelIndex()
        {
            return memReader.ReadAddressInt(MemoryAddress.CurrentLevelAddress);
        }

        private String GetLevelName(bool previous)
        {
            int levelIndex = GetLevelIndex();
            levelIndex = previous ? levelIndex - 1 : levelIndex;

            switch (levelIndex)
            {
                case 0:
                    return "Dream World";
                case 1:
                    return "An Unexpected Party";
                case 2:
                    return "Roast Mutton";
                case 3:
                    return "Troll-Hole";
                case 4:
                    return "Overhill and Underhill";
                case 5:
                    return "Riddles";
                case 6:
                    return "Flies and Spiders";
                case 7:
                    return "Barrels out of Bond";
                case 8:
                    return "A Warm Welcome";
                case 9:
                    return "Inside Information";
                case 10:
                    return "Gathering of the Clouds";
                default:
                    return GetLevelIndex().ToString();
            }
        }

        private void EndOfLevelCheck()
        {
            float missedQuestsCount = 0;
            float missedSPCount = 0;
            float missedCourageCount = 0;
            float missedChestCount = 0;

            missedQuestsCount += memReader.ReadAddressFloat(MemoryAddress.MissedQuestsAddress);

            if (hundredPercent)
            {
                missedSPCount += memReader.ReadAddressFloat(MemoryAddress.MissedSilverPenniesAddress);
                missedCourageCount += memReader.ReadAddressFloat(MemoryAddress.MissedCourageAddress);
                missedChestCount += memReader.ReadAddressFloat(MemoryAddress.MissedChestAddress);

                // Dream world quests check for hundo
                if (GetLevelIndex() == 1) 
                {
                    if (memReader.ReadAddressInt(MemoryAddress.QuestOneAddress) == 0) missedQuestsCount++;
                    if (memReader.ReadAddressInt(MemoryAddress.QuestThreeAddress) == 0) missedQuestsCount++;
                    if (memReader.ReadAddressInt(MemoryAddress.QuestFiveAddress) == 0) missedQuestsCount++;
                    if (memReader.ReadAddressInt(MemoryAddress.QuestSevenAddress) == 0) missedQuestsCount++;
                }
            }

            summaryFail = missedQuestsCount > 0 || missedSPCount > 0 || missedCourageCount > 0 || missedChestCount > 0 ? true : false;

            if (summaryFail)
            {
                string details = $"Time: {DateTime.Now.ToString("h:mm:ss tt")}\n" +
                                 $"Level: {GetLevelName(previous:true)}\n" +
                                 $"Quests Missed: {missedQuestsCount}\n";

                if (hundredPercent)
                {
                    details += $"Silver Pennies Missed: {missedSPCount}\n" +
                               $"Courage Points Missed: {missedCourageCount}\n" +
                               $"Chests Missed: {missedChestCount}"; 
                }

                Settings.SetDetails(details);
                SetRunState(RunState.FAILED);
            }
            else
            {
                if (CurrentState.CurrentPhase == TimerPhase.Ended && CurrentState.CurrentSplitIndex >= CurrentState.Run.Count)
                {
                    SetRunState(RunState.FINISHED);
                }
                else
                {
                    SetRunState(RunState.SPLIT);
                }
            }

            levelID = GetLevelIndex();
        }

        private void state_OnStart(object sender, EventArgs e)
        {
            if(CurrentState.Run.CategoryName == Settings.Category)
            {
                if (Settings.Category == "100%") hundredPercent = true;
                else hundredPercent = false;
                SetRunState(RunState.STARTED);
            }
            else SetRunState(RunState.MISMATCH);
        }

        private void state_OnReset(object sender, TimerPhase e)
        {
            summaryFail = false;
            categoryMismatch = false;
            levelID = -1;
            SetRunState(RunState.WAITING, forceReset:true);
        }

        private void state_OnSplit(Object sender, EventArgs e)
        {
            if (runState != RunState.PLAYING || runState == RunState.FAILED) return;
            EndOfLevelCheck();
        }

        private void SetRunState(RunState _state, bool forceReset = false)
        {
            if (runState == RunState.CLOSED && _state != RunState.LOADED && !forceReset) return;
            runState = _state;
            updateInformationText = true;
        }

        private string GetInformationText()
        {
            switch (runState)
            {
                case RunState.GAMENOTSTARTED:
                    statusColor = Color.BurlyWood;
                    return "Waiting for game...";
                case RunState.WAITING:
                    statusColor = Color.Coral;
                    return "Press \"New Game\" to start";
                case RunState.MISMATCH:
                    statusColor = Color.Red;
                    categoryMismatch = true;
                    return "Failed to start, category mismatch";
                case RunState.STARTED:
                    statusColor = Color.Green;
                    setPlaying = true;
                    return "Tracker now active. Goodluck!";
                case RunState.PLAYING:
                    statusColor = Color.Gold;
                    setPlaying = false;
                    runStatusText = $"Level in progress... [{GetLevelIndex() + 1}/12]";
                    return runStatusText;
                case RunState.CLOSED:
                    statusColor = Color.OrangeRed;
                    return "Game closed. Load save to continue.";
                case RunState.LOADED:
                    statusColor = Color.Green;
                    setPlaying = true;
                    summaryFail = false;
                    return $"Loaded back to {GetLevelName(previous:false)}";
                case RunState.FINISHED:
                    statusColor = Color.Cyan;
                    return "Run completed! GGs";
                case RunState.SPLIT:
                    statusColor = Color.Green;
                    setPlaying = true;
                    return $"{GetLevelName(previous:true)} done.";
                case RunState.FAILED:
                    statusColor = Color.Crimson;
                    return $"Summary failed. See log for info.";
                case RunState.CANTREAD:
                    statusColor = Color.OrangeRed;
                    return "Can't read game memory. Restart Game and Livesplit";
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
    }
}