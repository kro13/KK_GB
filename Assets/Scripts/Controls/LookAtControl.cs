using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class LookAtControl:AbstractControl
    {
        private float angle;

        //TODO:Current implementation is incorrect
        override protected void ReadInput()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), transform.position - mousePosition);
            transform.rotation = rotation;
            angle = (transform.rotation.eulerAngles.z - 180) / 180 - Mathf.Sign((transform.rotation.eulerAngles.z - 180) / 180);
            UpdateAngle(angle);
        }
    }
}