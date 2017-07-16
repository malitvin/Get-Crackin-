//Unity
using UnityEngine;

namespace Gameplay.GameInput
{
    /// <summary>
    /// Simple helper class for number input so i dont have to put it all in the input state
    /// </summary>
    public class NumberInput
    {
        public class HotKey
        {
            public int number;
            public string standard;
            public string numberPad;
            public HotKey(int number,string standard,string numberPad) {
                this.number = number;
                this.standard = standard;
                this.numberPad = numberPad;
            }

        }

        private HotKey[] keys;

        private int numberPressed;

        private void CreateHotKeys()
        {
            keys = new HotKey[9];
            for(int i=0; i < 9; i++)
            {
                HotKey key = new HotKey(i+1, (i+1).ToString(), "[" + (i+1).ToString() + "]");
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
