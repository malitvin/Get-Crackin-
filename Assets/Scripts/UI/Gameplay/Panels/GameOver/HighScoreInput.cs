//Unity
using UnityEngine;
using UnityEngine.UI;

//TMPRO


//Game
using UI.Framework;

//Managers



//C#


namespace UI.Gameplay.Panels
{
    /// <summary>
    /// High SCore input panel
    /// </summary>
    public class HighScoreInput : UIWidget
    {
        #region Components
        private GameOverPanel parentPanel;
        private GameOverPanel _ParenPanel
        {
            get { return parentPanel ?? (parentPanel = GetComponentInParent<GameOverPanel>()); }
        }

        private InputField inputField;
        private InputField _InputField
        {
            get { return inputField ?? (inputField = GetComponentInChildren<InputField>()); }
        }

        private Button submitButton;
        private Button _SubmitButton
        {
            get { return submitButton ?? (submitButton = GetComponentInChildren<Button>()); }
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            _SubmitButton.onClick.AddListener(() => SubmitButton_OnSelected());
        }

        private void Update()
        {
            if(_InputField.text.Length < 2)
            {
                _SubmitButton.interactable = false;
            }
            else
            {
                _SubmitButton.interactable = true;
            }
        }

        public override void MakeVisible(bool enable, bool affectInteraction = true)
        {
            base.MakeVisible(enable, affectInteraction);
            _InputField.text = "";
        }


        protected void SubmitButton_OnSelected()
        {

        }

    }
}
