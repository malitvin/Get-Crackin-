//Unity
using UnityEngine;

//TMPRO
using TMPro;


//Game
using Common.Pooler;

//C#
using System.Collections.Generic;

namespace UI.Gameplay.Widgets.GameStatusWidget
{
    /// <summary>
    /// Score text that displays
    /// </summary>
    public class ScoreText : GameplayWidget
    {
        public Color flashColor;
        public float textAnimationTime = 2f;
        public iTween.EaseType textAnimEaseType = iTween.EaseType.linear;
        private Color normalColor;

        private TextMeshProUGUI txt;
        protected TextMeshProUGUI _Txt
        {
            get { return txt ?? (txt = GetComponentInChildren<TextMeshProUGUI>()); }
        }

        private int totalScore = 0;

        protected override void Awake()
        {
            base.Awake();
            normalColor = _Txt.color;
        }


        public void UpdateText(int newScore)
        {
            TxtAnim(newScore);
        }


        /// <summary>
        /// Text Animation
        /// </summary>
        private void TxtAnim(int newScore)
        {
            _Txt.color = flashColor;
            //text animation
            iTween.ValueTo(gameObject, iTween.Hash(
              "from", totalScore,
              "to", newScore,
              "time", textAnimationTime,
              "easetype", textAnimEaseType,
              "onupdate", "TextOnUpdate"));

            //text color animation
            iTween.ValueTo(gameObject, iTween.Hash(
              "from", _Txt.color,
              "to", normalColor,
              "time", textAnimationTime,
              "easetype", textAnimEaseType,
              "delay",textAnimationTime/2,
              "onUpdate", (System.Action<object>)(newValue => { _Txt.color = (Color)newValue; })));
        }

        private void TextOnUpdate(int newValue)
        {
            totalScore = newValue;
            _Txt.text = totalScore.ToString();
        }
    }
}
