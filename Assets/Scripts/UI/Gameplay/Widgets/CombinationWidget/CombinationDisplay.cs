//Unity
using UnityEngine;

//Game
using UI.Framework;
using Common.Extensions;

//Pooler
using Common.Pooler;

//C#
using System;
using System.Collections.Generic;

namespace UI.Gameplay.Widgets.CombinationWidget
{
    /// <summary>
    /// Display that shows the combination on the board, either showing the current combination, the users input,
    /// or the users input with right and wrong answers
    /// </summary>
    public class CombinationDisplay : GameplayWidget
    {
        public enum Type { Normal,Correct,InCorrect}
        private Dictionary<Type, Color> TypeColors = new Dictionary<Type, Color>()
        {
            {Type.Normal,Color.white },
            {Type.InCorrect,Color.red },
            {Type.Correct,Color.green }
        };

        private GenericPooler combinationPooler;
        private GenericPooler _CombinationPooler
        {
            get { return combinationPooler ?? (combinationPooler = GetComponent<GenericPooler>()); }
        }

        private int tempNumber;
        private Vector3 tempPos = new Vector3(0, 0, 0);


        protected override void Awake()
        {
            base.Awake();
            CreateListeners();
        }

        private void CreateListeners()
        {
            Action<string> prepareNumber = new Action<string>(PrepareNumber);
            StartListenting(UIEvents.Type.PrepareCombinationNumber, prepareNumber);

            Action<string> displayCombinationNumber = new Action<string>(DisplayNumber);
            StartListenting(UIEvents.Type.DisplayCombinationNumber, displayCombinationNumber);
        }

        private void PrepareNumber(string number)
        {
            tempNumber = number.ParseIntFast();
        }

        private void DisplayNumber(string type)
        {
            Type spawnType = (Type)Enum.Parse(typeof(Type), type);
            CombinationOrb orb = _CombinationPooler.GetPooledObject(tempPos) as CombinationOrb;
            orb.transform.localPosition = Vector3.zero;
            orb.Init(TypeColors[spawnType],tempNumber);
        }
    }
}
