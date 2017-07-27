 //Unity
using UnityEngine;

//Game
using Audio;
using UI.Framework;
using UI.Gameplay.Widgets.CombinationWidget;
using Managers;

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

        private int pointsThisRound = 0;
        private bool perfectRound = true;
        private bool achieveHighScore = false;
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

                if(correctInput)
                {
                    pointsThisRound += stateMachine.GetGameBlueprint().pointsPerCorrectAnswer;
                }
                else
                {
                    perfectRound = false;
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
            perfectRound = true;
            achieveHighScore = false;
            reviewTimer = 0;
            currentReviewIndex = 0;
            pointsThisRound = 0;
        }
        #endregion

        #region Gameplay Methods
        private void DetermineNextSteps()
        {
            //You have been detected
            if (stateMachine.detectionLevel >= 100)
            {
                //to game over state
                stateMachine.StartCoroutine(ChangeState(GameplayStateMachine.GameplayState.GameOver));
            }
            //You won! Somehow
            else if(stateMachine.GetRound() == stateMachine.GetGameBlueprint().combinationCount)
            {
                stateMachine.gameWon = true; //GAME WON!
                
                //update score with win points!
                stateMachine.playerScore += stateMachine.GetGameBlueprint().pointsForWin;
                stateMachine.TriggerHUDEvent(UIEvents.Type.UpdateScoreText, stateMachine.playerScore.ToString());
                //To Game over state
                stateMachine.StartCoroutine(ChangeState(GameplayStateMachine.GameplayState.GameOver));
            }
            //continue game
            else
            {
               
                //Unlock progress orb
                stateMachine.TriggerHUDEvent(UIEvents.Type.UnlockProgressOrb);
                //Update Score Text
                stateMachine.TriggerHUDEvent(UIEvents.Type.UpdateScoreText,GetUpdatedScore().ToString());
                //TO DISPLAY STATE
                stateMachine.StartCoroutine(ChangeState(GameplayStateMachine.GameplayState.Display));
            }
        }
        private IEnumerator ChangeState(GameplayStateMachine.GameplayState nextState)
        {
            if(nextState == GameplayStateMachine.GameplayState.GameOver)
            {
                yield return UpdateHighScoreGameplay();
            }

            //wait a second to give the user time
            yield return new WaitForSeconds(1.5f);
            //remove numbers on UI
            stateMachine.TriggerHUDEvent(UIEvents.Type.RemoveCombination, combinationFadeOutTime.ToString());
            //fade out combination
            yield return new WaitForSeconds(combinationFadeOutTime);
            //TO Next State
            stateMachine.ChangeState(nextState);
        }

        private IEnumerator UpdateHighScoreGameplay()
        {
            bool hasHighScore = false;
            yield return GAMEManager.Instance.IsHighScore(stateMachine.GetHighScoreMax(), stateMachine.playerScore, value => { hasHighScore = value; });
            achieveHighScore = hasHighScore;
        }

        private int GetUpdatedScore()
        {
            stateMachine.playerScore += (perfectRound) ? stateMachine.GetGameBlueprint().pointsPerCorrectRound : 0;
            stateMachine.playerScore += pointsThisRound;
            return stateMachine.playerScore;
        }
        #endregion
    }
}
