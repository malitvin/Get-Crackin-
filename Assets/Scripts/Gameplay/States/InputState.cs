//Unity
using UnityEngine;

//Game 
using Audio;
using Gameplay.GameInput;

//UI
using UI.Framework;
using UI.Gameplay.Widgets.CombinationWidget;

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
        #endregion

        #region Interface Methods
        public void Begin()
        {

        }

        public void Update()
        {
            if(userInput.NumberPressed())
            {
                int number = userInput.GetNumberPressed();
                //Play Sounds
                stateMachine.PlaySound(AudioFiles.GameplaySoundClip.UserInput);
                //display number on screen
                stateMachine.TriggerHUDEvent(UIEvents.Type.PrepareCombinationNumber, number.ToString());
                stateMachine.TriggerHUDEvent(UIEvents.Type.DisplayCombinationNumber, CombinationDisplay.Type.Normal.ToString());


            }
        }

        public void End()
        {

        }
        #endregion
    }
}
