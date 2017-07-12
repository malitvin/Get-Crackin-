//Unity
using UnityEngine;

namespace Common.Managers
{

    /// <summary>
    /// This approach ensures that only one instance is created and only when the instance is needed. 
    /// Also, the variable is declared to be volatile to ensure that assignment to the instance variable completes before the instance variable can be accessed. 
    /// Lastly, this approach uses a syncRoot instance to lock on, rather than locking on the type itself, to avoid deadlocks.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    //The following class will make any class that inherits from it a singleton automatically
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static volatile T instance;
        private static object syncRoot = new System.Object();

        //Returns the instance of this singleton.
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = (T)FindObjectOfType(typeof(T));

                        if (instance == null)
                        {
                            Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none!");
                        }
                    }
                }

                return instance;
            }
        }
    }
}
