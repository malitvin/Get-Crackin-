//Unity
using UnityEngine;

//Game
using Common.Extensions;
using UI.Framework;

//C#
using System;

namespace UI.Gameplay.Widgets.GameStatusWidget
{
    /// <summary>
    /// The gameplay widget with the current status of the game
    /// </summary>
    public class GameStatusWidget : GameplayWidget
    {
        private CurrentStateText currentStateText;
        private CurrentStateText _CurrentStateText
        {
            get { return currentStateText ?? (currentStateText = GetComponentInChildren<CurrentStateText>()); }
        }

        private NumberFeedback numberFeedback;
        private NumberFeedback _NumberFeedback
        {
            get { return numberFeedback ?? (numberFeedback = GetComponentInChildren<NumberFeedback>()); }
        }

        private ScoreText scoreText;
        private ScoreText _ScoreText
        {
            get { return scoreText ?? (scoreText = GetComponentInChildren<ScoreText>()); }
        }

        protected override void Awake()
        {
            base.Awake();
            _CurrentStateText.MakeVisible(false);
            CreateListeners();
        }

        protected void CreateListeners()
        {
            Action<string> updateDisplayText = new Action<string>(ChangeDisplayText);
            StartListenting(UIEvents.Type.ChangeGameStatusText, updateDisplayText);

            Action<string> spawnAnimationNumber = new Action<string>(SpawnAnimationNumber);
            StartListenting(UIEvents.Type.SpawnAnimNumber, spawnAnimationNumber);

            Action<string> updateScoreText = new Action<string>(UpdateScoreText);
            StartListenting(UIEvents.Type.UpdateScoreText, updateScoreText);
        }

        private void ChangeDisplayText(string message)
        {
            _CurrentStateText.ChangeText(message);
        }

        private void UpdateScoreText(string score)
        {
            int n = score.ParseIntFast();
            _ScoreText.UpdateText(n);
        }

        private void SpawnAnimationNumber(string number)
        {
            int n = number.ParseIntFast();
            _NumberFeedback.SpawnNumber(n);
        }
    }
}
