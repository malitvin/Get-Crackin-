//Unity
using UnityEngine;

//Game
using Common.UI.Effects.Ripple;
using UnityEngine.UI;

namespace UI.Effects
{
    /// <summary>
    /// Number ripple on interactive background
    /// </summary>
    public class NumberRipple : UIRipple
    {
        [Space(10)]
        public Sprite[] numberSprites;

        protected override void SetSprite(Image ThisRipple)
        {
            base.SetSprite(ThisRipple);
            //set random number sprite
            ThisRipple.sprite = numberSprites[Random.Range(0, numberSprites.Length)];
        }
    }
}
