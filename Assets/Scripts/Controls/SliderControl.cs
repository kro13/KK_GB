using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class SliderControl:AbstractControl
    {
        private float input;
        override protected void ReadInput()
        {
            float horizontalMove = Input.GetAxis("Mouse X");
            input = horizontalMove * GetSensitivity() * Time.deltaTime;
            UpdateAngle(input);
        }
    }
}