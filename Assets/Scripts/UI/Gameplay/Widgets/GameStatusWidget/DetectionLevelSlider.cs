//Unity
using UnityEngine;
using UnityEngine.UI;

//Game
using UI.Framework;
using Common.Extensions;

//C#
using System;

namespace UI.Gameplay.Widgets.GameStatusWidget
{
    /// <summary>
    /// Detection level slider, used for feedback, has animations
    /// </summary>
    public class DetectionLevelSlider : GameplayWidget
    {
        public float animTime = 2f;
        public iTween.EaseType animEaseType = iTween.EaseType.easeOutElastic;

        [Space(10)]
        public Color lowDetection;
        public Color mediumDetection;
        public Color highDetection;

        private Image fill;
        private Image Fill
        {
            get { return fill ?? (fill = _Slider.fillRect.GetComponent<Image>()); }
        }

        protected override void Awake()
        {
            base.Awake();
            SetUpActions();
        }

        private void SetUpActions()
        {
            Action<string> updateSlider = new Action<string>(UpdateSlider);
            StartListenting(UIEvents.Type.UpdateDetectionSlider, updateSlider);
        }

        private void UpdateSlider(string value)
        {
            float newValue = value.FloatParse(); 
            SliderValueTo(newValue, animTime, animEaseType);
            FillColorChange(DetermineDetectionColor(newValue));
        }

        private void FillColorChange(Color color)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                   "from", Fill.color,
                   "to", color,
                   "time", animTime,
                   "easetype", animEaseType,
                   "oncompletetarget", gameObject,
                   "onUpdate", (Action<object>)(newValue => { Fill.color = (Color)newValue; })
               ));
        }

        private Color DetermineDetectionColor(float value)
        {
            if (value < 25)
            {
                return lowDetection;
            }
            else if (value > 25 && value < 75)
            {
                return mediumDetection;
            }
            else
            {
                return highDetection;
            }
        }

    }
}
