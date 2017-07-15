//Unity
using UnityEngine;


//C#
using System.Collections;

//Game
using Gameplay.Events;
using Cameras;

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

            stateMachine.StartCoroutine(BringInCamera());
        }

        public void Update()
        {

        }

        public void End()
        {

        }
        #endregion

        private IEnumerator BringInCamera()
        {
            yield return new WaitForSeconds(cameraInWaitTime);
            stateMachine.TriggerGameplayEvent(GameplayEvent.Type.CameraChange, GameplayCamera.LocationKey.Main.ToString());
        }

    }
}
