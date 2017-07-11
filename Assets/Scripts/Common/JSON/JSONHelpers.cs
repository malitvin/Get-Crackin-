//Unity
using UnityEngine;

//C#
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Common.JSON
{
    /// <summary>
    /// Help Serialize JSON data
    /// </summary>
    public static class JSONHelpers
    {
        /// <summary>
        /// Wrapper for Array of Items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }

        /// <summary>
        /// Load from file location
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileLocation"></param>
        /// <returns></returns>
        public static List<T> Load<T>(string fileLocation)
        {
            if (!File.Exists(fileLocation))
            {
                File.WriteAllText(fileLocation, "");
            }
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
                return wrapper.Items.ToList();
            }

        }

        /// <summary>
        /// Save list to file location
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileLocation"></param>
        /// <param name="elements"></param>
        public static void SAVE<T>(string fileLocation,T[] elements)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = elements;
            
            string json = JsonUtility.ToJson(wrapper,true);
            File.WriteAllText(fileLocation, json);
        }





    }
}
