using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using LiveSplit.UI.Util;

namespace LiveSplit.UI.Components
{
    public class QuestTrackerComponent : IComponent
    {
        protected InfoTextComponent InternalComponent { get; set; }
        // This is how we will access all the settings that the user has set.
        public QuestTrackerSettings Settings { get; set; }
        // This object contains all of the current information about the splits, the timer, etc.
        protected LiveSplitState CurrentState { get; set; }

        private MemoryReader MemoryReader { get; set; }

        private string status = "Waiting for game to start...";

        private Color statusColor = Color.White;

        private string missedQuestsAddress = "0x35C094";

        private string oolStateAddress = "0x362B58";

        private bool closed;

        private bool runComplete;

        public string ComponentName => "The Hobbit - All Quests Tracker";

        public float HorizontalWidth => InternalComponent.HorizontalWidth;
        public float MinimumWidth => InternalComponent.MinimumWidth;
        public float VerticalHeight => InternalComponent.VerticalHeight;
        public float MinimumHeight => InternalComponent.MinimumHeight;

        public float PaddingTop => InternalComponent.PaddingTop;
        public float PaddingLeft => InternalComponent.PaddingLeft;
        public float PaddingBottom => InternalComponent.PaddingBottom;
        public float PaddingRight => InternalComponent.PaddingRight;

        // I'm going to be honest, I don't know what this is for, but I know we don't need it.
        public IDictionary<string, Action> ContextMenuControls => null;

        // This function is called when LiveSplit creates your component. This happens when the
        // component is added to the layout, or when LiveSplit opens a layout with this component
        // already added.
        public QuestTrackerComponent(LiveSplitState state)
        {
            InternalComponent = new InfoTextComponent("The Hobbit - All Quests Tracker", null);
            Settings = new QuestTrackerSettings();

            MemoryReader = new MemoryReader();

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

        // We will be adding the ability to display the component across two rows in our settings menu.
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

        // This is the function where we decide what needs to be displayed at this moment in time,
        // and tell the internal component to display it. This function is called hundreds to
        // thousands of times per second.
        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Process[] processes = Process.GetProcessesByName("meridian");
            if (closed && processes.Length > 0)
            {
                closed = false;
                statusColor = Color.White;
                status = "Waiting for run to start...";
            }
            else
            {
                if (processes.Length == 0)
                {
                    closed = true;
                    runComplete = false;
                    statusColor = Color.White;
                    status = "-";
                }

                if (runComplete)
                {
                    byte[] stateMem = MemoryReader.ReadMemory("meridian", MemoryReader.ConstructPointer(oolStateAddress), true);
                    if (stateMem != null)
                    {
                        int oolState = MemReaderUtil.ConvertMemory(stateMem, MemType.INT);
                        if (oolState == 12)
                        {
                            byte[] questMem = MemoryReader.ReadMemory("meridian", MemoryReader.ConstructPointer(missedQuestsAddress), true);
                            if (questMem != null)
                            {
                                int missedQuestsCount = MemReaderUtil.ConvertMemory(questMem, MemType.FLOAT);
                                bool missedQuests = missedQuestsCount > 0 ? true : false;

                                if (missedQuests) { status = $"Run Invalid, missed {missedQuestsCount} quests!"; statusColor = Color.Red; }
                                else { status = "Run Valid, All Quests Acquired!"; statusColor = Color.LimeGreen; }
                            }
                        }
                    }
                }

                InternalComponent.InformationValue = status;
                InternalComponent.Update(invalidator, state, width, height, mode);
            }
        }

        private void state_OnStart(object sender, EventArgs e)
        {
            statusColor = Color.Gold;
            status = "Run currently in progress...";
        }

        private void state_OnReset(object sender, TimerPhase e)
        {
            runComplete = false;
            statusColor = Color.White;
            status = "Waiting for run to start...";
        }

        private void state_OnSplit(Object sender, EventArgs e)
        {
            if(CurrentState.CurrentPhase == TimerPhase.Ended && CurrentState.CurrentSplitIndex == CurrentState.Run.Count)
            {
                runComplete = true;
                statusColor = Color.CadetBlue;
                status = "Run Complete! Skip end cinema for final count.";
            }
        }

        // This function is called when the component is removed from the layout, or when LiveSplit
        // closes a layout with this component in it.
        public void Dispose()
        {
            CurrentState.OnStart -= state_OnStart;
            CurrentState.OnReset -= state_OnReset;
            CurrentState.OnSplit -= state_OnSplit;
        }

        // I do not know what this is for.
        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}