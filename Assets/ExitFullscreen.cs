using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitFullscreen : MonoBehaviour
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
        }
    }
}
