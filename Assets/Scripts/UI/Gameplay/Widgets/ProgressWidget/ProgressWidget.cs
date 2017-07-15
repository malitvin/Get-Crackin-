//Unity
using UnityEngine;
using UnityEngine.UI;

//Game
using Common.Attributes;
using UI.Gameplay.Widgets;

//C#
using System;
using System.Collections;

//Game
using Common.Extensions;

namespace UI.Gameplay.Widgets.ProgressWidget
{
    /// <summary>
    /// Shows Progress of the Safe Combo
    /// </summary>
    public class ProgressWidget : GameplayWidget
    {
        [PrefabDropdown("UI/Gameplay/Spawnable")]
        public ProgressOrb progressOrbPrefab;

        private ProgressOrb[] progressOrbs;

        #region Components
        #endregion

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
            progressOrbs = new ProgressOrb[combinationCount];

            for (int i = 0; i < combinationCount; i++)
            {
                progressOrbs[i] = Instantiate(progressOrbPrefab, transform, false);
                progressOrbs[i].name = "Progress ORB " + i.ToString();
            }

            Canvas.ForceUpdateCanvases();

            //Generate Lines
            for (int i = 0; i < combinationCount; i++)
            {
                if (i > 0)
                {
                    Vector2 newPos = new Vector2(progressOrbs[i].GetAnchoredPosition().x - progressOrbs[i - 1].GetAnchoredPosition().x, 0);
                    progressOrbs[i - 1].SetConnection(newPos);
                }
                
            }
        }


    }
}

