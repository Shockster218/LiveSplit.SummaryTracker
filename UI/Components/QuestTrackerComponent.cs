using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class QuestTrackerComponent : IComponent
    {
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize);
        // This internal component does the actual heavy lifting. Whenever we want to do something
        // like display text, we will call the appropriate function on the internal component.
        protected InfoTextComponent InternalComponent { get; set; }
        // This is how we will access all the settings that the user has set.
        public QuestTrackerSettings Settings { get; set; }
        // This object contains all of the current information about the splits, the timer, etc.
        protected LiveSplitState CurrentState { get; set; }

        private string status = "Waiting...";
        private Color statusColor = Color.White;

        public string ComponentName => "All Quests Tracker";

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
            InternalComponent = new InfoTextComponent("All Quests Tracker", null);
            Settings = new QuestTrackerSettings();

            state.OnStart += state_OnStart;
            state.OnReset += state_OnReset;
            state.OnSplit += state_OnSplit;
            CurrentState = state;
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
            InternalComponent.DisplayTwoRows = Settings.Display2Rows;

            InternalComponent.NameLabel.HasShadow
                = InternalComponent.ValueLabel.HasShadow
                = state.LayoutSettings.DropShadows;

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
            InternalComponent.InformationValue = status;
            InternalComponent.Update(invalidator, state, width, height, mode);
        }

        void state_OnStart(object sender, EventArgs e)
        {
            statusColor = Color.Turquoise;
            status = "In progress...";
        }

        void state_OnReset(object sender, TimerPhase e)
        {
            statusColor = Color.White;
            status = "Waiting...";
        }

        void state_OnSplit(object sender, EventArgs e)
        {
            if(CurrentState.CurrentPhase == TimerPhase.Ended)
            {
                Task.Factory.StartNew(() => EndCheck());
            }
        }

        private async void EndCheck()
        {
            Process process = Process.GetProcessesByName("meridian")[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            bool run = true;

            int questAddress = 0x0075C094;
            int stateAddress = 0x00762B58;

            while (run)
            {
                byte[] stateBuffer = new byte[4];

                ReadProcessMemory((int)processHandle, stateAddress, stateBuffer, stateBuffer.Length);

                int state = BitConverter.ToInt32(stateBuffer, 0);

                if (state == 12)
                {
                    await Task.Delay(1500);
                    byte[] questBuffer = new byte[4];
                    ReadProcessMemory((int)processHandle, questAddress, questBuffer, questBuffer.Length);
                    float missedQuestsCount = BitConverter.ToSingle(questBuffer, 0);
                    bool missedQuests = missedQuestsCount > 0 ? true : false;

                    if (missedQuests) { status = $"Run Invalid, missed {missedQuestsCount} quests!"; statusColor = Color.Red; }
                    else { status = "All Quests Acquired!"; statusColor = Color.Green; }
                    run = false;
                }

                await Task.Delay(3000);
            }
        }

        // This function is called when the component is removed from the layout, or when LiveSplit
        // closes a layout with this component in it.
        public void Dispose()
        {
            CurrentState.OnStart -= state_OnStart;
            CurrentState.OnReset -= state_OnReset;
        }

        // I do not know what this is for.
        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}