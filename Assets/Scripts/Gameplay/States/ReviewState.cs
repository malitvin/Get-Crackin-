//Unity
using UnityEngine;

//Game
using Audio;
using UI.Framework;
using UI.Gameplay.Widgets.CombinationWidget;

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
        private bool reviewing = true;
        private float reviewTimer = 0;
        private int currentReviewIndex = 0;
        #endregion

        #region Interface Methods
        public void Begin()
        {

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

                //display combo number as incorrect or correct
                stateMachine.TriggerHUDEvent(UIEvents.Type.DisplayCombinationNumber, correctInput
                    ? CombinationDisplay.Type.Correct.ToString()
                    : CombinationDisplay.Type.InCorrect.ToString());
                //play correct or incorrect sound
                stateMachine.PlaySound(correctInput
                    ? AudioFiles.GameplaySoundClip.Correct
                    : AudioFiles.GameplaySoundClip.Incorrect);

                if(currentReviewIndex == stateMachine.GetCombinationNumber())
                {
                    Debug.Log("DONE");
                }


            }
        }

        public void End()
        {
            reviewing = true;
            reviewTimer = 0;
            currentReviewIndex = 0;
        }
        #endregion
    }
}
