//Unity
using UnityEngine;


//Game
using UI.MainMenu.Panels;
using UI.MainMenu.Events;

//C#
using System.Collections.Generic;

namespace UI.MainMenu.States
{
    public class MainMenuStateController : MonoBehaviour
    {
        #region Publics
        public PanelsBlueprint mainMenuBlueprint;

        [Space(10)]
        [Range(0.25f,3)]
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
        #endregion

        #region Unity Methods
        private void Awake()
        {
            ChangeState(MainMenuState.Main);
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
    }
}
