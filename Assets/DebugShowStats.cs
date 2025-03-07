using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugShowStats : MonoBehaviour
{
    //code for showing useful stats on the dubug screen
    //'cuz I'm too lazy to check the inspector all the time

    //references
    [SerializeField]public PlayerMovement playerMovement;
    [SerializeField]public InputReader inputReader;
    [SerializeField]public GameObject airTimeText;
    [SerializeField]public GameObject dashCounterText;
    
    private TextMeshProUGUI airTimeTMP;
    private TextMeshProUGUI dashCounterTMP;

    void Start()
    {
        airTimeTMP = airTimeText.GetComponent<TextMeshProUGUI>();
        dashCounterTMP = dashCounterText.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        airTimeTMP.text = "Airtime: " + playerMovement.AirTime.ToString("F2") + " s";
        dashCounterTMP.text = "Dash Counter: " + playerMovement.DashCounter.ToString();
    }

}
