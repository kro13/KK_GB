using UnityEngine;
using System.Collections;

public class PixelPerfectScale:MonoBehaviour{

    public static int screenWidth = 160;
    private static float screenHeight = 0;
	
	public void Update()
    {
	    if(screenHeight!=(float) Screen.height)
        {
            screenHeight = (float)Screen.height;
            float screenRatio = screenHeight / screenWidth;
            float ratio = Mathf.Ceil(screenRatio) / screenRatio;

            transform.localScale = Vector3.one * ratio;
        }
	}

    /*public static void MakePixelPerfect(Transform transform)
    {
        if (screenHeight != (float)Screen.height)
        {
            screenHeight = (float)Screen.height;
            float screenRatio = screenHeight / screenWidth;
            float ratio = Mathf.Ceil(screenRatio) / screenRatio;

            transform.localScale = Vector3.one * ratio;
        }
    }*/
}
