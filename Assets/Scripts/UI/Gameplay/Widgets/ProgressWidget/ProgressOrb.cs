//Unity
using UnityEngine;
using UnityEngine.UI.Extensions;

//Game
using UI.Framework;

namespace UI.Gameplay.Widgets.ProgressWidget
{
    public class ProgressOrb : AbstractUI
    {
        private UILineRenderer line;
        private UILineRenderer _Line
        {
            get { return line ?? (line = GetComponentInChildren<UILineRenderer>()); }
        }

        public void SetConnection(Vector3 pos)
        {
            _Line.Points[1] = pos;
            _Line.enabled = false; _Line.enabled = true;
        }
        public Vector2 GetAnchoredPosition()
        {
            return Rect.anchoredPosition3D;
        }
    }
}
