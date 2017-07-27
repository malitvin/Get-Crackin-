//Unity
using UnityEngine;

//TMPRO
using TMPro;

//Game
using UI.Framework;

namespace UI.MainMenu.Panels.HighScores
{
    /// <summary>
    /// high score display spawn text
    /// </summary>
    public class HighScoreText : UIWidget
    {
        public TextMeshProUGUI nameTxt;
        public TextMeshProUGUI scoreTxt;

        public void SetText(string firstname,int score)
        {
            nameTxt.text = firstname;
            scoreTxt.text = score.ToString();
        }

    }
}
