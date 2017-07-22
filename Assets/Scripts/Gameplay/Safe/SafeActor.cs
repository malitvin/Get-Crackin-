//Unity
using UnityEngine;

//Managers
using Managers;

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
            Action<string> openSafeListen = new Action<string>(OpenSafe);
            GAMEManager.Instance.StartListening(Events.GameplayEvent.Type.OpenSafe, openSafeListen);

        }

        private void OpenSafe(string message)
        {
            _Anim.SetTrigger(1);
        }
    }
}
