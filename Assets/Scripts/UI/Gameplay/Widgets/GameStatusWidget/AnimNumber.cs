//Unity
using UnityEngine;

//TMPRO
using TMPro;


//Game
using Common.Pooler;

//C#
using System.Collections.Generic;

namespace UI.Gameplay.Widgets.GameStatusWidget
{
    /// <summary>
    /// Animation numbers that float up
    /// </summary>
    public class AnimNumber : PooledObject
    {

        private GameplayWidget thisUI;
        private GameplayWidget _ThisUI
        {
            get { return thisUI ?? (thisUI = gameObject.AddComponent<GameplayWidget>()); }
        }

        private TrailRenderer trail;
        private TrailRenderer _Trail
        {
            get { return trail ?? (trail = GetComponent<TrailRenderer>()); }
        }

        private TextMeshProUGUI txt;
        private TextMeshProUGUI _Txt
        {
            get { return txt ?? (txt = GetComponent<TextMeshProUGUI>()); }
        }

        private float animSpeed;

        private const float DESTRUCT_TIME = 3;
        private float destructionTimer = 0;

        private void Update()
        {
            _ThisUI.Rect.anchoredPosition += new Vector2(0,animSpeed * Time.smoothDeltaTime);

            destructionTimer += Time.deltaTime;
            if (destructionTimer > DESTRUCT_TIME) GetPooler().RemovePooledObject(this);
        }


        #region Pooled Methods
        public override void Show()
        {
            base.Show();
            _ThisUI.MakeVisible(false);
            _ThisUI.FadeTo(1, 0.5f);
        }

        public override void Remove()
        {
            base.Remove();
            _Trail.Clear();
            destructionTimer = 0;
        }
        #endregion

        //Init number and position
        public void Init(int number,Vector2 pos,float animSpeed)
        {
            _Txt.text = number.ToString();
            _ThisUI.SetRectAnchoredPosition(pos);
            this.animSpeed = animSpeed;
        }

    }
}
