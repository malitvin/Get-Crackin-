//Unity
using UnityEngine;
using UnityEngine.UI;

//TMPRO
using TMPro;

//Game
using Common.Extensions;
using UI.Framework;
using UI.Gameplay.Events;
using UI.Gameplay.Widgets;

//Managers
using Managers;


//C#
using System;

namespace UI.Gameplay.Panels
{
    public class GameOverPanel : GameplayWidget
    {
        public enum Options { Replay, Quit }

        public Image header;

        private TextMeshProUGUI txt;
        private TextMeshProUGUI HeaderText
        {
            get { return txt ?? (txt = header.GetComponentInChildren<TextMeshProUGUI>()); }
        }

        private UIWidget buttonGroup;
        private UIWidget _ButtonGrid
        {
            get { return buttonGroup ?? (buttonGroup = GetComponentInChildren<HorizontalLayoutGroup>().GetComponent<UIWidget>()); }
        }

        private HighScoreInput highScoreInput;
        private HighScoreInput _HighScoreInput
        {
            get { return highScoreInput ?? (highScoreInput = GetComponentInChildren<HighScoreInput>()); }
        }


        [Space(10)]
        public string loseText;
        public Sprite loseSprite;

        [Space(10)]
        public string winText;
        public Sprite winSprite;

        [Space(5)]

        public float fadeInTime = 2;
        public float fadeOutTime = 1;

        protected override void Awake()
        {
            base.Awake();
            MakeVisible(false);
            SetUpEvents();
        }

        protected void SetUpEvents()
        {
            //listen for have we won or lost the game?
            Action<string> listenForHeaderPrep = new Action<string>(SetHeader);
            StartListenting(UIEvents.Type.PrepareWinLoseUpdate, listenForHeaderPrep);

            //listen for prepare/achieving high score panel
            Action<string> ListenForHighScore = new Action<string>(ListenerAchievedHighScore);
            StartListenting(UIEvents.Type.PrepareHighScoreNumber, ListenForHighScore);

            //listen for toggling game panel
            Action<String> ToggleGameOverPanel = new Action<string>(ListenerDisplay);
            StartListenting(UIEvents.Type.ToggleGameOverPanel, ToggleGameOverPanel);

            
        }

        private void SetHeader(string message)
        {
            bool won = message.BoolParse();
            header.sprite = won ? winSprite : loseSprite;
            HeaderText.text = won ? winText : loseText;
        }

        private void ListenerAchievedHighScore(string message)
        {
            bool achieveScore = message.BoolParse();
            //disbale continue grid if achieved high score
            EnableButtonGrid(achieveScore);
            //move button grid if achieved high score
            _ButtonGrid.SetRectAnchoredPosition(achieveScore ? new Vector2(0,30) : new Vector2(0,350));
            //enable high score input
            _HighScoreInput.MakeVisible(achieveScore);
        }

        private void ListenerDisplay(string message)
        {
            HUD.VisibleToggle toggle = (HUD.VisibleToggle)Enum.Parse(typeof(HUD.VisibleToggle), message);
            if (toggle == HUD.VisibleToggle.Display) Display();
            else
            {
                Hide();
            }
        }

        public void Display()
        {
            FadeTo(1, fadeInTime);
        }

        protected override void Fade_OnComplete()
        {
            base.Fade_OnComplete();
            if (Grid.alpha > 0) EnableInteraction(true);
        }

        public void Hide()
        {
            EnableInteraction(false);
            FadeTo(0, fadeOutTime);
        }

        public void GameOverButton_OnSelected(Options option)
        {
            if (option == Options.Quit) TriggerHUDEvent(UIEvents.Type.QuitButtonSelected);
            else TriggerHUDEvent(UIEvents.Type.ReplayButtonSelected);
        }

        private void EnableButtonGrid(bool enable)
        {
            _ButtonGrid.SetDirectAlpha((enable) ? 1 : 0.2f);
            _ButtonGrid.EnableInteraction(enable);
        }
    }
}
