//Unity
using UnityEngine;


//C#
using System.Collections;

//Game
using Gameplay.Events;
using Cameras;

//UI
using UI.Framework;
using UI.Gameplay.Events;

namespace Gameplay.States
{
    /// <summary>
    /// Intro state of the game
    /// </summary>
    public class IntroState : IGameState
    {
        #region Constructor
        GameplayStateMachine stateMachine;
        public IntroState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
        #endregion

        #region Privates
        private const float cameraInWaitTime = 2;
        #endregion

        #region Interface Methods
        public void Begin()
        {
            stateMachine.TriggerHUDEvent(UI.Framework.UIEvents.Type.SceneComeIn);

            stateMachine.StartCoroutine(IntroSequence());
        }

        public void Update()
        {

        }

        public void End()
        {

        }
        #endregion

        private IEnumerator IntroSequence()
        {
            yield return new WaitForSeconds(cameraInWaitTime);
            //bring in camera
            stateMachine.TriggerGameplayEvent(GameplayEvent.Type.CameraChange, GameplayCamera.LocationKey.Main.ToString());
            //bring in Game Panel
            yield return new WaitForSeconds(cameraInWaitTime);
            stateMachine.TriggerHUDEvent(UIEvents.Type.ToggleGamePanel, HUD.VisibleToggle.Display.ToString());
        }

    }
}
