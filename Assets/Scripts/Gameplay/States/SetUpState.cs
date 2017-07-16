//Unity
using UnityEngine;

//Game
using UI.Framework;

namespace Gameplay.States
{
    /// <summary>
    /// Gets the game ready for setup
    /// All Init functions here
    /// </summary>
    public class SetUpState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public SetUpState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
        #endregion

        #region Interface Methods
        public void Begin()
        {
            //init generate combination
            stateMachine.GenerateCombination();
            //UI
            stateMachine.TriggerHUDEvent(UIEvents.Type.InitProgressWidget,stateMachine.GetGameBlueprint().combinationCount.ToString());

            //reset detection level
            stateMachine.detectionLevel = 0;

            stateMachine.ChangeState(GameplayStateMachine.GameplayState.Intro);
        }

        public void Update()
        {

        }

        public void End()
        {

        }
        #endregion
    }
}
