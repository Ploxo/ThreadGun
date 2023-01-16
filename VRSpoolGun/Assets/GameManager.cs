using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private OVRInput.Button button;
    [SerializeField] private OVRInput.Controller controller;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject surface;

    bool gameOver = false;

    void Start()
    {
        gameOver = false;
        gameOverScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    void Update()
    {
        if (!gameOver && OVRInput.GetDown(button, controller))
        {
            ToggleSettingsScreen();
        }

        if (!gameOverScreen.activeSelf && !settingsScreen.activeSelf)
        {
            surface.SetActive(false);
        }
        else
        {
            surface.SetActive(true);
        }
    }


    public void GameOver()
    {
        gameOver = true;
        settingsScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    private void ToggleSettingsScreen()
    {
        settingsScreen.SetActive(!settingsScreen.activeSelf);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
