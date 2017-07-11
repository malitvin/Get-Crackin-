//Unity
using UnityEngine;

//C#
using System.Collections.Generic;

namespace Gameplay.States
{
    /// <summary>
    /// Gameplay Controller to controller entire flow of gameplay 
    /// through the IGAMESTATE Interface
    /// </summary>
    public class GameplayStateMachine : MonoBehaviour
    {
        #region State Creation
        public enum GameplayState { Intro, Display, Input, Review, GameOver, Replay }

        private Dictionary<GameplayState, IGameState> StateLookup;

        /// <summary>
        /// For Performance to avoid boxing
        /// </summary>
        public struct MyEnumComparer : IEqualityComparer<GameplayState>
        {
            public bool Equals(GameplayState x, GameplayState y)
            {
                return x == y;
            }

            public int GetHashCode(GameplayState obj)
            {
                return (int)obj;
            }
        }

        private IGameState currentState;
        #endregion

        #region State Lookup
        public void CreateStateLookup()
        {
            StateLookup = new Dictionary<GameplayState, IGameState>();
            StateLookup.Add(GameplayState.Intro, new IntroState(this));
            StateLookup.Add(GameplayState.Display, new DisplayState(this));
            StateLookup.Add(GameplayState.Input, new InputState(this));
            StateLookup.Add(GameplayState.Review, new ReviewState(this));
            StateLookup.Add(GameplayState.GameOver, new GameOverState(this));
            StateLookup.Add(GameplayState.Replay, new ReplayState(this));
        }

        public void ChangeState(GameplayState newState)
        {
            if (currentState == StateLookup[newState]) return;

            if (currentState != null) currentState.End();

            currentState = StateLookup[newState];
            currentState.Begin();
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            ChangeState(GameplayState.Intro);
        }

        private void Update()
        {
            if (currentState != null) currentState.Update();
        }
        #endregion


    }
}
