//Unity
using UnityEngine;

//C#
using System;

namespace Audio.Background
{
    /// <summary>
    /// Basic background audio control, add anything you want to it
    /// Don't see anything happening to it besides volume
    /// </summary>
    public class BackgroundAudio : MonoBehaviour
    {
        [Range(0.5f,5)]

        public float volumeFadeTime = 2;
        private float idealVolume;

        private AudioSource a;
        private AudioSource _Audio
        {
            get { return a ?? (a = GetComponent<AudioSource>()); }
        }

        private void Awake()
        {
            idealVolume = _Audio.volume;
            _Audio.volume = 0;
            FadeVolume(false);
        }

        public void FadeVolume(bool mute)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                 "from", _Audio.volume,
                 "to", mute ? 0 : idealVolume,
                 "time", volumeFadeTime,
                 "easetype", iTween.EaseType.linear,
                 "oncomplete", (Action<object>)(newValue => { }),
                 "oncompletetarget", gameObject,
                 "onUpdate", (Action<object>)(newValue => { _Audio.volume = (float)newValue; })
             ));

        }
    }
}
