using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject inGameScene;
    [SerializeField] private GameObject gameOverScene;
    [SerializeField] private GameObject winningScene;


    private void Start()
    {
        inGameScene.SetActive(true);
        gameOverScene.SetActive(false);
        winningScene.SetActive(false);

        Time.timeScale = 1;
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);   
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
