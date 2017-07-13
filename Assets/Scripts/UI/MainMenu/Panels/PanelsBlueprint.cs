//Unity
using UnityEngine;

//Game
using Common.Attributes;
using UI.MainMenu.States;

namespace UI.MainMenu.Panels
{
    [CreateAssetMenu(menuName = "UI/Main Menu Blueprint",fileName ="UI")]
    public class PanelsBlueprint : ScriptableObject
    {
        [System.Serializable]
        public struct Panel
        {
            public string name;
            public MainState.PanelType key;

            [PrefabDropdown("UI/MainMenu/Panels")]
            public MainMenuPanel prefab;
        }


        public Panel[] panels;
    }
}
