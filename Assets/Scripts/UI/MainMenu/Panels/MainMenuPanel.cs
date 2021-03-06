﻿//Unity
using UnityEngine;


//Game
using UI.Framework;
using UI.MainMenu.States;

namespace UI.MainMenu.Panels
{
    public class MainMenuPanel : AbstractUI
    {
        private MainMenuStateController controller;
        protected MainMenuStateController Controller
        {
            get { return controller ?? (controller = FindObjectOfType<MainMenuStateController>()); }
        }
        public void BringIn(float time,iTween.EaseType easeType,float fadeSpeed)
        {
            transform.localScale = new Vector3(0, 0, 0);
            FadeTo(1, fadeSpeed);
            ScaleTo(1, time, easeType);
        }

        public void BringOut(float time, iTween.EaseType easeType, float fadeSpeed)
        {
            FadeTo(1, fadeSpeed);
            ScaleTo(0, time, easeType);
        }
    }
}
