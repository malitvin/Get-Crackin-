//Unity
using UnityEngine;

//Game

//C#
using System;
using System.Collections.Generic;

namespace Gameplay.Events
{
    /// <summary>
    /// Acts as a game manager / gameplay observer for gameplay only specific event
    /// </summary>
    public class GameplayObserver 
    {
        private Dictionary<GameplayEvent.Type, Action<string>> eventDictionary;

        /// <summary>
        /// For Performance to avoid boxing
        /// </summary>
        public struct MyEnumComparer : IEqualityComparer<GameplayEvent.Type>
        {
            public bool Equals(GameplayEvent.Type x, GameplayEvent.Type y)
            {
                return x == y;
            }

            public int GetHashCode(GameplayEvent.Type obj)
            {
                return (int)obj;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<GameplayEvent.Type, Action<string>>(new MyEnumComparer());
            }
        }

        public void StartListening(GameplayEvent.Type eventName, Action<string> listener)
        {
            if (eventDictionary == null) Init();

            Action<string> thisEvent = null;
            if (eventDictionary.ContainsKey(eventName))
            {
                thisEvent = eventDictionary[eventName];
                thisEvent += listener;
            }
            else
            {
                thisEvent += listener;
                eventDictionary.Add(eventName, thisEvent);
            }
        }

        public void StopListening(GameplayEvent.Type eventName, Action<string> listener)
        {
            if (eventDictionary == null) Init();

            Action<string> thisEvent = null;
            if (eventDictionary.ContainsKey(eventName))
            {
                thisEvent = eventDictionary[eventName];
                thisEvent -= listener;
            }
        }

        public void TriggerEvent(GameplayEvent.Type eventName, string message = "")
        {
            if (eventDictionary == null) Init();

            Action<string> thisEvent = null;

            if (eventDictionary.ContainsKey(eventName))
            {
                thisEvent = eventDictionary[eventName];
                thisEvent(message);
            }
        }

        private void OnDestroy()
        {
            if (eventDictionary != null) eventDictionary.Clear();
            eventDictionary = null;
        }
    }
}
