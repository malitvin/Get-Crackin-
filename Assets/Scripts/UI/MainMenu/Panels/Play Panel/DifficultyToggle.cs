//Unity
using UnityEngine;

//Game
using UI.MainMenu.Widgets;
using Managers.GameSettings;

namespace UI.MainMenu.Panels.PlayPanel
{
    /// <summary>
    /// Toggle Difficulty
    /// </summary>
    public class DifficultyToggle : MainMenuWidget
    {
        public GameSettings.Difficulty difficultySetting;

        protected override void Toggle_OnValueChanged(bool isOn)
        {
            base.Toggle_OnValueChanged(isOn);
            if(isOn) TriggerEvent(Framework.UIEvents.Type.UpdateDifficulty, difficultySetting.ToString());
        }

    }
}
