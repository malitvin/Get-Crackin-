﻿//Unity
using UnityEngine;

//Game
using UI.MainMenu.Widgets;

namespace UI.MainMenu.Panels.PlayPanel
{
    /// <summary>
    /// The button on the play game panel that actually starts the game
    /// </summary>
    public class PlayGameButton : MainMenuWidget
    {

        protected override void UI_OnSelected()
        {
            base.UI_OnSelected();
            EnableInteraction(false); //disable play button by default
            TriggerEvent(Framework.UIEvents.Type.PlayGame); // Trigger Play Game Event
        }
    }
}
