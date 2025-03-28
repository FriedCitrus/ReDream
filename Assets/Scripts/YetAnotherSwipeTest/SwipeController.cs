using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SwipeManager))]
public class InputController : MonoBehaviour
{
    //public Player OurPlayer; // Perhaps your playerscript?

    void Start()
    {
        SwipeManager swipeManager = GetComponent<SwipeManager>();
        swipeManager.onSwipe += HandleSwipe;
        swipeManager.onLongPress += HandleLongPress;
        Debug.Log("Screen Width : " + Screen.width);
    }

    void HandleSwipe(SwipeAction swipeAction)
    {
        Debug.LogFormat("HandleSwipe: {0}", swipeAction);
        if (swipeAction.direction == SwipeDirection.Up || swipeAction.direction == SwipeDirection.UpRight)
        {
            // move up
            //if(OurPlayer != null)
            Debug.Log("up");
        }
        else if (swipeAction.direction == SwipeDirection.Right || swipeAction.direction == SwipeDirection.DownRight)
        {
            // move right
            //if (OurPlayer != null)
            Debug.Log("right");
        }
        else if (swipeAction.direction == SwipeDirection.Down || swipeAction.direction == SwipeDirection.DownLeft)
        {
            // move down
            //if (OurPlayer != null)
            Debug.Log("down");
        }
        else if (swipeAction.direction == SwipeDirection.Left || swipeAction.direction == SwipeDirection.UpLeft)
        {
            // move left
            //if (OurPlayer != null)
            Debug.Log("left");
        }
    }

    void HandleLongPress(SwipeAction swipeAction)
    {
        Debug.LogFormat("HandleLongPress: {0}", swipeAction);
    }
}

