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
        private List<int> userInput;

        [ReadOnlyCustom]
        public float detectionLevel = 0;
        [ReadOnlyCustom]
        public int round = 0;
        [ReadOnlyCustom]
        public int currentCombinationCount;
        [ReadOnlyCustom]
        public int playerScore;
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
        public enum GameplayState { SetUp, Intro, Display, Input, Review, GameOver, Replay,Win }

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
            StateLookup.Add(GameplayState.Win, new WinState(this));
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
        /// <summary>
        /// Trigger Gameplay event
        /// </summary>
        /// <param name="e"></param>
        /// <param name="message"></param>
        public void TriggerGameplayEvent(GameplayEvent.Type e,string message)
        {
            GAMEManager.Instance.TriggerGameplayEvent(e, message);
        }

        /// <summary>
        /// PLay Audio
        /// </summary>
        /// <param name="clip"></param>
        public void PlaySound(Audio.AudioFiles.GameplaySoundClip clip)
        {
            GAMEManager.Instance.PlaySound(clip);
        }

        /// <summary>
        /// PLay Audio
        /// </summary>
        /// <param name="clip"></param> (UI)
        public void PlaySound(Audio.AudioFiles.UISoundClip clip)
        {
            GAMEManager.Instance.PlaySound(clip);
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

        /// <summary>
        /// Add to user input list
        /// </summary>
        /// <param name="input"></param>
        public void AddToUserInput(int input)
        {
            userInput.Add(input);
        }

        /// <summary>
        /// Clear user input
        /// </summary>
        public void ClearUserInput()
        {
            userInput = new List<int>();
        }

        /// <summary>
        /// Is the user input equal to that of the  combination
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool AreEqualByIndex(int index)
        {
            return userInput[index] == combination[index];
        }

        /// <summary>
        /// Get the combination number by the current combination count
        /// </summary>
        /// <returns></returns>
        public int GetCombinationNumber()
        {
            return combination[GetCurrentCombinationCount()];
        }

        /// <summary>
        /// Get user input by current combinationCount
        /// </summary>
        /// <returns></returns>
        public int GetUserInputNumber(int index)
        {
            return userInput[index];
        }

        /// <summary>
        /// Gets the current combination count
        /// </summary>
        /// <returns></returns>
        public int GetCurrentCombinationCount()
        {
            return currentCombinationCount;
        }

        /// <summary>
        /// Reset Combination COunt
        /// </summary>
        public void ResetCombinationCount()
        {
            currentCombinationCount = 0;
        }

        /// <summary>
        /// Increment the combination Count
        /// </summary>
        public void IncrementCombinationCount()
        {
            currentCombinationCount++;
        }

        /// <summary>
        /// Get the currentRound
        /// </summary>
        /// <returns></returns>
        public int GetRound()
        {
            return round;
        }

        /// <summary>
        /// Increment Round
        /// </summary>
        public void IncrementRound()
        {
            round++;
        }

        #endregion

        #region UI Methods
        public void TriggerHUDEvent(UIEvents.Type type,string message="")
        {
            _HUD.TriggerEvent(type,message);
        }
        public void ListenForHUDEvent(UIEvents.Type type,System.Action<string> listen)
        {
            _HUD.StartListening(type, listen);
        }
        #endregion


    }
}
