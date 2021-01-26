using Assets.Scripts.HMS;
using Assets.Scripts.HMS.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    public Text score;
    private int playerScore;
    float vertical, horizontal;
    public int speed;

    public Text scoreLabel;

    public Joystick joystick;

    public GameObject losePanel;

    private bool isPaused;

    void Start()
    {
        //anim = GetComponent<Animator>();
        playerScore = 0;
        Time.timeScale = 1;

        GameObject.Find("version").GetComponent<Text>().text = HMSManager.Instance.remoteConfig.GetValueAsString("version");

    }

    void Update()
    {

        /*if (vertical != 0 && horizontal != 0)
        {
            anim.SetBool("isSwimming", true);
        }
        else
        {
            anim.SetBool("isSwimming", false);
        }

        /*if (input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }*/

        score.text = "Score : " + playerScore;
    }

    void FixedUpdate()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;
        //Debug.Log($"{TAG} , {vertical} - {horizontal}");

        if (vertical != 0 || horizontal != 0)
        {
            transform.up = new Vector3(horizontal * speed, vertical * speed, 0);
            transform.Translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime, Space.World);
        }
    }

    public void TakeDamage()
    {
        //Destroy(gameObject);
        Time.timeScale = 0;

        scoreLabel.text = "Score : " + playerScore;
        losePanel.SetActive(true);

        HMSManager.Instance.CheckGamePlayTimeAchievements(playerScore);
        HMSManager.Instance.gameService.SendScore(playerScore, GameServiceConstants.leaderboard_best_scores);

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
