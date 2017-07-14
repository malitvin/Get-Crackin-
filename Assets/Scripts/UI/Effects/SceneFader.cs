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
        #region Publics
        public bool visibleOnStart;
        public Color gradientColor = Color.white;

        [Space(10)]
        public float transitionSpeed = 2;
        #endregion

        #region Privates
        private Color visibleGradient;
        private Color invisibleGradient;
        #endregion

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

            CreateSceneTransitionEvent();
        }

        private void CreateSceneTransitionEvent()
        {
            Action<string> sceneInListen = new Action<string>(SceneIn);
            _UIObserver.StartListening(UIEvents.Type.SceneComeIn, sceneInListen);

            Action<string> sceneOutListen = new Action<string>(SceneOut);
            _UIObserver.StartListening(UIEvents.Type.SceneComeOut, sceneOutListen);

        }


        private void SceneIn(string message)
        {
            StartCoroutine(Transition(false));
        }

        private void SceneOut(string message)
        {
            StartCoroutine(Transition(true));
        }

        private IEnumerator Transition(bool visible)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                "from", _RawImage.material.GetColor("_BottomColor"),
                "to", visible ? visibleGradient : invisibleGradient,
                "time", transitionSpeed/2.0f,
                "easetype", iTween.EaseType.linear,
                "oncomplete", (Action<object>)(newValue => { }),
                "oncompletetarget", gameObject,
                "onUpdate", (Action<object>)(newValue => { _RawImage.material.SetColor("_BottomColor", (Color)newValue); })
            ));
            yield return new WaitForSeconds(transitionSpeed/2.0f);
            iTween.ValueTo(gameObject, iTween.Hash(
                "from", _RawImage.material.GetColor("_TopColor"),
                "to", visible ? visibleGradient : invisibleGradient,
                "time", transitionSpeed/2.0f,
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
