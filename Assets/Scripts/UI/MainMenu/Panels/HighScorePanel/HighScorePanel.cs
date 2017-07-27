//Unity
using UnityEngine;

//Game
using Database;
using Common.Attributes;

//C#
using System.Collections;
using System.Collections.Generic;

namespace UI.MainMenu.Panels.HighScores
{
    /// <summary>
    /// Panel that displays high scores
    /// </summary>
    public class HighScorePanel : MainMenuPanel
    {
        public Transform container;

        [PrefabDropdown("UI/MainMenu/Spawnable")]
        public HighScoreText highScorePrefab;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(GenerateHighScores());
        }

        private IEnumerator GenerateHighScores()
        {
            List<dreamloLeaderBoard.Score> scores = null;
            yield return Controller.GetHighScores(value => { scores = value; });
            if (scores == null) Debug.LogError("Scores is null for some reason, check Database namespace");


            int count = scores.Count;
            Debug.Log(count);
            for(int i=0; i < scores.Count;i++)
            {
                HighScoreText text = Instantiate(highScorePrefab, container, false) as HighScoreText;
                text.SetText("<color=black>"+(i+1).ToString()+ ".</color>                 " + scores[i].playerName, scores[i].score);
            }

        }

    }
}
