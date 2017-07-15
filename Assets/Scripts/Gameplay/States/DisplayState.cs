//Unity
using UnityEngine;

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

        #region Gameplay Methods

        #endregion
    }
}
