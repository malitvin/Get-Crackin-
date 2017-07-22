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
        private Animation anim;
        private Animation _Animation
        {
            get { return anim ?? (anim = GetComponent<Animation>()); }
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
            _Animation.Play();
        }
    }
}
