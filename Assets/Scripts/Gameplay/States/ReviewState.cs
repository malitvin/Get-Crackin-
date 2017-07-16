 //Unity
using UnityEngine;

//Game
using Audio;
using UI.Framework;
using UI.Gameplay.Widgets.CombinationWidget;

//C#
using System.Collections;

namespace Gameplay.States
{
    /// <summary>
    /// State all user input has been completed for a round and it is reviewed
    /// </summary>
    public class ReviewState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public ReviewState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
        #endregion

        #region Privates
        private const float combinationFadeOutTime = 0.5f;
        private bool reviewing = true;
        private float reviewTimer = 0;
        private int currentReviewIndex = 0;
        #endregion

        #region Interface Methods
        public void Begin()
        {
            stateMachine.TriggerHUDEvent(UIEvents.Type.ChangeGameStatusText, "Comparing Input");
        }

        public void Update()
        {
            if (!reviewing) return;
            reviewTimer += Time.deltaTime;
            if (reviewTimer > stateMachine.GetGameBlueprint().timeBetweenNumbersDisplayed)
            {
                reviewTimer = 0;

                bool correctInput = stateMachine.AreEqualByIndex(currentReviewIndex);

                stateMachine.TriggerHUDEvent(UIEvents.Type.PrepareCombinationNumber, stateMachine.GetUserInputNumber(currentReviewIndex).ToString());

                //spawn anim number
                stateMachine.TriggerHUDEvent(UIEvents.Type.SpawnAnimNumber, stateMachine.GetUserInputNumber(currentReviewIndex).ToString());


                //display combo number as incorrect or correct
                stateMachine.TriggerHUDEvent(UIEvents.Type.DisplayCombinationNumber, correctInput
                    ? CombinationDisplay.Type.Correct.ToString()
                    : CombinationDisplay.Type.InCorrect.ToString());
                //play correct or incorrect sound
                stateMachine.PlaySound(correctInput
                    ? AudioFiles.GameplaySoundClip.Correct
                    : AudioFiles.GameplaySoundClip.Incorrect);

                if(!correctInput)
                {
                    stateMachine.detectionLevel += stateMachine.GetGameBlueprint().incorrectPenalty;
                    stateMachine.TriggerHUDEvent(UIEvents.Type.UpdateDetectionSlider, stateMachine.detectionLevel.ToString());
                }

                //Review Complete
                if(currentReviewIndex == stateMachine.GetRound()-1)
                {
                    reviewing = false;
                    DetermineNextSteps();

                }
                currentReviewIndex++;

            }
        }

        public void End()
        {
            reviewing = true;
            reviewTimer = 0;
            currentReviewIndex = 0;
        }
        #endregion

        #region Gameplay Methods
        private void DetermineNextSteps()
        {
            //You have somehow won
            if (stateMachine.GetRound() == stateMachine.GetGameBlueprint().combinationCount + 1)
            {
                stateMachine.StartCoroutine(FadeOutNumbers(GameplayStateMachine.GameplayState.GameOver));
            }
            //continue game
            else
            {
                stateMachine.StartCoroutine(FadeOutNumbers(GameplayStateMachine.GameplayState.Display));
                //Unlock progress orb
                stateMachine.TriggerHUDEvent(UIEvents.Type.UnlockProgressOrb);
            }
        }
        private IEnumerator FadeOutNumbers(GameplayStateMachine.GameplayState nextState)
        {
            //wait a second to give the user time
            yield return new WaitForSeconds(1.5f);
            //remove numbers on UI
            stateMachine.TriggerHUDEvent(UIEvents.Type.RemoveCombination, combinationFadeOutTime.ToString());
            //fade out combination
            yield return new WaitForSeconds(combinationFadeOutTime);
            //TO Next State
            stateMachine.ChangeState(nextState);
        }
        #endregion
    }
}
