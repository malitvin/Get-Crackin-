//Unity
using UnityEngine;

//C#
using System.Collections.Generic;

//Game
using Gameplay.Events;
using Gameplay.Configuration;
//UI
using UI.Framework;
using UI.Gameplay.Events;
//Common
using Common.Attributes;
//Managers
using Managers;
using Managers.GameSettings;


namespace Gameplay.States
{
    /// <summary>
    /// Gameplay Controller to controller entire flow of gameplay 
    /// through the IGAMESTATE Interface
    /// </summary>
    public class GameplayStateMachine : MonoBehaviour
    {
        #region Public Variables
        [Space(10)]
        public GameplayConfig gameBlueprint;

        [Space(10)]

        [ReadOnlyCustom]
        //this is used for debug purposes to see what gameplay state we are in in the machine
        public string currentGameplayState = "Intro";
        #endregion

        #region Private Variables
        private List<int> combination;
        #endregion

        #region Components
        //Access to hud for UI events, connection between gameplay and UI
        private HUD hud;
        private HUD _HUD
        {
            get { return hud ?? (hud = FindObjectOfType<HUD>()); }
        }
        #endregion

        #region State Creation
        public enum GameplayState { SetUp, Intro, Display, Input, Review, GameOver, Replay }

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
            StateLookup.Add(GameplayState.SetUp, new SetUpState(this));
            StateLookup.Add(GameplayState.Intro, new IntroState(this));
            StateLookup.Add(GameplayState.Display, new DisplayState(this));
            StateLookup.Add(GameplayState.Input, new InputState(this));
            StateLookup.Add(GameplayState.Review, new ReviewState(this));
            StateLookup.Add(GameplayState.GameOver, new GameOverState(this));
            StateLookup.Add(GameplayState.Replay, new ReplayState(this));
        }

        public void ChangeState(GameplayState newState)
        {
            if (StateLookup == null) CreateStateLookup();

            if (currentState != null) currentState.End();

            currentGameplayState = newState.ToString();
            currentState = StateLookup[newState];
            currentState.Begin();
        }
        #endregion

        #region Unity Methods
        private void Start()
        {
            ChangeState(GameplayState.SetUp);
        }

        private void Update()
        {
            if (currentState != null) currentState.Update();
        }
        #endregion

        #region GAMEPLAY Methods
        public void TriggerGameplayEvent(GameplayEvent.Type e,string message)
        {
            GAMEManager.Instance.TriggerGameplayEvent(e, message);
        }

        /// <summary>
        /// Get our current game blueprint
        /// </summary>
        /// <returns></returns>
        public GameplayConfig.GameplayBlueprint GetGameBlueprint()
        {
            return gameBlueprint.GetBlueprint(GameSettings.GetDifficulty());
        }

        /// <summary>
        /// Generate our combination
        /// </summary>
        public void GenerateCombination()
        {
            //apparently its faster to allocate a new list then call list.Clear();
            //https://www.dotnetperls.com/list-clear
            combination = new List<int>();

            //get combination count
            int count = GetGameBlueprint().combinationCount;

            for (int i = 0; i < count; i++)
            {
                int combinationNumber = Random.Range(1, 10);
                combination.Add(combinationNumber);
            }
        }
        #endregion

        #region UI Methods
        public void TriggerHUDEvent(UIEvents.Type type,string message="")
        {
            _HUD.TriggerEvent(type,message);
        }
        #endregion


    }
}
