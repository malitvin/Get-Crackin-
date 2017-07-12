//Unity
using UnityEngine;

//Game
using UI.MainMenu.Panels;

//C#
using System;
using System.Collections;
using System.Collections.Generic;

namespace UI.MainMenu.States
{
    public class MainState : IMainMenuState
    {

        #region Constructor
        private MainMenuStateController controller;
        public MainState(MainMenuStateController controller)
        {
            this.controller = controller;
        }
        #endregion

        #region Panel Type Lookup
        public enum PanelType { Main, Play, HighScores, Credits, HowToPlay }

        private Dictionary<PanelType, MainMenuPanel> PanelLookup;

        /// <summary>
        /// For Performance to avoid boxing
        /// </summary>
        public struct MyEnumComparer : IEqualityComparer<PanelType>
        {
            public bool Equals(PanelType x, PanelType y)
            {
                return x == y;
            }

            public int GetHashCode(PanelType obj)
            {
                return (int)obj;
            }
        }

        public PanelType currentPanelType = PanelType.Main;
        #endregion

        #region Unity Methods
        public void Begin()
        {
            StartListenForEvents();
            GeneratePanels();
        }

        public void Update()
        {

        }

        public void End()
        {
            //Remove the current panel
            PanelLookup[currentPanelType].EnableInteraction(false);
            PanelLookup[currentPanelType].BringOut(controller.panelShrinkSpeed, controller.panelShrinkEaseType, controller.panelFadeTime);
        }
        #endregion

        #region UI Methods
        private void StartListenForEvents()
        {
            //listen for navigation
            Action<string> transitionListen = new Action<string>(TransitionalListen);
            controller._MainMenuObserver.StartListening(Framework.UIEvents.Type.MainMenuNavigation, transitionListen);

            //listen for start playing game
            Action<string> listenStart = new Action<string>(ToExitState);
            controller._MainMenuObserver.StartListening(Framework.UIEvents.Type.PlayGame, listenStart);


        }

        /// <summary>
        /// Generate and store out panel prefabs
        /// </summary>
        private void GeneratePanels()
        {
            PanelLookup = new Dictionary<PanelType, MainMenuPanel>();
            PanelLookup.Add(PanelType.Main, controller._MainCanvas.GetComponentInChildren<MainMenuPanel>()); //add current main menu panel
            int panelCount = controller.mainMenuBlueprint.panels.Length;

            for (int i = 0; i < panelCount; i++)
            {
                MainMenuPanel panel = UnityEngine.Object.Instantiate(controller.mainMenuBlueprint.panels[i].prefab, controller._MainCanvas.transform) as MainMenuPanel;
                panel.MakeVisible(false);
                PanelLookup.Add(controller.mainMenuBlueprint.panels[i].key, panel);
            }
        }

        /// <summary>
        /// Listen for Transition events throughout the main menu
        /// </summary>
        /// <param name="s"></param>
        public void TransitionalListen(string s)
        {
            controller.StartCoroutine(Transition((PanelType)(Enum.Parse(typeof(PanelType), s))));
        }

        /// <summary>
        /// The Main coroutine that controls animation throughout the main menu of the lookup of panels
        /// </summary>
        /// <param name="newPanel"></param>
        /// <returns></returns>
        private IEnumerator Transition(PanelType newPanel)
        {
            //disable interaction on current panel
            PanelLookup[currentPanelType].EnableInteraction(false);

            if (currentPanelType == PanelType.Main)
            {
                PanelLookup[currentPanelType].FadeTo(0, controller.mainPanelFadeTime);
                yield return new WaitForSeconds(controller.mainPanelFadeTime);
            }
            else
            {
                PanelLookup[currentPanelType].BringOut(controller.panelShrinkSpeed, controller.panelShrinkEaseType, controller.panelFadeTime);
                yield return new WaitForSeconds(controller.panelShrinkSpeed);
            }

            currentPanelType = newPanel;

            if (currentPanelType == PanelType.Main)
            {
                PanelLookup[currentPanelType].FadeTo(1, controller.mainPanelFadeTime);
                yield return new WaitForSeconds(controller.mainPanelFadeTime);
            }
            else
            {
                PanelLookup[newPanel].BringIn(controller.panelGrowSpeed, controller.panelGrowEaseType, controller.panelFadeTime);
                yield return new WaitForSeconds(controller.panelGrowSpeed);
            }

            //Enable Interaction on final panel
            PanelLookup[currentPanelType].EnableInteraction(true);

        }

        public void ToExitState(string message="")
        {
            controller.ChangeState(MainMenuStateController.MainMenuState.Exit);
        }
        #endregion
    }
}
