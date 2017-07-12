//Unity
using UnityEngine;

//Game
using UI.MainMenu.States;
using UI.Framework;

namespace UI.MainMenu.Widgets
{
    /// <summary>
    /// Any Button that leads to navigation in the main menu
    /// </summary>
    public class NavigationButton : MainMenuWidget
    {
        [Tooltip("When you click this button where does it take you")]
        public MainState.PanelType destination;

        protected override void Button_OnSelected()
        {
            base.Button_OnSelected();
            TriggerEvent(UIEvents.Type.MainMenuNavigation,destination.ToString());
        }

    }
}
