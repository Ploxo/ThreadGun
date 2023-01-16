using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNPC : MonoBehaviour
{
    [SerializeField] private double maxHealth = 100;
    [SerializeField] private Slider slider;

    private double health;


    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0) Destroy(gameObject);
    }

    public void Damaged(double amount)
    {
        health -= amount;

        if (slider != null)
            slider.value = (float)health;
    }
}
