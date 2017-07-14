//Unity
using UnityEngine;

//Game
using UI.Framework;

//C#
using System;
using System.Collections;

namespace UI.Effects
{
    /// <summary>
    /// Our scene fader with one material instance
    /// </summary>
    public class SceneFader : AbstractUI
    {
        public bool visibleOnStart;
        public Color gradientColor = Color.white;

        private Color visibleGradient;
        private Color invisibleGradient;

        /// <summary>
        /// This will be in both scenes
        /// </summary>
        private UIObserver uiObserver;
        private UIObserver _UIObserver
        {
            get { return uiObserver ?? (uiObserver = FindObjectOfType<UIObserver>()); }
        }


        protected override void Awake()
        {
            base.Awake();

            visibleGradient = new Color(gradientColor.r, gradientColor.g, gradientColor.b, 1);
            invisibleGradient = new Color(gradientColor.r, gradientColor.g, gradientColor.b, 0);

            MakeVisible(visibleOnStart);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X)) StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                "from", _RawImage.material.GetColor("_BottomColor"),
                "to", visibleGradient,
                "time", 1,
                "easetype", iTween.EaseType.linear,
                "oncomplete", (Action<object>)(newValue => {}),
                "oncompletetarget", gameObject,
                "onUpdate", (Action<object>)(newValue => { _RawImage.material.SetColor("_BottomColor", (Color)newValue); })
            ));
            yield return new WaitForSeconds(1);
            iTween.ValueTo(gameObject, iTween.Hash(
                "from", _RawImage.material.GetColor("_TopColor"),
                "to", visibleGradient,
                "time", 1,
                "easetype", iTween.EaseType.linear,
                "oncomplete", (Action<object>)(newValue => { }),
                "oncompletetarget", gameObject,
                "onUpdate", (Action<object>)(newValue => { _RawImage.material.SetColor("_TopColor", (Color)newValue); })
            ));
        }


        private void MakeVisible(bool visible)
        {
            _RawImage.material.SetColor("_TopColor", visible ? visibleGradient : invisibleGradient);
            _RawImage.material.SetColor("_BottomColor", visible ? visibleGradient : invisibleGradient);
        }


        private void OnDestroy()
        {
            MakeVisible(false);
        }
    }
}
