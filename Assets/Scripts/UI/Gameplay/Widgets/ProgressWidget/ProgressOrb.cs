//Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

//Game
using UI.Framework;

//C#
using System;

namespace UI.Gameplay.Widgets.ProgressWidget
{
    public class ProgressOrb : AbstractUI
    {
        private UILineRenderer line;
        private UILineRenderer _Line
        {
            get { return line ?? (line = GetComponentInChildren<UILineRenderer>()); }
        }

        private UIWidget img;
        private UIWidget OrbImage
        {
            get { return img ?? (img = GetComponentInChildren<UIWidget>()); }
        }

        private Sprite tempChangeSprite;
        private float tempTransitionTime;

        #region Init
        public void InitLine(Vector3 pos, Color color)
        {
            _Line.color = color;
            _Line.Points[1] = pos;
            _Line.enabled = false; _Line.enabled = true;
        }

        public void InitOrb(Sprite img)
        {
            OrbImage._Image.sprite = img;
        }
        #endregion

        #region Helpers
        public Vector2 GetAnchoredPosition()
        {
            return Rect.anchoredPosition3D;
        }
        #endregion

        #region Gameplay
        public void Morph(Sprite sprite,Color lineColor,float transitionTime)
        {
            tempChangeSprite = sprite;
            tempTransitionTime = transitionTime / 2;
            FadeLine(lineColor);
            OrbImage.FadeTo(0, tempTransitionTime);           
        }

        protected override void Fade_OnComplete()
        {
            base.Fade_OnComplete();
            if (!OrbImage.IsVIsible())
            {
                OrbImage._Image.sprite = tempChangeSprite;
                OrbImage.FadeTo(1, tempTransitionTime);
            }
            
        }

        private void FadeLine(Color color)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                   "from", _Line.color,
                   "to", color,
                   "time", tempTransitionTime * 2,
                   "oncomplete", (Action<object>)(newValue => { Fade_OnComplete(); }),
                   "oncompletetarget", gameObject,
                   "onUpdate", (Action<object>)(newValue => { _Line.color = (Color)newValue; })
               ));
        }
        #endregion
    }
}
