//Unity
using UnityEngine;

//C#
using System.Collections.Generic;

namespace Database
{
    /// <summary>
    /// High Score Controller in scene
    /// </summary>
    public class HighScoreController : MonoBehaviour
    {
        private HighScoresCalculator highScoreCalculator = new HighScoresCalculator();

        #region Unity Methods
        private void Awake()
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X)) AddScore("Maxim Litvinov", Random.Range(0, 100));
        }
        #endregion

        /// <summary>
        /// Add a score to the system returns true if a high score
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool AddScore(string name, int score)
        {
            return highScoreCalculator.AttemptToAddScore(name,score);
        }

        /// <summary>
        /// Gets a list of all high scores
        /// </summary>
        /// <returns></returns>
        public List<HighScore> GetHighScores()
        {
            return highScoreCalculator.GetUpdatedHighScores();
        }

    }
}
