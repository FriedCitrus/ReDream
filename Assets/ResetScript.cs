using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> ResetAnims;
    [SerializeField] private PlayerMovementScript disablePlayer;
    void Start()
    {
        // Initialize the list if not done in the inspector
        if (ResetAnims == null)
        {
            ResetAnims = new List<MonoBehaviour>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetAllScripts();
        }
    }
    public void ResetAllScripts()
    {
        foreach (var script in ResetAnims)
        {
            if (script is IResettable resettable)
            {
                Debug.Log("Reset");
                resettable.Reset();
            }
        }
    }
}

public interface IResettable
{
    void Reset();
}

