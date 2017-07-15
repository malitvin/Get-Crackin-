//Unity
using UnityEngine;

//C#
using System.Collections;

//Game
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
        #endregion

        #region Interface Methods
        public void Begin()
        {
            stateMachine.StartCoroutine(WaitForDisplay());
        }

        public void Update()
        {
            if (!displayingNumbers) return;
            displayTimer += Time.deltaTime;
            if (displayTimer > stateMachine.GetGameBlueprint().timeBetweenNumbersDisplayed)
            {
                displayTimer = 0;
                //display number on screen
                stateMachine.TriggerHUDEvent(UIEvents.Type.PrepareCombinationNumber, stateMachine.GetCombinationNumber().ToString());
                stateMachine.TriggerHUDEvent(UIEvents.Type.DisplayCombinationNumber, CombinationDisplay.Type.Normal.ToString());

                //if the current combination count is equal to the round
                if(stateMachine.GetCurrentCombinationCount() == stateMachine.GetRound())
                {
                    stateMachine.ChangeState(GameplayStateMachine.GameplayState.Input);
                }
                else
                {
                    stateMachine.InCrementCombinationCount();
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
        #endregion
    }
}
