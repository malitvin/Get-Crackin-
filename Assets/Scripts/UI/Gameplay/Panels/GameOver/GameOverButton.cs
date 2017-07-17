//Unity


//Game
using UI.Gameplay.Widgets;

//C#


namespace UI.Gameplay.Panels
{
    /// <summary>
    /// Buttons that appear on the game over screen
    /// </summary>
    public class GameOverButton : GameplayWidget
    {
        public GameOverPanel.Options OPTION;


        private GameOverPanel panel;
        private GameOverPanel _Panel
        {
            get { return panel ?? (panel = GetComponentInParent<GameOverPanel>()); }
        }


        protected override void Button_OnSelected()
        {
            base.Button_OnSelected();
            _Panel.GameOverButton_OnSelected(OPTION);
        }

    }
}
