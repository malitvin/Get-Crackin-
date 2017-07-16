//Unity
using UnityEngine;

//TMPRO
using TMPro;

//Game
using UI.Gameplay.Widgets;

//C#
using System.Collections;

namespace UI.Gameplay.Widgets.GameStatusWidget
{
    /// <summary>
    /// current game state display text
    /// </summary>
    public class CurrentStateText : GameplayWidget
    {
        public float transitionTime = 1f;
        private string tempText;

        private TextMeshProUGUI txt;
        private TextMeshProUGUI Txt
        {
            get { return txt ?? (txt = GetComponent<TextMeshProUGUI>()); }
        }

        public void ChangeText(string newText)
        {
            tempText = newText;
            StartCoroutine(ChangeText());
        }

        private IEnumerator ChangeText()
        {
            FadeTo(0, transitionTime / 2);
            yield return new WaitForSeconds(transitionTime / 2);
            Txt.text = tempText;
            FadeTo(1, transitionTime / 2);
        }
    }
}
