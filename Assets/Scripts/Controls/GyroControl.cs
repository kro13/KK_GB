using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class GyroControl:AbstractControl
    {
        private float input;
        private float newInput;

        private void Start()
        {
            Input.gyro.enabled = true;
            Input.compass.enabled = true;
        }

        override protected void ReadInput()
        {
            newInput = Input.acceleration.x*GetSensitivity()*100*Time.deltaTime - GetAngle();
            input = input*0.7f + newInput*0.1f;
            UpdateAngle(input);
        }
    }
}