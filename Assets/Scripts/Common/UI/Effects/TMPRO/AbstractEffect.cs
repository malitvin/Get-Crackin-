//Unity
using UnityEngine;

//TMPRO
using TMPro;

//C#
using System;

namespace Common.UI.Effects.TMPRO
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AbstractEffect : MonoBehaviour
    {
        public enum EffectType { Glow }
        private TextMeshProUGUI uiTEXT;
        private TextMeshProUGUI TextMeshUI
        {
            get { return uiTEXT ?? (uiTEXT = GetComponent<TextMeshProUGUI>()); }
        }

        public EffectType type;
        public float speed;
        public iTween.EaseType easeType;

        [Space(10)]
        public float value;

        #region Unity Methods
        private void Awake()
        {
            Init();
            StartEffects();
        }

        private void Update()
        {

        }
        #endregion

        private void Init()
        {
            TextMeshUI.fontSharedMaterial.SetFloat("_GlowOffset", 0.6f);
        }

        private void StartEffects()
        {
            switch (type)
            {
                case EffectType.Glow:
                    Glow(value);
                    break;
            }
        }

        #region ITween Methods
        private void Glow(float value)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                  "from", TextMeshUI.fontSharedMaterial.GetFloat("_GlowOffset"),
                  "to", value,
                  "time", speed,
                  "easetype", easeType,
                  "looptype", iTween.LoopType.pingPong,
                  "oncomplete", (Action<object>)(newValue => { }),
                  "oncompletetarget", gameObject,
                  "onUpdate", (Action<object>)(newValue => { TextMeshUI.fontSharedMaterial.SetFloat("_GlowOffset", (float)newValue); })
              ));
        }
        #endregion


    }
}
