//Unity
using UnityEngine;
using UnityEngine.Audio;

//C#
using System;
using System.Collections.Generic;

/// <summary>
/// This class was taken and modified from the C# Programming Unity Cookbook by Jeff Murray
/// https://www.crcpress.com/C-Game-Programming-Cookbook-for-Unity-3D/Murray/p/book/9781466581401
/// </summary>
/// 
namespace Audio
{
    /// <summary>
    /// Object that controls and audio source
    /// </summary>
    public class SoundObject
    {
        public AudioSource source;
        public GameObject sourceGO;
        public Transform sourceTR;
        public AudioClip clip;

        public SoundObject(AudioFiles.AudioBlueprint blueprint)
        {
            //in this (the constructor) we create a new audio source and store the details of the sound itself
            sourceGO = new GameObject("AudioSource: " + blueprint.name);
            sourceGO.transform.SetParent(UnityEngine.Object.FindObjectOfType<BaseSoundController>().transform);
            sourceTR = sourceGO.transform;
            source = sourceGO.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.clip = blueprint.clip;
            source.volume = blueprint.volume;
            source.pitch = blueprint.pitch;
            if (blueprint.group != null) source.outputAudioMixerGroup = blueprint.group;
            clip = blueprint.clip;
        }

        //2D Sound
        public void PlaySound()
        {
            source.spatialBlend = 0.0f;
            source.PlayOneShot(clip);
        }

        //3D Sound
        public void PlaySound(Vector3 position)
        {
            source.spatialBlend = 1.0f;
            sourceTR.position = position;
            source.PlayOneShot(clip);
        }

    }

    /// <summary>
    /// Our Monobehaviour class to play sounds based on our Sounds Sound enum
    /// </summary>
    public class BaseSoundController : MonoBehaviour
    {
        public AudioFiles files;

        private SoundObject tempSoundObj;
        
        private Dictionary<AudioFiles.GameplaySoundClip, SoundObject> gameplaySoundLookup;
        private Dictionary<AudioFiles.UISoundClip, SoundObject> uiSoundLookup;

        void Awake()
        {
           
        }

        private void CreateLookups()
        {
            gameplaySoundLookup = new Dictionary<AudioFiles.GameplaySoundClip, SoundObject>();
            uiSoundLookup = new Dictionary<AudioFiles.UISoundClip, SoundObject>();

            foreach (AudioFiles.AudioBlueprint blueprint in files.gameplaySounds)
            {
                tempSoundObj = new SoundObject(blueprint);
                gameplaySoundLookup.Add((AudioFiles.GameplaySoundClip)Enum.Parse(typeof(AudioFiles.GameplaySoundClip), blueprint.clip.name), tempSoundObj);
            }
            foreach (AudioFiles.AudioBlueprint blueprint in files.UISounds)
            {
                tempSoundObj = new SoundObject(blueprint);
                uiSoundLookup.Add((AudioFiles.UISoundClip)Enum.Parse(typeof(AudioFiles.UISoundClip), blueprint.clip.name), tempSoundObj);
            }
        }

        public void PlayUISound(AudioFiles.UISoundClip sound)
        {
            if (uiSoundLookup == null) CreateLookups();
            if(!uiSoundLookup.ContainsKey(sound))
            {
                Debug.LogError("No Sound Found in Sound Lookup");
                return;
            }
            tempSoundObj = uiSoundLookup[sound];
            tempSoundObj.PlaySound();
        }

        //play sound 3D
        public void PlayGameplaySound(AudioFiles.GameplaySoundClip sound, Vector3 position)
        {
            if (gameplaySoundLookup == null) CreateLookups();
            if (!gameplaySoundLookup.ContainsKey(sound))
            {
                Debug.LogError("No Sound Found in Sound Lookup");
                return;
            }
            tempSoundObj = gameplaySoundLookup[sound];
            tempSoundObj.PlaySound(position);
        }
        

    }
}