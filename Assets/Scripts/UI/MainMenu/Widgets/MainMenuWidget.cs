//Unity
using UnityEngine;

//Game
using UI.MainMenu.Events;
using UI.Framework;

namespace UI.MainMenu.Widgets
{
    /// <summary>
    /// Any UI widget in the main menu
    /// </summary>
    public class MainMenuWidget : AbstractUI
    {
        private MainMenuObserver observer;
        private MainMenuObserver _Observer
        {
            get { return observer ?? (observer = FindObjectOfType<MainMenuObserver>()); }
        }

        /// <summary>
        /// Trigger a main menu ui event
        /// </summary>
        /// <param name="type"></param>
        protected void TriggerEvent(UIEvents.Type type,string message="")
        {
            _Observer.TriggerEvent(type,message);
        }
    }
}
