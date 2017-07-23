//Unity
using UnityEngine;

//C#
using System.Collections.Generic;

//Game
using Common.JSON;

namespace Database
{
    public class HighScoresCalculator
    {
        private List<HighScore> updatedHighScores;

        public List<HighScore> GetUpdatedHighScores()
        {
            REFRESH_HighScores();
            return updatedHighScores;
        }

        /// <summary>
        /// Refresh our high score list
        /// </summary>
        private void REFRESH_HighScores()
        {
            updatedHighScores = JSONHelpers.Load<HighScore>(GetFileLocation());
         }

        private void SaveHighScores()
        {
            JSONHelpers.SAVE(GetFileLocation(), updatedHighScores.ToArray());
        }

        public bool AttemptToAddScore(string name,int score)
        {
            //make new high score and populate
            HighScore highScore = new HighScore(name,score);

            REFRESH_HighScores();

            if(updatedHighScores == null)
            {
                //This is the first person that has played
                updatedHighScores = new List<HighScore>();
                return true;
            }

            //add it to list
            updatedHighScores.Add(highScore);

            //save and return true
            SaveHighScores();

            return false;
        }


        private string GetFileLocation()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor) return Application.dataPath + "/HighScores.json";
            else if (Application.platform == RuntimePlatform.WindowsPlayer) return Application.dataPath + "/HighScores.json";
            else if (Application.platform == RuntimePlatform.WebGLPlayer) return "http://www.maxim-litvinov.com/Games/GetCrackin/Build/HighScores.json";
            else
            {
                Debug.LogError("Currently this Game only has high score support for Windows and WebGL");
                return "";
            }
        }

    }
}
