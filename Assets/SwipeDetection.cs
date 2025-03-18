using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

    [SerializeField] private float minDistance = .2f;
    [SerializeField] private float maxTime = 1f;
    private SwipeInput swipeInput;

    private Vector2 startSwipePosition;
    private float swipeStartTime;

    private Vector2 endSwipePosition;
    private float swipeEndTime;

    private void Awake()
    {
        swipeInput = SwipeInput.Instance;
    }

    private void OnEnable()
    {
        swipeInput.OnSwipeStart += SwipeStart;
        swipeInput.OnSwipeEnd += SwipeEnd;
    }

    private void OnDisable()
    {
        swipeInput.OnSwipeStart -= SwipeStart;
        swipeInput.OnSwipeEnd -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        Debug.Log("Swipe Start at: " + position + " at time: " + time);
        startSwipePosition = position;
        swipeStartTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        Debug.Log("Swipe End at: " + position + " at time: " + time);
        endSwipePosition = position;
        swipeEndTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startSwipePosition, endSwipePosition) >= minDistance && swipeEndTime - swipeStartTime <= maxTime)
        {
            Debug.DrawLine(startSwipePosition, endSwipePosition, Color.red, 5f);
            Vector2 direction = endSwipePosition - startSwipePosition;
            float angle = Vector2.Angle(Vector2.right, direction);
            if (angle < 45)
            {
                Debug.Log("Swipe Right");
            }
            else if (angle < 135)
            {
                if (direction.y > 0)
                {
                    Debug.Log("Swipe Up");
                }
                else
                {
                    Debug.Log("Swipe Down");
                }
            }
            else
            {
                Debug.Log("Swipe Left");
            }
        }
    }
}
