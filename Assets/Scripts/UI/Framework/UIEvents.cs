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
            PlayButtonSound,

            /// <summary>
            /// GAMEPLAY
            /// </summary>
            InitProgressWidget,
            UnlockProgressOrb,
            ToggleGamePanel,
            PrepareCombinationNumber,
            DisplayCombinationNumber,
            RemoveCombination,
            ChangeGameStatusText,
            SpawnAnimNumber,
            UpdateDetectionSlider,
            ToggleGameOverPanel,
            PrepareHighScoreNumber,
            UpdateScoreText,
            ReplayButtonSelected,
            QuitButtonSelected,

            /// <summary>
            /// Other
            /// </summary>
            SceneComeOut,
            SceneComeIn
        }
    }
}
