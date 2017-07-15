//Unity
using UnityEngine;

namespace Gameplay.States
{
    /// <summary>
    /// Intro state of the game
    /// </summary>
    public class IntroState : IGameState
    {
        #region Constructor
        GameplayStateMachine stateMachine;
        public IntroState(GameplayStateMachine stateMachine) { this.stateMachine = stateMachine; }
        #endregion

        #region Interface Methods
        public void Begin()
        {
            stateMachine.TriggerHUDEvent(UI.Framework.UIEvents.Type.SceneComeIn);
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
