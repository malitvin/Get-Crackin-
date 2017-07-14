//Unity
using UnityEngine;

namespace UI.MainMenu.States
{
    public class ExitState : IMainMenuState
    {
        #region Constructor
        private MainMenuStateController controller;
        public ExitState(MainMenuStateController controller)
        {
            this.controller = controller;
        }
        #endregion

        #region Unity Methods
        public void Begin()
        {
            controller.TriggerUIEvent(Framework.UIEvents.Type.SceneComeOut);
        }

        public void Update()
        {

        }

        public void End()
        {

        }
        #endregion

    }
}
