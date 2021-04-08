using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void JumpScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void unPauseGame()
    {
        PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerScript.unPauseGame();
    }

    public void showPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void hidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

}
