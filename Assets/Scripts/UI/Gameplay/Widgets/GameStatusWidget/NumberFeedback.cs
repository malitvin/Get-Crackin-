//Unity
using UnityEngine;


//Game
using Common.Pooler;

//C#
using System.Collections.Generic;

namespace UI.Gameplay.Widgets.GameStatusWidget
{
    /// <summary>
    /// widget that will animate in number feedback and have numbers animate up
    /// </summary>
    public class NumberFeedback : GenericPooler
    {
        public float leftMostAnchorX;
        public float animSpeed = 500;

        private Dictionary<int, Vector2> spawnPositionLookup;

        private CanvasGroup grid;
        private CanvasGroup _Grid
        {
            get { return grid ?? (grid = GetComponent<CanvasGroup>()); }
        }

        private void CreateSpawnPositionLookup()
        {
            spawnPositionLookup = new Dictionary<int, Vector2>();
            for(int i=0; i < 9; i++)
            {
                spawnPositionLookup.Add((i + 1), new Vector2(leftMostAnchorX + (i * 30), 0));
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_Grid.alpha > 0) _Grid.alpha -= 1 * Time.deltaTime;

        }

        public void SpawnNumber(int number)
        {
            if (spawnPositionLookup == null) CreateSpawnPositionLookup();

            _Grid.alpha = 1;
            AnimNumber gen = GetPooledObject(Vector3.zero) as AnimNumber;
            gen.transform.localPosition = new Vector3(0, 0, 0);
            gen.Init(number, spawnPositionLookup[number], animSpeed);
        }

    }
}
