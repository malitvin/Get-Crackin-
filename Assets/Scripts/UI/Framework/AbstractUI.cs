//Unity
using UnityEngine;
using UnityEngine.UI;

//C#
using System;

//Project


namespace UI.Framework
{
    /// <summary>
    /// Base class for any UI elements
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AbstractUI : MonoBehaviour
    {
        #region Components

        /// <summary>
        /// What are the dimensions of the Canvas that our UI element is attached to?
        /// </summary>
        /// 
        private RectTransform canvasRect;
        protected RectTransform CanvasRect
        {
            get { return canvasRect ?? (canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>()); }
        }

        private CanvasGroup grid;
        protected CanvasGroup Grid
        {
            get { return grid ?? (grid = GetUIComponent(grid)); }
        }

        private RectTransform rect;
        public RectTransform Rect
        {
            get { return rect ?? (rect = GetUIComponent(rect)); }
        }

        private Button b;
        private Button B
        {
            get { return b ?? (b = GetComponent<Button>()); }
        }

        private Slider slider;
        protected Slider _Slider
        {
            get { return slider ?? (slider = GetComponent<Slider>()); }
        }

        private Toggle t;
        protected Toggle _Toggle
        {
            get { return t ?? (t = GetComponent<Toggle>()); }
        }

        private RawImage rawImage;
        protected RawImage _RawImage
        {
            get { return rawImage ?? (rawImage = GetUIComponent(rawImage)); }
        }

        private Image image;
        public Image _Image
        {
            get { return image ?? (image = GetUIComponent(image)); }
        }

        #endregion

        #region Unity Functions
        protected virtual void Awake()
        {
            if (B != null) B.onClick.AddListener(() => Button_OnSelected());
            if (_Toggle != null) _Toggle.onValueChanged.AddListener((on) => { Toggle_OnValueChanged(on); });
        }
        #endregion

        #region Init
        public virtual void Init()
        {

        }
        #endregion

        #region iTween Functions
        public virtual void FadeTo(float Alpha, float time = 0, iTween.EaseType easeType = iTween.EaseType.linear, float delay = 0)
        {
            EnableInteraction(Alpha > 0 ? true : false);

            iTween.ValueTo(gameObject, iTween.Hash(
                   "from", Grid.alpha,
                   "to", Alpha,
                   "time", time,
                   "easetype", easeType,
                   "oncomplete", (Action<object>)(newValue => { Fade_OnComplete(); }),
                   "oncompletetarget", gameObject,
                   "onUpdate", (Action<object>)(newValue => { Grid.alpha = (float)newValue; })
               ));

        }

        protected virtual void Fade_OnComplete()
        {

        }

        protected void RectAnchorTo(Vector2 anchor, float time = 0, iTween.EaseType easeType = iTween.EaseType.linear, iTween.LoopType looptype = iTween.LoopType.none)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                   "from", Rect.anchoredPosition,
                   "to", anchor,
                   "time", time,
                   "easetype", easeType,
                   "oncomplete", (Action<object>)(newValue => { }),
                   "looptype", looptype,
                   "oncompletetarget", gameObject,
                   "onUpdate", (Action<object>)(newValue => { Rect.anchoredPosition = (Vector2)newValue; })
               ));
        }

        public void RectSizeTo(Vector2 size, float time = 0, iTween.EaseType easeType = iTween.EaseType.linear, float delay = 0)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                   "from", Rect.sizeDelta,
                   "to", size,
                   "time", time,
                   "easetype", easeType,
                   "delay", delay,
                   "oncomplete", (Action<object>)(newValue => { }),
                   "oncompletetarget", gameObject,
                   "onUpdate", (Action<object>)(newValue => { Rect.sizeDelta = (Vector2)newValue; })
               ));
        }
        public void ScaleTo(float scale, float time = 0, iTween.EaseType easeType = iTween.EaseType.linear, float delay = 0)
        {
            iTween.ScaleTo(gameObject, iTween.Hash(
                               "scale", new Vector3(scale, scale, scale),
                               "time", time,
                               "easetype", easeType,
                               "oncomplete", (Action<object>)(newValue => { }),
                               "oncompletetarget", gameObject)
                               );
        }

        protected void MoveTo(Vector3 position, float duration, iTween.EaseType easeType = iTween.EaseType.linear, bool isLocal = true)
        {
            iTween.MoveTo(gameObject, iTween.Hash(
                    "islocal", isLocal,
                    "position", position,
                    "time", duration,
                    "easetype", easeType,
                    "oncomplete", (Action<object>)(newValue => { }),
                    "oncompletetarget", gameObject)
                    );
        }


        protected void SliderValueTo(float value, float time = 0, iTween.EaseType easeType = iTween.EaseType.linear, float delay = 0)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                   "from", _Slider.value,
                   "to", value,
                   "time", time,
                   "easetype", easeType,
                   "delay", delay,
                   "oncomplete", (Action<object>)(newValue => { }),
                   "oncompletetarget", gameObject,
                   "onUpdate", (Action<object>)(newValue => { _Slider.value = (float)newValue; })
               ));
        }
        #endregion

        #region Visibility & Interaction
        public virtual void MakeVisible(bool enable, bool affectInteraction = true)
        {
            Grid.alpha = enable ? 1 : 0;
            if (affectInteraction) EnableInteraction(enable);
        }

        public void EnableInteraction(bool enable)
        {
            Grid.blocksRaycasts = Grid.interactable = enable;
        }

        /// <summary>
        /// This has potential to be a button
        /// </summary>
        protected virtual void Button_OnSelected()
        {

        }

        /// <summary>
        /// Potential for a toggle
        /// </summary>
        /// <param name="isOn"></param>
        protected virtual void Toggle_OnValueChanged(bool isOn)
        {

        }
        #endregion

        #region Helpers
        /// <summary>
        /// Checks References to make sure it is in the unity scene
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Object"></param>
        private T GetUIComponent<T>(T Object) where T : Component
        {
            Object = GetComponent<T>();
            if (Object == null) Debug.LogError(typeof(T) + " is not attached to  " + gameObject.name);
            return Object;

        }

        public bool IsVIsible()
        {
            return (Grid.alpha > 0);
        }
        #endregion

        #region Setting
        public void SetRectAnchoredPosition(Vector2 v)
        {
            Rect.anchoredPosition = v;
        }
        public void SetDirectAlpha(float alpha)
        {
            Grid.alpha = alpha;
        }
        #endregion
    }
}
