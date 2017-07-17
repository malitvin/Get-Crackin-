//Unity
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [CreateAssetMenu(menuName = "Audio/Audio File")]
    public class AudioFiles : ScriptableObject
    {

        /// <summary>
        /// Make Sure these enums match the audio files names
        /// </summary>
        public enum GameplaySoundClip {UserInput,DisplayInput,Correct,Incorrect,GameOver}
        public enum UISoundClip {CartoonPop}

       
        public class AudioBlueprint
        {
            public string name;
            public AudioClip clip;
            [Range(0, 1)]
            public float volume=1;
            [Range(-3, 3)]
            public float pitch = 1;
            public AudioMixerGroup group;
        }

        public GameplaySound[] gameplaySounds;
        public UISound[] UISounds;
    }

    [System.Serializable]
    public class GameplaySound : AudioFiles.AudioBlueprint { public AudioFiles.GameplaySoundClip key; }
    [System.Serializable]
    public class UISound : AudioFiles.AudioBlueprint { public AudioFiles.UISoundClip key; }
}
