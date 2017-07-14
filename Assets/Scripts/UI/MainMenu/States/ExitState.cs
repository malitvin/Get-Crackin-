//Unity
using UnityEngine;

//C#
using System.Collections;

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

            controller.StartCoroutine(LoadGAME());
        }

        public void Update()
        {

        }

        public void End()
        {

        }
        #endregion

        #region Loading Methods
        public IEnumerator LoadGAME()
        {
            yield return new WaitForSeconds(2.0f);
            controller.LoadGAME();
        }
        #endregion

    }
}
