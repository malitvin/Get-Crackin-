//Unity
using UnityEngine;

//Game
using Cameras;
using Gameplay.Events;
using UI.Framework;
using UI.Gameplay.Events;

//C#
using System.Collections;

namespace Gameplay.States
{
    /// <summary>
    /// State where the user has chosen to replay the game and all visua/other elements are restarted
    /// </summary>
    public class ReplayState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public ReplayState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
        #endregion

        #region Interface Methods
        public void Begin()
        {
            //remove game over panel
            stateMachine.TriggerHUDEvent(UIEvents.Type.ToggleGameOverPanel, HUD.VisibleToggle.Hide.ToString());

            //camera change
            stateMachine.TriggerGameplayEvent(GameplayEvent.Type.CameraChange, GameplayCamera.LocationKey.Main.ToString());

            //reset hud
            stateMachine.TriggerHUDEvent(UIEvents.Type.ResetProgressOrbs);
            stateMachine.TriggerHUDEvent(UIEvents.Type.UpdateDetectionSlider, 0.ToString());
            stateMachine.TriggerHUDEvent(UIEvents.Type.UpdateScoreText, 0.ToString());

            //RESET MAIN VARIABLES
            stateMachine.GenerateCombination();
            stateMachine.gameWon = false;
            stateMachine.round = 0;
            stateMachine.detectionLevel = 0;
            stateMachine.currentCombinationCount = 0;
            stateMachine.playerScore = 0;

            //transition
            stateMachine.StartCoroutine(TransitionToDisplay());
        }

        public void Update()
        {

        }

        public void End()
        {

        }
        #endregion

        /// <summary>
        /// Toggle game panel and go to display state
        /// </summary>
        /// <returns></returns>
        private IEnumerator TransitionToDisplay()
        {
            yield return new WaitForSeconds(3.0f);

            stateMachine.TriggerHUDEvent(UIEvents.Type.ToggleGamePanel, HUD.VisibleToggle.Display.ToString());
            yield return new WaitForSeconds(1.0f);
            stateMachine.ChangeState(GameplayStateMachine.GameplayState.Display);
        }

    }
}
