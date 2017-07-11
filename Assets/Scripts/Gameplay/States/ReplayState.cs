//Unity
using UnityEngine;

namespace Gameplay.States
{
    /// <summary>
    /// State where the user has chosen to replay the game and all elements are restarted
    /// </summary>
    public class ReplayState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public ReplayState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
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
