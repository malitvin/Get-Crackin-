//Unity
using UnityEngine;

//C#
using System.Collections.Generic;

namespace Cameras
{
    public class GameplayCamera : MonoBehaviour
    {
        public enum LocationKey { Main, Win }

        [System.Serializable]
        public struct Location
        {
            public string description;
            public LocationKey key;
            public Transform point;
            [Range(0.5f,3)]
            public float animationTime;
            public iTween.EaseType animEaseType;
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
    }
}
