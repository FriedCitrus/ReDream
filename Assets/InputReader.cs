using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeStartTime;

    void Update()
    {
        if (Input.touchCount > 0) // Check if there's at least one touch
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                swipeStartTime = Time.time;
                Debug.Log("Touch detected at: " + touch.position);
            }else if(touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                float swipeDuration = Time.time - swipeStartTime;
                float swipeLength = (endTouchPosition - startTouchPosition).magnitude;

                if(swipeLength > 100 && swipeDuration < 1f)
                {
                    DetectSwipe();
                }
            }
        }
    }

    private void DetectSwipe(){
        Vector2 swipeVector = endTouchPosition - startTouchPosition;
        float swipeAngle = Vector2.SignedAngle(Vector2.right, swipeVector);

        if(swipeVector.magnitude < 50)
        {
            return;
        }

        if (swipeAngle >= -22.5f && swipeAngle < 22.5f)
        {
            Debug.Log("Swipe Right");
        }
        else if (swipeAngle >= 22.5f && swipeAngle < 67.5f)
        {
            Debug.Log("Swipe Up-Right");
        }
        else if (swipeAngle >= 67.5f && swipeAngle < 112.5f)
        {
            Debug.Log("Swipe Up");
        }
        else if (swipeAngle >= 112.5f && swipeAngle < 157.5f)
        {
            Debug.Log("Swipe Up-Left");
        }
        else if (swipeAngle >= 157.5f || swipeAngle < -157.5f)
        {
            Debug.Log("Swipe Left");
        }
        else if (swipeAngle >= -157.5f && swipeAngle < -112.5f)
        {
            Debug.Log("Swipe Down-Left");
        }
        else if (swipeAngle >= -112.5f && swipeAngle < -67.5f)
        {
            Debug.Log("Swipe Down");
        }
        else if (swipeAngle >= -67.5f && swipeAngle < -22.5f)
        {
            Debug.Log("Swipe Down-Right");
        }
    }

}
