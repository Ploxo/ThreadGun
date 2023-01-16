using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float tickTime = 1f;
    [SerializeField] private int baseHealth;
    [SerializeField] private int baseDamage;
    [SerializeField] private int damageTaken = 2;
    [SerializeField] private TextMeshProUGUI healthText;

    private int currentHealth;
    private float time = 0f;
    private List<EnemyNPC> currentEnemies = new List<EnemyNPC>();


    void Start()
    {
        currentHealth = baseHealth;
        healthText.text = "Health: " + currentHealth;
    }

    void Update()
    {
        if (time < 0)
        {
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                if (currentEnemies[i] != null)
                {
                    currentEnemies[i].Damaged(baseDamage);
                }
                else
                {
                    Debug.Log("Removing enemy");
                    currentEnemies.RemoveAt(i);
                }
         }

            if (currentEnemies.Count > 0)
                TakeDamage(currentEnemies.Count * damageTaken);

            time = tickTime;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = (int)Mathf.Max(0f, currentHealth - amount);
        healthText.text = "Health: " + currentHealth;

        if (currentHealth == 0)
            GameOver();

    }

    public void GameOver()
    {
        Debug.Log("Game over!");
        gameManager.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collision with enemy");

            EnemyNPC enemy = other.GetComponent<EnemyNPC>();

            if (!currentEnemies.Contains(enemy))
            {
                Debug.Log("Adding enemy");
                currentEnemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Removing enemy");
            currentEnemies.Remove(other.GetComponent<EnemyNPC>());
        }
    }
}
