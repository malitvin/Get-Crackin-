//Unity
using UnityEngine;

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

        #region Interface Methods
        public void Begin()
        {

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
