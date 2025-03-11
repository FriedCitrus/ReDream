using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    
    public void WalkLeftonPress()
    {
        playerMovement.WalkLeft();
    }

    
    void Update()
    {
        
    }
}
