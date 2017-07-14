//Unity

namespace UI.Framework
{
    /// <summary>
    /// Different types of things that can happen related to the UI
    /// This is so that the UI and gameplay state machine states can work together without knowing much about each other
    /// </summary>
    public class UIEvents
    {
        public enum Type
        { 
            /// <summary>
            /// MAIN MENU
            /// </summary>
            MainMenuNavigation,
            PlayGame,
            UpdateDifficulty,

            /// <summary>
            /// GAMEPLAY
            /// </summary>
            InitProgressWidget,

            /// <summary>
            /// Other
            /// </summary>
            SceneComeOut,
            SceneComeIn
        }
    }
}
