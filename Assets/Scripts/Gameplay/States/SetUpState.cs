//Unity
using UnityEngine;

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
            stateMachine.TriggerHUDEvent(UI.Framework.UIEvents.Type.InitProgressWidget,stateMachine.GetGameBlueprint().combinationCount.ToString());
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
