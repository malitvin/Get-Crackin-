//Game
using Common.Managers;
using Gameplay.Events;
using Database;

//Audio
using Audio;

//C#
using System;
using System.Collections;

namespace Managers
{
    /// <summary>
    /// GLOBAL Game Manager, only singleton in game
    /// Main job is to keep track on game only events
    /// </summary>
    public class GAMEManager : Singleton<GAMEManager>
    {

        private GameplayObserver gameplayObserver;
        private GameplayObserver _GameplayObserver
        {
            get { return gameplayObserver ?? (gameplayObserver = new GameplayObserver()); }
        }

        private BaseSoundController baseSoundController;
        private BaseSoundController _BaseSoundController
        {
            get { return baseSoundController ?? (baseSoundController = FindObjectOfType<BaseSoundController>()); }
        }

        private HighScoreController highScoreController;
        private HighScoreController _HighScoreController
        {
            get { return highScoreController ?? (highScoreController = FindObjectOfType<HighScoreController>()); }
        }


        public void TriggerGameplayEvent(GameplayEvent.Type type, string message = "")
        {
            _GameplayObserver.TriggerEvent(type, message);
        }

        public void StartListening(GameplayEvent.Type type, Action<string> listen)
        {
            _GameplayObserver.StartListening(type, listen);
        }

        public void PlaySound(AudioFiles.UISoundClip clip)
        {
            _BaseSoundController.PlayUISound(clip);
        }
        public void PlaySound(AudioFiles.GameplaySoundClip clip)
        {
            _BaseSoundController.PlayGameplaySound(clip, new UnityEngine.Vector3(0, 0, 0));
        }

        public void AddHighScore(string name, int score)
        {

            _HighScoreController.AddScore(name, score);
        }

        public IEnumerator NameExistsInHighScores(string name, Action<bool> callback)
        {
            bool nameExists = false;
            yield return _HighScoreController.NameExists(name, value => { nameExists = value; });
            callback(nameExists);
        }

        public IEnumerator IsHighScore(int maxCount, int score, Action<bool> callback)
        {
            bool isHighScore = false;
            yield return _HighScoreController.IsHighScore(maxCount, score, value => { isHighScore = value; });
            callback(isHighScore);
        }
    }
}
