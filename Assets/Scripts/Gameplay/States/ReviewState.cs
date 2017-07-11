//Unity
using UnityEngine;

namespace Gameplay.States
{
    /// <summary>
    /// State all user input has been completed for a round and it is reviewed
    /// </summary>
    public class ReviewState : IGameState
    {

        #region Constructor
        GameplayStateMachine stateMachine;
        public ReviewState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
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
