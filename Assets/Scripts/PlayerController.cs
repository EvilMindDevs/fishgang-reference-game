using HmsPlugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text score;
    private int playerScore;
    float vertical, horizontal;
    public int speed;

    public Text scoreLabel;

    public Joystick joystick;

    public GameObject losePanel;

    void Start()
    {
        playerScore = 0;
        Time.timeScale = 1;
    }

    void Update()
    {
        score.text = "Score : " + playerScore;
    }

    void FixedUpdate()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;

        if (vertical != 0 || horizontal != 0)
        {
            transform.up = new Vector3(horizontal * speed, vertical * speed, 0);
            transform.Translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime, Space.World);
        }
    }

    public void TakeDamage()
    {
        Time.timeScale = 0;

        scoreLabel.text = "Score : " + playerScore;
        losePanel.SetActive(true);

        CheckGamePlayTimeAchievements(playerScore);
    }
     
    public void CheckGamePlayTimeAchievements(int score)
    {
        if (score >= 5)
        {
            HMSAchievementsManager.Instance.UnlockAchievement(HMSAchievementConstants.level_1);
        } else if (score >= 100)
        {
            HMSAchievementsManager.Instance.UnlockAchievement(HMSAchievementConstants.level_2);
        } else if (score >= 200)
        {
            HMSAchievementsManager.Instance.UnlockAchievement(HMSAchievementConstants.level_3);
        }
    }

    public void raiseScore()
    {
        playerScore += 1;
    }

    public void unPauseGame()
    {
        losePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
