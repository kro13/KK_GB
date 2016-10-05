using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseLocker : MonoBehaviour
#if UNITY_WEBGL&&!UNITY_EDITOR
    , IPointerDownHandler
#else
    , IPointerClickHandler
#endif
{

    public bool isLocker=true;
#if UNITY_WEBGL&&!UNITY_EDITOR
    public void OnPointerDown(PointerEventData eData)
    {
        if (isLocker)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
#else
    public void OnPointerClick(PointerEventData eData)
    {
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
#endif
}
