//Unity
using UnityEngine;

//Game
using Common.UI;
using UI.Framework;

namespace UI.Gameplay.Widgets.ProgressWidget
{
    public class ProgressOrb : AbstractUI
    {
        private LineRenderer line;
        private LineRenderer _Line
        {
            get { return line ?? (line = GetComponent<LineRenderer>()); }
        }


        public void SetConnection(Vector3 pos)
        {
            _Line.SetPosition(1, pos);
        }
        public Vector2 GetAnchoredPosition()
        {
            return Rect.anchoredPosition3D;
        }
    }
}
