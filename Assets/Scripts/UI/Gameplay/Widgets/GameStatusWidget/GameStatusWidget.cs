//Unity
using UnityEngine;

//Game
using UI.Framework;

//C#
using System;

namespace UI.Gameplay.Widgets.GameStatusWidget
{
    /// <summary>
    /// The gameplay widget with the current status of the game
    /// </summary>
    public class GameStatusWidget : GameplayWidget
    {
        private CurrentStateText currentStateText;
        private CurrentStateText _CurrentStateText
        {
            get { return currentStateText ?? (currentStateText = GetComponentInChildren<CurrentStateText>()); }
        }

        protected override void Awake()
        {
            base.Awake();
            _CurrentStateText.MakeVisible(false);
            CreateListeners();
        }

        protected void CreateListeners()
        {
            Action<string> updateDisplayText = new Action<string>(ChangeDisplayText);
            StartListenting(UIEvents.Type.ChangeGameStatusText, updateDisplayText);
        }

        private void ChangeDisplayText(string message)
        {
            _CurrentStateText.ChangeText(message);
        }
    }
}
