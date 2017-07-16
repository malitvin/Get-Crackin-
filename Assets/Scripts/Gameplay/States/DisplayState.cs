//Unity
using UnityEngine;

//C#
using System.Collections;

//Game
using Audio;
using UI.Framework;
using UI.Gameplay.Widgets.CombinationWidget;

namespace Gameplay.States
{
    /// <summary>
    /// State of the game where the number combination is displayed on the screen
    /// </summary>
    public class DisplayState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public DisplayState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
        #endregion

        #region Private
        private bool displayingNumbers = false;
        private float displayTimer = 0;

        private float combinationFadeOutTime = 0.5f;
        #endregion

        #region Interface Methods
        public void Begin()
        {
            stateMachine.StartCoroutine(WaitForDisplay());

            stateMachine.TriggerHUDEvent(UIEvents.Type.ChangeGameStatusText, "Displaying Combination");
        }

        public void Update()
        {
            if (!displayingNumbers) return;
            displayTimer += Time.deltaTime;
            if (displayTimer > stateMachine.GetGameBlueprint().timeBetweenNumbersDisplayed)
            {
                displayTimer = 0;
                //Play sound
                stateMachine.PlaySound(AudioFiles.GameplaySoundClip.DisplayInput);
                //display number on screen
                stateMachine.TriggerHUDEvent(UIEvents.Type.PrepareCombinationNumber, stateMachine.GetCombinationNumber().ToString());
                stateMachine.TriggerHUDEvent(UIEvents.Type.DisplayCombinationNumber, CombinationDisplay.Type.Normal.ToString());

                //if the current combination count is equal to the round
                if(stateMachine.GetCurrentCombinationCount()+1 == stateMachine.GetRound())
                {
                    displayingNumbers = false;
                    stateMachine.StartCoroutine(FadeOutNumbers());
                }
                else
                {
                    stateMachine.IncrementCombinationCount();
                }
            }
        }

        public void End()
        {
            displayingNumbers = false;
        }
        #endregion

        #region Gameplay Methods
        private IEnumerator WaitForDisplay()
        {
            yield return new WaitForSeconds(stateMachine.GetGameBlueprint().initialDisplayWait);
            displayingNumbers = true;
        }

        private IEnumerator FadeOutNumbers()
        {
            //wait a second to give the user time
            yield return new WaitForSeconds(1.5f);
            //remove numbers on UI
            stateMachine.TriggerHUDEvent(UIEvents.Type.RemoveCombination, combinationFadeOutTime.ToString());
            //fade out combination
            yield return new WaitForSeconds(combinationFadeOutTime);
            //TO INPUT STATE
            stateMachine.ChangeState(GameplayStateMachine.GameplayState.Input);
        }
        #endregion
    }
}
