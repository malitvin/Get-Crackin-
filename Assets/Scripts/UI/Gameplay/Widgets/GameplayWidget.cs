//Unity
using UnityEngine;

//Game
using UI.Gameplay.Events;
using UI.Framework;

namespace UI.Gameplay.Widgets
{
    /// <summary>
    /// Gameplay widget with access to HUD for UI Observer events
    /// </summary>
    public class GameplayWidget : AbstractUI
    {
        private HUD hud;
        private HUD _HUD
        {
            get { return hud ?? (hud = GetComponentInParent<HUD>()); }
        }

        protected void TriggerHUDEvent(UIEvents.Type type)
        {
            _HUD.TriggerEvent(type);
        }

        protected void StartListenting(UIEvents.Type type,System.Action<string> listener)
        {
            _HUD.StartListening(type, listener);
        }
    }
}
