//Unity
using UnityEngine;

namespace Gameplay.GameInput
{
    /// <summary>
    /// Simple helper class for number input so i dont have to put it all in the input state
    /// </summary>
    public class NumberInput
    {
        public struct HotKey
        {
            public int number;
            public string standard;
            public string numberPad;

        }

        private HotKey[] keys;

        private int numberPressed;

        private void CreateHotKeys()
        {
            keys = new HotKey[9];
            for(int i=0; i < 10; i++)
            {
                HotKey key;
                key.number = i;
                key.standard = i.ToString();
                key.numberPad = "[" + i.ToString() + "]";
                keys[i] = key;
            }
        }

        public bool NumberPressed()
        {
            if (keys == null) CreateHotKeys();
            for(int i=0; i < keys.Length;i++)
            {
                if (Input.GetKeyDown(keys[i].standard) || Input.GetKeyDown(keys[i].numberPad))
                {
                    numberPressed = keys[i].number;
                    return true;
                }
            }
            return false;
        }

        public int GetNumberPressed()
        {
            return numberPressed;
        }
    }
}
