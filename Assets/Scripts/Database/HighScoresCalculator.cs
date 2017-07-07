//Unity
using UnityEngine;

//C#
using System.Linq;
using System.Collections.Generic;

//Game
using Common.JSON;

namespace Database
{
    public class HighScoresCalculator
    {
        private List<HighScore> updatedHighScores;


        private void UpdateHighScores()
        {
            updatedHighScores = JSONHelpers.Load<HighScore>(GetFileLocation());
         }

        private void SaveHighScores()
        {
            JSONHelpers.SAVE(GetFileLocation(), updatedHighScores.ToArray());
        }

        public bool AttemptToAddScore(string name,int score)
        {
            UpdateHighScores();
            if(updatedHighScores == null)
            {
                //This is the first person that has played
                updatedHighScores = new List<HighScore>();
                HighScore highScore = new HighScore();
                highScore.name = name;
                highScore.score = score;
                updatedHighScores.Add(highScore);

                SaveHighScores();

                return true;
            }
            else
            {
                
            }
            return false;
        }


        private string GetFileLocation()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor) return Application.dataPath + "/HighScores.json";
            else if (Application.platform == RuntimePlatform.WindowsPlayer) return Application.dataPath + "/HighScores.json";
            else if (Application.platform == RuntimePlatform.WebGLPlayer) return "";
            else
            {
                Debug.LogError("Currently this Game only has high score support for Windows and WebGL");
                return "";
            }
        }

    }
}
