using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crublescript : MonoBehaviour, IResettable
{
    private Animator animator;
    private Transform transform;

    void Start()
    {
        animator = GetComponent<Animator>();

    }
     
    public void Animate()
    {
        Debug.Log("Animating");
        animator.SetBool("PlayerReset", false);
        animator.SetBool("PlayerTouched", true);
        //animator.SetTrigger("crumbling");
    }

    public void Reset()
    {
        Debug.Log("Reseting");
        //animator.SetTrigger("reset");
        animator.SetBool("PlayerReset", true);
    }

}
