//Unity
using UnityEngine;

//Game
using Common.Attributes;
using Common.Extensions;
using UI.Framework;

//C#
using System;

namespace UI.Gameplay.Widgets.ProgressWidget
{
    /// <summary>
    /// Shows Progress of the Safe Combo
    /// </summary>
    public class ProgressWidget : GameplayWidget
    {
        #region Publics
        [PrefabDropdown("UI/Gameplay/Spawnable")]
        public ProgressOrb progressOrbPrefab;

        private ProgressOrb[] progressOrbs;

        [Space(10)]
        public Sprite lockedOrb;
        public Sprite unlockedOrb;

        [Space(10)]
        public Color lockedLine;
        public Color unlockedLine;

        [Space(10)]
        [Range(0.01f,2f)]
        public float unlockAnimationTime = 2;
        [Range(0.01f, 2f)]
        public float lockAnimationTime = 2;
        #endregion

        #region Privates
        private int unlockCounter = 0;
        #endregion

        #region Components
        #endregion

        protected override void Awake()
        {
            base.Awake();
            CreateListeners();
        }

        private void Update()
        {
        }

        private void CreateListeners()
        {
            Action<string> listenForInit = new Action<string>(InitProgressWidget);
            StartListenting(UIEvents.Type.InitProgressWidget, listenForInit);

            Action<string> listenForUnlcok = new Action<string>(UnLockOrb);
            StartListenting(UIEvents.Type.UnlockProgressOrb, listenForUnlcok);

            Action<string> listenForReset = new Action<string>(ResetProgress);
            StartListenting(UIEvents.Type.ResetProgressOrbs, listenForReset);

        }

        private void InitProgressWidget(string s)
        {

            int combinationCount = s.ParseIntFast();
            progressOrbs = new ProgressOrb[combinationCount];

            //Generate Orbs
            for (int i = 0; i < combinationCount; i++)
            {
                progressOrbs[i] = Instantiate(progressOrbPrefab, transform, false);
                progressOrbs[i].name = "Progress ORB " + i.ToString();
                progressOrbs[i].InitOrb(lockedOrb);
            }

            Canvas.ForceUpdateCanvases();

            //Generate Lines
            for (int i = 0; i < combinationCount; i++)
            {
                if (i > 0)
                {
                    Vector2 newPos = new Vector2(progressOrbs[i].GetAnchoredPosition().x - progressOrbs[i - 1].GetAnchoredPosition().x, 0);
                    progressOrbs[i - 1].InitLine(newPos, lockedLine);
                }

            }

        }

        public void UnLockOrb(string s)
        {
            progressOrbs[unlockCounter].Morph(unlockedOrb, unlockedLine, unlockAnimationTime);
            unlockCounter++;
        }

        private void ResetProgress(string mesage)
        {
            unlockCounter = 0;
            int count = progressOrbs.Length;
            for(int i=0; i < progressOrbs.Length;i++)
            {
                progressOrbs[i].Morph(lockedOrb, lockedLine, lockAnimationTime);
            }
        }


    }
}

