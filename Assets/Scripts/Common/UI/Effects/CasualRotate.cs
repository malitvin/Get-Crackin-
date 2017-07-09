//Unity
using UnityEngine;

namespace Common.UI.Effects
{
    /// <summary>
    /// Used a simple rotate by speed for an euler axis
    /// </summary>
    public class CasualRotate : MonoBehaviour
    {
        
        public enum EulerAxis { X,Y,Z}

        public float rotateSpeed;

        public EulerAxis rotationAxis;

        private void Update()
        {
            switch(rotationAxis)
            {
                case EulerAxis.X:
                    transform.eulerAngles += new Vector3(rotateSpeed * Time.deltaTime, 0,0);
                    break;
                case EulerAxis.Y:
                    transform.eulerAngles += new Vector3(0, rotateSpeed * Time.deltaTime, 0);
                    break;
                case EulerAxis.Z:
                    transform.eulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
                    break;
            }
            
        }
    }
}
