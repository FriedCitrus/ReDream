using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_Script : MonoBehaviour
{

    void Start()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Detected");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Detected Player");
            //animator.SetTrigger("PlayAnimation");
        }
    }
}
