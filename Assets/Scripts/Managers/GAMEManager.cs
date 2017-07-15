//Game
using Common.Managers;
using Gameplay.Events;

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


        public void TriggerGameplayEvent(GameplayEvent.Type type,string message ="")
        {
            _GameplayObserver.TriggerEvent(type, message);
        }

        public void StartListening(GameplayEvent.Type type,System.Action<string> listen)
        {
            _GameplayObserver.StartListening(type, listen);
        }
    }
}
