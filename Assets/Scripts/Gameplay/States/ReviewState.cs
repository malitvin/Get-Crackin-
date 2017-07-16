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

        #region Privates
        private bool reviewing = true;
        private float reviewTimer = 0;
        #endregion

        #region Interface Methods
        public void Begin()
        {

        }

        public void Update()
        {
            if (!reviewing) return;
            reviewTimer += Time.deltaTime;
            if (reviewTimer > stateMachine.GetGameBlueprint().timeBetweenNumbersDisplayed)
            {
                reviewTimer = 0;

            }
        }

        public void End()
        {
            reviewing = true;
            reviewTimer = 0;
        }
        #endregion
    }
}
