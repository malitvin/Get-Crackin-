//Unity
using UnityEngine;
using UnityEngine.Events;

//Project

//C#
using System;
using System.Collections.Generic;

namespace UI.Framework
{
    /// <summary>
    /// Observer Pattern in a way
    /// http://gameprogrammingpatterns.com/observer.html
    /// </summary>
    public class UIObserver : MonoBehaviour
    {

        private Dictionary<UIEvents.Type, Action<string>> eventDictionary;

        /// <summary>
        /// For Performance to avoid boxing
        /// </summary>
        public struct MyEnumComparer : IEqualityComparer<UIEvents.Type>
        {
            public bool Equals(UIEvents.Type x, UIEvents.Type y)
            {
                return x == y;
            }

            public int GetHashCode(UIEvents.Type obj)
            {
                return (int)obj;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<UIEvents.Type, Action<string>>(new MyEnumComparer());
            }
        }

        public void StartListening(UIEvents.Type eventName, Action<string> listener)
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

        public void StopListening(UIEvents.Type eventName, Action<string> listener)
        {
            if (eventDictionary == null) Init();

            Action<string> thisEvent = null;
            if (eventDictionary.ContainsKey(eventName))
            {
                thisEvent = eventDictionary[eventName];
                thisEvent -= listener;
            }
        }

        public void TriggerEvent(UIEvents.Type eventName,string message="")
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
            if(eventDictionary != null) eventDictionary.Clear();
            eventDictionary = null;
        }

    }
}
