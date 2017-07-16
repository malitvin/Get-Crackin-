//Game
using Common.Managers;
using Gameplay.Events;


//Audio
using Audio;

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


        public void TriggerGameplayEvent(GameplayEvent.Type type,string message ="")
        {
            _GameplayObserver.TriggerEvent(type, message);
        }

        public void StartListening(GameplayEvent.Type type,System.Action<string> listen)
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
    }
}
