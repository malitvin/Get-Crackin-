//Unity
using UnityEngine;
using UnityEngine.UI;

//TMPRO
using TMPro;

//Game
using UI.Gameplay.Widgets;

//Pooler
using Common.Pooler;

namespace UI.Gameplay.Widgets.CombinationWidget
{
    /// <summary>
    /// Combination Orb as Displayed on screen
    /// </summary>
    public class CombinationOrb : PooledObject
    {
        private Image img;
        private Image _Img
        {
            get { return img ?? (img = GetComponent<Image>()); }
        }

        private TextMeshProUGUI txt;
        private TextMeshProUGUI _Txt
        {
            get { return txt ?? (txt = GetComponentInChildren<TextMeshProUGUI>()); }
        }

        private GameplayWidget thisUI;
        private GameplayWidget _ThisUI
        {
            get { return thisUI ?? (thisUI = GetComponent<GameplayWidget>()); }
        }

        public override void Show()
        {
            base.Show();
            _ThisUI.MakeVisible(false);
        }

        public void Init(Color color,int number)
        {
            _ThisUI.FadeTo(1, 0.5f);
            _Img.color = color;
            _Txt.text = number.ToString();
        }
    }
}
