//Unity
using UnityEngine;
using UnityEngine.SceneManagement;

//Game
using Audio;
using Managers;
using Gameplay.Events;
using Cameras;

//UI
using UI.Framework;
using UI.Gameplay.Events;

//C#
using System;
using System.Collections;

namespace Gameplay.States
{
    /// <summary>
    /// The State where the game is over
    /// </summary>
    public class GameOverState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public GameOverState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
        #endregion

        #region Interface Methods
        public void Begin()
        {
            //Create HUD listeners
            CreateListeners();

            //Hide Game Panel
            stateMachine.TriggerHUDEvent(UIEvents.Type.ToggleGamePanel, HUD.VisibleToggle.Hide.ToString());
            //move out camera
            stateMachine.TriggerGameplayEvent(GameplayEvent.Type.CameraChange, GameplayCamera.LocationKey.Lose.ToString());
            //Play game Over sound
            GAMEManager.Instance.PlaySound(AudioFiles.GameplaySoundClip.GameOver);

            //Display game over panel
            stateMachine.StartCoroutine(DisplayGameOverPanel());
        }

        public void Update()
        {

        }

        public void End()
        {

        }
        #endregion

        #region Gameplay Methods
        private void CreateListeners()
        {
            //listen for replay button selected on game over panel
            Action<string> ListenForReplayButton = new Action<string>(ReplayGame);
            stateMachine.ListenForHUDEvent(UIEvents.Type.ReplayButtonSelected, ListenForReplayButton);

            //listen for quit button selected on quit panel
            Action<string> ListenForQuitButton = new Action<string>(QuitGame);
            stateMachine.ListenForHUDEvent(UIEvents.Type.QuitButtonSelected, ListenForQuitButton);
        }

        private IEnumerator DisplayGameOverPanel()
        {
            yield return new WaitForSeconds(3);
            stateMachine.TriggerHUDEvent(UIEvents.Type.PrepareHighScoreNumber,false.ToString());
            stateMachine.TriggerHUDEvent(UIEvents.Type.ToggleGameOverPanel, HUD.VisibleToggle.Display.ToString());
        }

        /// <summary>
        /// when the replay game button is selected
        /// </summary>
        /// <param name="message"></param>
        private void ReplayGame(string message)
        {
            stateMachine.PlaySound(AudioFiles.UISoundClip.CartoonPop);
        }

        /// <summary>
        /// when the quit game button is selected
        /// </summary>
        /// <param name="message"></param>
        private void QuitGame(string message)
        {
            stateMachine.PlaySound(AudioFiles.UISoundClip.CartoonPop);
            stateMachine.StartCoroutine(QuitRoutine());
        }
        //Sore Loser
        private IEnumerator QuitRoutine()
        {
            stateMachine.TriggerHUDEvent(UIEvents.Type.ToggleGameOverPanel, HUD.VisibleToggle.Hide.ToString());
            stateMachine.TriggerHUDEvent(UIEvents.Type.SceneComeOut);
            stateMachine.TriggerGameplayEvent(GameplayEvent.Type.CameraChange, GameplayCamera.LocationKey.Start.ToString());
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(0); //LOAD MAIN MENU
           
        }
        #endregion
    }
}
