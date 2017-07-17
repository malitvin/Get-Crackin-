//Unity
using UnityEngine;

//C#
using System;
using System.Collections.Generic;

//Game
using Managers;
using Gameplay.Events;

namespace Cameras
{
    /// <summary>
    /// Gameplay camera controller basically
    /// </summary>
    public class GameplayCamera : MonoBehaviour
    {
        private Camera cam;
        private Camera Cam
        {
            get { return cam ?? (cam = GetComponentInChildren<Camera>()); }
        }

        public enum LocationKey { Main, Win,Lose,Start }

        [System.Serializable]
        public struct Location
        {
            public string description;
            public LocationKey key;
            public Transform point;
            [Range(0.5f,3)]
            public float animationTime;
            public iTween.EaseType animEaseType;

            public Vector3 GetPosition()
            {
                return point.localPosition;
            }

            public Vector3 GetEuler()
            {
                return point.localEulerAngles;
            }
        }

        private Dictionary<LocationKey, Location> Lookup;

        /// <summary>
        /// For Performance to avoid boxing
        /// </summary>
        public struct MyEnumComparer : IEqualityComparer<LocationKey>
        {
            public bool Equals(LocationKey x, LocationKey y)
            {
                return x == y;
            }

            public int GetHashCode(LocationKey obj)
            {
                return (int)obj;
            }
        }

        public Location[] locations;


        private void Awake()
        {
            Action<string> listenForCameraMovement = new Action<string>(ToLocation);
            GAMEManager.Instance.StartListening(GameplayEvent.Type.CameraChange, listenForCameraMovement);

        }

        private void CreateLookup()
        {
            Lookup = new Dictionary<LocationKey, Location>();
            for(int i=0; i <locations.Length;i++)
            {
               if(!Lookup.ContainsKey(locations[i].key)) Lookup.Add(locations[i].key, locations[i]);
            }
        }

        public Location GetLocation(LocationKey key)
        {
            if (Lookup == null) CreateLookup();
            return Lookup[key];
        }

        public void ToLocation(string message)
        {
            LocationKey key = (LocationKey)(Enum.Parse(typeof(LocationKey), message));
            AnimateTo(key);
        }

        private void AnimateTo(LocationKey key)
        {
            Location location = GetLocation(key);
            iTween.MoveTo(Cam.gameObject, iTween.Hash(
                  "islocal", true,
                  "position", location.GetPosition(),
                  "time", location.animationTime,
                  "easetype", location.animEaseType
                  ));
            iTween.RotateTo(Cam.gameObject, iTween.Hash(
                  "islocal", true,
                  "rotation", location.GetEuler(),
                  "time", location.animationTime,
                  "easetype", location.animEaseType
                  ));
        }
    }
}
