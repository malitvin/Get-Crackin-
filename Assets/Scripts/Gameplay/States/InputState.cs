//Unity
using UnityEngine;

//Game 
using Audio;
using Gameplay.GameInput;

//UI
using UI.Framework;
using UI.Gameplay.Widgets.CombinationWidget;

//C#
using System.Collections;
using System.Collections.Generic;

namespace Gameplay.States
{
    /// <summary>
    /// State where the user input combination is taken in
    /// </summary>
    public class InputState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public InputState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
        #endregion

        #region Privates
        private NumberInput userInput = new NumberInput();
        private int NumberCount = 0; //how many numbers has the user typed in
        private float combinationFadeOutTime = 0.5f;
        private bool receivingInput = true;
        #endregion

        #region Interface Methods
        public void Begin()
        {
            stateMachine.TriggerHUDEvent(UIEvents.Type.ChangeGameStatusText, "Receiving Input");
            //Clear user input list
            stateMachine.ClearUserInput();
        }

        public void Update()
        {
            if (!receivingInput) return;

            if (userInput.NumberPressed())
            {
                int number = userInput.GetNumberPressed();
                //Play Sounds
                stateMachine.PlaySound(AudioFiles.GameplaySoundClip.UserInput);
                //display number on screen
                stateMachine.TriggerHUDEvent(UIEvents.Type.PrepareCombinationNumber, number.ToString());
                stateMachine.TriggerHUDEvent(UIEvents.Type.DisplayCombinationNumber, CombinationDisplay.Type.Normal.ToString());

                //spawn anim number
                stateMachine.TriggerHUDEvent(UIEvents.Type.SpawnAnimNumber, number.ToString());

                //add to state controller user input
                stateMachine.AddToUserInput(number);
                //increment number count
                NumberCount++;
                if (stateMachine.GetRound() == NumberCount)
                {
                    receivingInput = false;

                    stateMachine.StartCoroutine(FadeOutNumbers());
                    
                }



            }
        }

        public void End()
        {
            receivingInput = true;
            NumberCount = 0;
        }
        #endregion

        #region Gameplay Methods
        private IEnumerator FadeOutNumbers()
        {
            //wait a second to give the user time
            yield return new WaitForSeconds(1.5f);
            //remove numbers on UI
            stateMachine.TriggerHUDEvent(UIEvents.Type.RemoveCombination, combinationFadeOutTime.ToString());
            //fade out combination
            yield return new WaitForSeconds(combinationFadeOutTime);
            //TO Review STATE
            stateMachine.ChangeState(GameplayStateMachine.GameplayState.Review);
        }
        #endregion

    }
}
