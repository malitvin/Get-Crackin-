//Unity
using UnityEngine;


//Game
using UI.MainMenu.States;

//C#
using System.Collections.Generic;

namespace UI.MainMenu
{
    public class MainMenuStateController : MonoBehaviour
    {
        public enum MainMenuState { Main, Exit }

        private Dictionary<MainMenuState, IMainMenuState> StateLookup;

        private IMainMenuState currentState;

        #region Unity Methods
        private void Awake()
        {
            ChangeState(MainMenuState.Main);
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
