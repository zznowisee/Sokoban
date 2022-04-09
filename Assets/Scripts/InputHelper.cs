using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class InputHelper
{
    static Camera mainCam;

    public static Camera MainCam
    {
        get
        {
            if(mainCam == null)
            {
                mainCam = Camera.main;
            }
            return mainCam;
        }
    }

    public static Vector3 MouseWorldPosition => MainCam.ScreenToWorldPoint(Input.mousePosition);
    public static bool IsMouseOverUIObject => EventSystem.current.IsPointerOverGameObject();
    public static MapUnit GetUnitUnderMousePosition()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(MouseWorldPosition, Vector3.forward, float.MaxValue);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider)
            {
                MapUnit unit = hits[i].collider.gameObject.GetComponent<MapUnit>();
                if (unit != null)
                {
                    return unit;
                }
            }
        }
        return null;
    }

    public static bool IsTheseKeysDown(params KeyCode[] keyCodes)
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsTheseKeysHeld(params KeyCode[] keyCodes)
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKey(keyCodes[i]))
            {
                return true;
            }
        }
        return false;
    }

}
