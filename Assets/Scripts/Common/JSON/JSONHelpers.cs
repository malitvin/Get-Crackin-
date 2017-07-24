//Unity
using UnityEngine;

//C#
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

namespace Common.JSON
{
    /// <summary>
    /// Help Serialize JSON data
    /// </summary>
    public static class JSONHelpers
    {
        [DllImport("__Internal")]
        private static extern void TextDownloader(string str, string fn);

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
        public static IEnumerator Load<T>(string fileLocation, Action<List<T>> listCall)
        {
            string json = "";

            if (fileLocation.Contains("://") || fileLocation.Contains(":///"))
            {
                WWW www = new WWW(fileLocation);
                yield return www;
                json = www.text;
            }
            else
            {
                json = File.ReadAllText(fileLocation);
            }

            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            listCall(wrapper.Items.ToList());

        }


        /// <summary>
        /// Save list to file location
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileLocation"></param>
        /// <param name="elements"></param>
        public static void SAVE<T>(string fileLocation,string fileName, T[] elements)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = elements;

            string json = JsonUtility.ToJson(wrapper, true);

            if (Application.platform == RuntimePlatform.WebGLPlayer) TextDownloader(fileLocation, fileName);
            else { File.WriteAllText(fileLocation, json); }
        }





    }
}
