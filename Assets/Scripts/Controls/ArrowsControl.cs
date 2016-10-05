using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class ArrowsControl:AbstractControl
    {
        private float angle;

        //TODO:Current implementation is incorrect
        override protected void ReadInput()
        {
            float horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 1.3f;
            float moveSign = Mathf.Sign(horizontalMove);

            if (Mathf.Abs(horizontalMove) > 0)
            {
                angle += moveSign * 0.02f * 1.2f;
            }

            if (angle < 0)
            {
                angle += 0.01f;
                if (angle > 0)
                {
                    angle = 0;
                }
            }
            else if (angle > 0)
            {
                angle -= 0.01f;
                if (angle < 0)
                {
                    angle = 0;
                }
            }

            UpdateAngle(angle);
        }

    }
}