using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    public float maxHealth = 100f;
    public float HP;

    void Start()
    {
        healthBar = GetComponent<Image>();
        HP = maxHealth;
        
    }

    void Update()
    {
        healthBar.fillAmount = HP / maxHealth;
    }
}
