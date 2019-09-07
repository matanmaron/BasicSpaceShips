using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    private bool tap, left, right, up, down;
    private bool dragging = false;
    private Vector2 start, delta;

    void Update()
    {
        tap = left = right = up = down = false;
        SupportMouseTouch();
        SupportMobileTouch();

        SupportDragging();
        SupportDeadZone();
    }

    private void SupportDeadZone()
    {
        if (delta.magnitude > 50)
        {
            float x = delta.x;
            float y = delta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //l or r
                if (x < 0)
                {
                    left = true;
                }
                else
                {
                    right = true;
                }
            }
            else
            {
                //u or d
                if (y < 0)
                {
                    down = true;
                }
                else
                {
                    up = true;
                }
            }
        }
    }

    private void SupportDragging()
    {
        delta = Vector2.zero;
        if (dragging)
        {
            if (Input.touches.Length > 0)
            {
                delta = Input.touches[0].position - start;
            }
            else if (Input.GetMouseButton(0))
            {
                delta = (Vector2)Input.mousePosition - start;
            }
        }
    }

    private void SupportMobileTouch()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                dragging = true;
                tap = true;
                start = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                dragging = false;
                Reset();
            }
        }
    }

    private void SupportMouseTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            tap = true;
            start = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            dragging = false;
            Reset();
        }
    }

    void Reset()
    {
        start = delta = Vector2.zero;
        dragging = false;
    }

    public Vector2 Delta { get { return delta; } }
    public bool Tap { get { return tap; } }
    public bool Drag { get { return dragging; } }
    public bool Left { get { return left; } }
    public bool Right { get { return right; } }
    public bool Up { get { return up; } }
    public bool Down { get { return down; } }
}
