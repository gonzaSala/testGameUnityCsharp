using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

     public void ShowGameOverScreen()
    {
        Debug.Log("Showing Game Over Screen");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
    }

    

}
