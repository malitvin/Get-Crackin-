//Unity
using UnityEngine;

//Game
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
            public MainMenuPanel prefab;
        }


        public Panel[] panels;
    }
}
