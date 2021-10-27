using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;
    private int lastScore;

   public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }
    private bool isGameStarted = false;
    private PlayerMotor motor;

    public Text scoreText, coinText, modifierText;
    private float score, coinScore, modifierScore;

    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {
        modifierScore = 1;
        Instance = this;
       // UpdateScores();
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString("0");
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    private void Update()
    {
        if (MobileInput.Instance.Tap & !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
        }

        if (isGameStarted && !IsDead)
        {
    
            score += (Time.deltaTime * modifierScore);
            if (lastScore != (int)score){
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
           
        }
    }

    public void GetCoin()
    {
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");
    }

   /* public void UpdateScores()
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
        modifierText.text =  "x" + modifierScore.ToString("0.0");
    }*/

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnDeath()
    {
        IsDead = true;
        deadScoreText.text = score.ToString("0");
        deadCoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
    }
}  
