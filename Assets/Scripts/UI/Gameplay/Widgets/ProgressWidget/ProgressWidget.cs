//Unity
using UnityEngine;

//Game
using UI.Gameplay.Widgets;

//C#
using System;

//Game
using Common.Extensions;

namespace UI.Gameplay.Widgets.ProgressWidget
{
    /// <summary>
    /// Shows Progress of the Safe Combo
    /// </summary>
    public class ProgressWidget : GameplayWidget
    {

        protected override void Awake()
        {
            base.Awake();
            CreateListeners();
        }

        private void CreateListeners()
        {
            Action<string> listenForInit = new Action<string>(InitProgressWidget);
            StartListenting(Framework.UIEvents.Type.InitProgressWidget, listenForInit);

        }

        private void InitProgressWidget(string s)
        {
            int combinationCount = s.ParseIntFast();
        }


    }
}

