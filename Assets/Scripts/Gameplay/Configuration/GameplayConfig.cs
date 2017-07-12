//Unity
using UnityEngine;

//Game
using Managers.GameSettings;

//C#
using System.Collections.Generic;

namespace Gameplay.Configuration
{
    /// <summary>
    /// A Game Configuration File
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/Gameplay Blueprint", fileName = "Config")]
    public class GameplayConfig : ScriptableObject
    {
        [System.Serializable]
        public struct GameplayBlueprint
        {
            public string name;
            public GameSettings.Difficulty difficulty;
            [Range(10, 50)]
            public int combinationCount;
        }

        private Dictionary<GameSettings.Difficulty, GameplayBlueprint> BlueprintLookup;

        /// <summary>
        /// For Performance to avoid boxing
        /// </summary>
        public struct MyEnumComparer : IEqualityComparer<GameSettings.Difficulty>
        {
            public bool Equals(GameSettings.Difficulty x, GameSettings.Difficulty y)
            {
                return x == y;
            }

            public int GetHashCode(GameSettings.Difficulty obj)
            {
                return (int)obj;
            }
        }

        #region Public Variables
        [Tooltip("Should be one for every difficulty")]
        public GameplayBlueprint[] blueprints;

        #endregion

        /// <summary>
        /// Creates our blueprint lookup dictionary
        /// </summary>
        private void CreateLookup()
        {
            BlueprintLookup = new Dictionary<GameSettings.Difficulty, GameplayBlueprint>();
            int Count = blueprints.Length;
            for(int i=0; i <Count;i++)
            {
                if (BlueprintLookup.ContainsKey(blueprints[i].difficulty))
                {
                    Debug.LogError("Multiple of the same difficulty " + blueprints[i].difficulty + " located in difficulty blueprint!!, Please FIX");
                }
                else
                {
                    BlueprintLookup.Add(blueprints[i].difficulty, blueprints[i]);
                }
                
            }
        }

        /// <summary>
        /// Get a gameplay blueprint by difficulty
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public GameplayBlueprint GetBlueprint(GameSettings.Difficulty difficulty)
        {
            if (BlueprintLookup == null) CreateLookup();
            return BlueprintLookup[difficulty];
        }



    }
}
