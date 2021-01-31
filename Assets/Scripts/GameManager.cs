using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void Win()
    {
        GameObject.FindObjectOfType<Player>().gameObject.SetActive(false);
        WinScreen.SetActive(true);
    }

    public void Lose()
    {
        GameObject.FindObjectOfType<Player>().gameObject.SetActive(false);
        LoseScreen.SetActive(true);
    }

    public void BackToTitleScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
