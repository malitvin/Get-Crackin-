//Unity
using UnityEngine;

//Managers
using Managers;

//Game
using Gameplay.Events;

//C#
using System;

namespace Gameplay.Safe
{
    /// <summary>
    /// The only real important 3D actor in a 2d game :(
    /// </summary>
    public class SafeActor : MonoBehaviour
    {
        private Animator anim;
        private Animator _Anim
        {
            get { return anim ?? (anim = GetComponent<Animator>()); }
        }

        private void Awake()
        {
            SetUpEvents();
        }

        private void SetUpEvents()
        {
            //listen open safe
            Action<string> openSafeListen = new Action<string>(OpenSafe);
            GAMEManager.Instance.StartListening(GameplayEvent.Type.OpenSafe, openSafeListen);

            //listen close sage
            Action<string> closeSafeListen = new Action<string>(CloseSafe);
            GAMEManager.Instance.StartListening(GameplayEvent.Type.CloseSafe, closeSafeListen);

        }

        private void OpenSafe(string message)
        {
            _Anim.SetTrigger("Open");
        }
        private void CloseSafe(string message)
        {
            _Anim.SetTrigger("Close");
        }
    }
}
