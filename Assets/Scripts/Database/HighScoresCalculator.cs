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

        private List<dreamloLeaderBoard.Score> updatedHighScores;

        private dreamloLeaderBoard dreamLo;
        private dreamloLeaderBoard _DreamLoLeaderboard
        {
            get { return dreamLo ?? (dreamLo = UnityEngine.Object.FindObjectOfType<dreamloLeaderBoard>()); }
        }

        public IEnumerator GetUpdatedHighScores(Action<List<dreamloLeaderBoard.Score>> callback)
        {
            yield return _DreamLoLeaderboard.GetScores();
            callback(_DreamLoLeaderboard.ToListHighToLow());
        }

        public void AddScore(string name,int score)
        {
            _DreamLoLeaderboard.AddScore(name, score);
        }

        public void RemoveScore(string name)
        {
            _DreamLoLeaderboard.RemoveScore(name);
        }

    }
}
