//Unity
using UnityEngine;

//Game
using UI.MainMenu.Panels;

//C#
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

        public enum PanelType { Main, Play, HighScores, Credits, HowToPlay }
        

        private Dictionary<PanelType, MainMenuPanel> PanelLookup;

        public PanelType currentPanelType = PanelType.Main;

        #region Unity Methods
        public void Begin()
        {
            GeneratePanels();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.X)) controller.StartCoroutine(Transition(PanelType.Play));
            if (Input.GetKeyDown(KeyCode.Z)) controller.StartCoroutine(Transition(PanelType.Main));
        }

        public void End()
        {

        }
        #endregion


        #region UI Methods
        private void GeneratePanels()
        {
            PanelLookup = new Dictionary<PanelType, MainMenuPanel>();
            PanelLookup.Add(PanelType.Main, controller._MainCanvas.GetComponentInChildren<MainMenuPanel>()); //add current main menu panel
            int panelCount = controller.mainMenuBlueprint.panels.Length;
            
            for (int i = 0; i < panelCount; i++)
            {
                MainMenuPanel panel = Object.Instantiate(controller.mainMenuBlueprint.panels[i].prefab, controller._MainCanvas.transform) as MainMenuPanel;
                panel.MakeVisible(false);
                PanelLookup.Add(controller.mainMenuBlueprint.panels[i].key, panel);
            }
        }
        public IEnumerator Transition(PanelType newPanel)
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

            if(currentPanelType == PanelType.Main)
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
        #endregion
    }
}
