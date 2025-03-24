using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crublescript : MonoBehaviour, IResettable
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("PlayAnimation");
        }
    }
     
    public void Animate(bool PlayerDisabled)
    {
        animator.SetBool("isDisabled", PlayerDisabled);
        Debug.Log("Animating");
        animator.SetTrigger("crumbling");
    }

    public void Reset(bool PlayerDisabled)
    {
        Debug.Log("Reseting");
        animator.SetTrigger("reset");
        animator.SetBool("isDisabled", PlayerDisabled);
    }

}
