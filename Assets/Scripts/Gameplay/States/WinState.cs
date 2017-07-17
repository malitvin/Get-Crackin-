//Unity
using UnityEngine;

namespace Gameplay.States
{
    /// <summary>
    /// The State where you have won the game
    /// </summary>
    public class WinState : IGameState {


        #region Constructor
        GameplayStateMachine stateMachine;
        public WinState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
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
