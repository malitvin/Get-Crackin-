//Unity
using UnityEngine;

//C#
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//Game
using Common.JSON;

namespace Database
{
    public class HighScoresCalculator
    {
        private const string FILE_NAME = "HighScores.json";

        private List<HighScore> updatedHighScores;

        public IEnumerator GetUpdatedHighScores(Action<List<HighScore>> callback)
        {
            yield return REFRESH_HighScores();
            callback(updatedHighScores);
        }

        /// <summary>
        /// Refresh our high score list
        /// </summary>
        private IEnumerator REFRESH_HighScores()
        {
            yield return JSONHelpers.Load<HighScore>(GetFileLocation(), value => { updatedHighScores = value; });
            updatedHighScores = updatedHighScores.OrderByDescending(x => x.score).ToList();
        }

        private void SaveHighScores()
        {
            JSONHelpers.SAVE(GetFileLocation(),FILE_NAME,updatedHighScores.ToArray());
        }

        public IEnumerator AddScore(string name,int score)
        {
            //make new high score and populate
            HighScore highScore = new HighScore(name,score);

            yield return REFRESH_HighScores();

            if(updatedHighScores == null)
            {
                //This is the first person that has played
                updatedHighScores = new List<HighScore>();
            }

            //add it to list
            updatedHighScores.Add(highScore);

            //save and return true
            SaveHighScores();

        }


        private string GetFileLocation()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor) return Application.dataPath;
            else if (Application.platform == RuntimePlatform.WindowsPlayer) return Application.dataPath;
            else if (Application.platform == RuntimePlatform.WebGLPlayer) return "http://www.maxim-litvinov.com/Games/GetCrackin/Build/HighScores.json";
            else
            {
                Debug.LogError("Currently this Game only has high score support for Windows and WebGL");
                return "";
            }
        }

    }
}
