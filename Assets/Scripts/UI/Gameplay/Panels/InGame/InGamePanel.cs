//Unity
using UnityEngine;

//Game
using UI.Gameplay.Events;
using UI.Gameplay.Widgets;

//C#
using System;

namespace UI.Gameplay.Panels
{
    /// <summary>
    /// Main in game panel
    /// </summary>
    public class InGamePanel : GameplayWidget
    {
        public float fadeInTime = 2;
        public float fadeOutTime = 2;

        protected override void Awake()
        {
            base.Awake();
            MakeVisible(false);
            SetUpEvents();
        }

        protected void SetUpEvents()
        {
            Action<String> ToggleGamePanel = new Action<string>(ListenerDisplay);
            StartListenting(Framework.UIEvents.Type.ToggleGamePanel, ToggleGamePanel);
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
    }
}
