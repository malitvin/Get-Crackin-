//Unity
using UnityEngine;

namespace Gameplay.States
{
    /// <summary>
    /// The State where the game is over
    /// </summary>
    public class GameOverState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public GameOverState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
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
