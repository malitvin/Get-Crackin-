//Unity
using UnityEngine;
using UnityEngine.SceneManagement;


//Game
using Audio;
using UI.Framework;
using UI.MainMenu.Panels;
using UI.MainMenu.Events;

using Managers.GameSettings;
using Database;

//C#
using System;
using System.Collections;
using System.Collections.Generic;

namespace UI.MainMenu.States
{
    public class MainMenuStateController : MonoBehaviour
    {
        #region Publics
        public PanelsBlueprint mainMenuBlueprint;

        [Space(10)]
        [Range(0.25f, 3)]
        public float mainPanelFadeTime;
        [Range(0.25f, 3)]
        public float panelFadeTime;
        [Range(0.25f, 3)]
        public float panelGrowSpeed;
        [Range(0.25f, 3)]
        public float panelShrinkSpeed;

        [Space(10)]

        public iTween.EaseType panelGrowEaseType;
        public iTween.EaseType panelShrinkEaseType;
        #endregion

        #region State Info
        public enum MainMenuState { Main, Exit }

        private Dictionary<MainMenuState, IMainMenuState> StateLookup;

        /// <summary>
        /// For Performance to avoid boxing
        /// </summary>
        public struct MyEnumComparer : IEqualityComparer<MainMenuState>
        {
            public bool Equals(MainMenuState x, MainMenuState y)
            {
                return x == y;
            }

            public int GetHashCode(MainMenuState obj)
            {
                return (int)obj;
            }
        }

        private IMainMenuState currentState;

        #endregion

        #region Components
        private Canvas mainCanvas;
        public Canvas _MainCanvas
        {
            get { return mainCanvas ?? (mainCanvas = FindObjectOfType<Canvas>()); }
        }

        private MainMenuObserver mainMenuObserver;
        public MainMenuObserver _MainMenuObserver
        {
            get { return mainMenuObserver ?? (mainMenuObserver = FindObjectOfType<MainMenuObserver>()); }
        }

        private BaseSoundController baseSoundController;
        public BaseSoundController _BaseSoundController
        {
            get { return baseSoundController ?? (baseSoundController = FindObjectOfType<BaseSoundController>()); }
        }

        private HighScoreController highScoreController;
        private HighScoreController _HighScoreController
        {
            get { return highScoreController ?? (highScoreController = FindObjectOfType<HighScoreController>()); }
        }
        #endregion

        #region Unity Methods
        private void Start()
        {
            ChangeState(MainMenuState.Main);
            GameSettings.SetGameDifficulty(GameSettings.Difficulty.Easy);
        }
        private void Update()
        {
            if (currentState != null) currentState.Update();
        }
        #endregion

        #region State Methods
        private void CreateStateMachine()
        {
            StateLookup = new Dictionary<MainMenuState, IMainMenuState>();
            StateLookup.Add(MainMenuState.Main, new MainState(this));
            StateLookup.Add(MainMenuState.Exit, new ExitState(this));
        }

        public void ChangeState(MainMenuState newState)
        {
            if (StateLookup == null) CreateStateMachine();

            if (currentState == StateLookup[newState]) return;

            if (currentState != null) currentState.End();


            currentState = StateLookup[newState];
            currentState.Begin();
        }
        #endregion

        #region UI Methods
        public void TriggerUIEvent(UIEvents.Type type)
        {
            _MainMenuObserver.TriggerEvent(type);
        }
        #endregion

        #region High Score Methods
        public IEnumerator GetHighScores(Action<List<dreamloLeaderBoard.Score>> callback)
        {
            List<dreamloLeaderBoard.Score> scores = null;
            yield return _HighScoreController.GetHighScores(value => { scores = value; });
            callback(scores);
        }
        #endregion

        #region LOADING
        public void LoadGAME()
        {
            SceneManager.LoadScene(1);
        }
        #endregion
    }
}
