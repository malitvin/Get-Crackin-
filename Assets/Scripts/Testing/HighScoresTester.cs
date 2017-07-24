//Unity
using UnityEngine;

//Data
using Database;

//C#
using System.Collections;
using System.Collections.Generic;

namespace Testing
{
    public class HighScoresTester : MonoBehaviour
    {
        private HighScoreController highScoreController;
        private HighScoreController _HighScoreController
        {
            get { return highScoreController ?? (highScoreController = FindObjectOfType<HighScoreController>()); }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(GetHighScores());
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(_HighScoreController.AddScore("Test Score", Random.Range(0, 250)));
            }
        }


        private IEnumerator GetHighScores()
        {
            List<HighScore> highScores = null;
            yield return StartCoroutine(_HighScoreController.GetHighScores(value => { highScores = value; }));
            for (int i = 0; i < highScores.Count; i++)
            {
                Debug.Log(highScores[i].name + "," + highScores[i].score);
            }
        }
    }
}
