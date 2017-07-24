//Unity
using UnityEngine;

//C#
using System;
using System.Collections;
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

        }
        #endregion

        /// <summary>
        /// Add a score to the system returns true if a high score
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public IEnumerator AddScore(string name, int score)
        {
            yield return highScoreCalculator.AddScore(name, score);
        }

        /// <summary>
        /// Gets a list of all high scores
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetHighScores(Action<List<HighScore>> callback)
        {
            List<HighScore> updated = null;
            yield return highScoreCalculator.GetUpdatedHighScores(value => { updated = value; });
            callback(updated);

        }

    }
}
