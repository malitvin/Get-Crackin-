//Unity
using UnityEngine;
using UnityEngine.UI;

//TMPRO


//Game
using UI.Framework;

//Managers


//C#
using System.Collections;

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

        private Text inputText;
        private Text _InputText
        {
            get { return inputText ?? (inputText = _InputField.GetComponentInChildren<Text>()); }
        }

        private Button submitButton;
        private Button _SubmitButton
        {
            get { return submitButton ?? (submitButton = GetComponentInChildren<Button>()); }
        }
        #endregion

        private Color originalInputColor;

        protected override void Awake()
        {
            base.Awake();
            _SubmitButton.onClick.AddListener(() => SubmitButton_OnSelected());
            originalInputColor = _InputText.color;
        }

        private void Update()
        {
            if(_InputField.text.Length < 3)
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
            _ParenPanel.SubmitButton_OnSelected(_InputField.text);
            EnableInteraction(false);
        }

        public void TryAgain()
        {
            StartCoroutine(TryAgainRoutine());
        }

        private IEnumerator TryAgainRoutine()
        {
            _InputText.color = Color.red;
            _InputField.text = "NAME EXISTS!";
            yield return new WaitForSeconds(1);
            _InputText.color = originalInputColor;
            _InputField.text="";
            EnableInteraction(true);
        }

    }
}
