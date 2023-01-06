using UnityEngine;
using System;

public class Controls : MonoBehaviourSingleton<Controls>
{
    [HideInInspector] public bool InputLocked = false;
    [HideInInspector] public bool TouchDown = false;
    [HideInInspector] public bool TouchUp = false;
    [HideInInspector] public bool IsDragging = false;

    public bool Multitouch
    {
        get { return Input.touches.Length >= 2; }
    }

    public Vector2 TouchPosition
    {
        // THIS IS NOT IN WORLD COORDINATES. We have to make; ScreenUtils.MainCamera.screentoworld()
        get
        {
            if (PlatformUtils.isWindows() || PlatformUtils.isMac())
                return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            else
            {
                if (Input.touchCount > 0)
                    return Input.GetTouch(0).position;

                return Vector2.zero;
            }
        }
    }


    public override void Awake()
    {
        base.Awake();
        InputLocked = false;
    }

    private void Update()
    {
        if (InputLocked)
            return;

        if (PlatformUtils.isWindows() || PlatformUtils.isMac())
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchDown = true;
                IsDragging = true;
            }
            else
                TouchDown = false;

            if (Input.GetMouseButtonUp(0))
            {
                IsDragging = false;
                TouchUp = true;
            }
            else
                TouchUp = false;
        }
        else if (PlatformUtils.isAndroid() || PlatformUtils.isApple())
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    TouchDown = true;
                    IsDragging = true;
                }
                else
                {
                    TouchDown = false;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    IsDragging = false;
                    TouchUp = true;
                }
                else
                {
                    TouchUp = false;
                }
            }
            else
            {
                TouchDown = false;
                TouchUp = false;
                IsDragging = false;
            }
        }

    }
}
