using UnityEngine;

namespace Assets.Scripts.Controls
{
    public abstract class AbstractControl:MonoBehaviour,IPlayerControl
    {
        private float angle;
        private float sensitivity;
        private int angleMin=-1, angleMax=1;
#if UNITY_WEBGL && !UNITY_EDITOR
        private const float baseSensitivity = 0.1f;
#else
        private const float baseSensitivity = 1f;
#endif

        //unity
        private void FixedUpdate()
        {
            ReadInput();
        }
        //unity

        protected abstract void ReadInput();

        public float GetAngle()
        {
            return angle;
        }

        public void SetAngle(float val)
        {
            angle = val;
        }

        protected void UpdateAngle(float val)
        {
            angle += val;
            if (angle < angleMin)
            {
                angle = angleMax;
            }
            else if (angle > angleMax)
            {
                angle = angleMin;
            }
        }

        public void SetSensitivity(float val)
        {
            sensitivity = baseSensitivity*val;
        }

        protected float GetSensitivity()
        {
            return sensitivity;
        }
    }
}