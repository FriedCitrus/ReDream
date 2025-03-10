using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{   
    //variables
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeStartTime;

    //state of the input: "None", "Touch", "Tap", "Right", "Up-Right", "Up", "Up-Left", "Left", "Down-Left", "Down", "Down-Right"
    public string state = "None";

    //the buttons will be placed in the corners of the screen and have it's own input

    void Update()
    {
        if (Input.touchCount > 0) // Check if there's at least one touch
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began) //basically tapping the screen
            {
                startTouchPosition = touch.position;
                swipeStartTime = Time.time;
                Debug.Log("Touch detected at: " + touch.position);
                state = "Touch";
            }else if(touch.phase == TouchPhase.Ended) //swiping the screen
            {
                endTouchPosition = touch.position;
                float swipeDuration = Time.time - swipeStartTime;
                float swipeLength = (endTouchPosition - startTouchPosition).magnitude;

                if(swipeLength > 100 && swipeDuration < 1f)
                {
                    Debug.Log("Swipe " + DetectSwipe());
                    state = DetectSwipe();
                }
            }

        }else{
            //if nothing is happening, it will be just this
            state = "None";
        }
        //Debug.Log(state);
    }

    private void checkButtonPress()
    {
        Debug.Log("The button is pressed");
    }


    //detect the swipe direction
    //wish I could optimize this
    public string DetectSwipe(){
        Vector2 swipeVector = endTouchPosition - startTouchPosition;
        float swipeAngle = Vector2.SignedAngle(Vector2.right, swipeVector);

        if(swipeVector.magnitude > 50)
        {
            if (swipeAngle >= -22.5f && swipeAngle < 22.5f)
            {
                return "Right";
            }
            else if (swipeAngle >= 22.5f && swipeAngle < 67.5f)
            {
                return "Up-Right";
            }
            else if (swipeAngle >= 67.5f && swipeAngle < 112.5f)
            {
                return "Up";
            }
            else if (swipeAngle >= 112.5f && swipeAngle < 157.5f)
            {
                return "Up-Left";
            }
            else if (swipeAngle >= 157.5f || swipeAngle < -157.5f)
            {
                return "Left";
            }
            else if (swipeAngle >= -157.5f && swipeAngle < -112.5f)
            {
                return "Down-Left";
            }
            else if (swipeAngle >= -112.5f && swipeAngle < -67.5f)
            {
                return "Down";
            }
            else if (swipeAngle >= -67.5f && swipeAngle < -22.5f)
            {
                return "Down-Right";
            }
        }
        return "Tap";
        
    }

}
