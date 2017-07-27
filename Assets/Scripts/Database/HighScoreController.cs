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
        public void AddScore(string name, int score)
        {
            highScoreCalculator.AddScore(name, score);
        }

        public void RemoveScore(string name)
        {
            highScoreCalculator.RemoveScore(name);
        }

        /// <summary>
        /// Does this name exists in the list of high scores?
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IEnumerator NameExists(string name,Action<bool> callback)
        {
            bool exists = false;
            yield return highScoreCalculator.NameExists(name,value => { exists = value; });
            callback(exists);
        }

        /// <summary>
        /// Gets a list of all high scores
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetHighScores(Action<List<dreamloLeaderBoard.Score>> callback)
        {
            List<dreamloLeaderBoard.Score> updated = null;
            yield return highScoreCalculator.GetUpdatedHighScores(value => { updated = value; });
            callback(updated);

        }

        /// <summary>
        /// Is this a high score?
        /// </summary>
        /// <param name="maxCount"></param>
        /// <param name="score"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IEnumerator IsHighScore(int maxCount,int score,Action<bool> callback)
        {
            List<dreamloLeaderBoard.Score> updatedScores = null;
      
            yield return GetHighScores(value => { updatedScores = value; });
            if (updatedScores.Count < maxCount) callback(true);
            else
            {
                
                if (updatedScores[updatedScores.Count - 1].score < score)
                {
                    RemoveScore(updatedScores[updatedScores.Count - 1].playerName);
                    callback(true);
                    Debug.Log("High score achieved!");
                }
                else
                {
                    callback(false);
                }
            }
        }

    }
}
