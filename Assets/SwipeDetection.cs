using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private SwipeListener listener;

    private void OnEnable()
    {
        listener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        Debug.Log(swipe);
    }

    private void OnDisable()
    {
        listener.OnSwipe.RemoveListener(OnSwipe);
    }
}
