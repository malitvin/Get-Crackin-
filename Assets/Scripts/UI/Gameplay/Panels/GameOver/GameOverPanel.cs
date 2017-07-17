//Unity

//Game
using Common.Extensions;
using UI.Framework;
using UI.Gameplay.Events;
using UI.Gameplay.Widgets;


//C#
using System;

namespace UI.Gameplay.Panels
{
    public class GameOverPanel : GameplayWidget
    {
        public enum Options { Replay, Quit }

        public float fadeInTime = 2;
        public float fadeOutTime = 1;

        protected override void Awake()
        {
            base.Awake();
            MakeVisible(false);
            SetUpEvents();
        }

        protected void SetUpEvents()
        {
            //listen for prepare/achieving high score panel
            Action<string> ListenForHighScore = new Action<string>(ListenerAchievedHighScore);
            StartListenting(UIEvents.Type.PrepareHighScoreNumber, ListenForHighScore);

            //listen for toggling game panel
            Action<String> ToggleGameOverPanel = new Action<string>(ListenerDisplay);
            StartListenting(UIEvents.Type.ToggleGameOverPanel, ToggleGameOverPanel);
        }

        private void ListenerAchievedHighScore(string message)
        {
            bool achieveScore = message.BoolParse();
        }

        private void ListenerDisplay(string message)
        {
            HUD.VisibleToggle toggle = (HUD.VisibleToggle)Enum.Parse(typeof(HUD.VisibleToggle), message);
            if (toggle == HUD.VisibleToggle.Display) Display();
            else
            {
                Hide();
            }
        }

        public void Display()
        {
            EnableInteraction(true);
            FadeTo(1, fadeInTime);
        }

        public void Hide()
        {
            EnableInteraction(false);
            FadeTo(0, fadeOutTime);
        }

        public void GameOverButton_OnSelected(Options option)
        {
            if (option == Options.Quit) TriggerHUDEvent(UIEvents.Type.QuitButtonSelected);
            else TriggerHUDEvent(UIEvents.Type.ReplayButtonSelected);
        }
    }
}
