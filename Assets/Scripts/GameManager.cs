using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] int scoreCapToGainBall = 25000;
    int nextBall;
    private int multiplicator = 1;
    private int maxMultiplicator = 20;
    [SerializeField] private int balls = 5;
    private bool noMoreBalls = false;

    [SerializeField] int multiplicatorJackport = 11;
    private bool jackpotActive = false;
    Sprite[] initialJackpotSprites;
    SpriteRenderer[] jackpotSpriteRenderers;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text multiplicatorText;
    [SerializeField] TMP_Text ballsLeftText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] GameObject jackpot;

    [SerializeField] private UnityEvent resetEvent;

    // Start is called before the first frame update
    void Start()
    {
        nextBall = scoreCapToGainBall;
        setHUDTexts();

        jackpotSpriteRenderers = jackpot.GetComponentsInChildren<SpriteRenderer>();
        initialJackpotSprites = new Sprite[jackpotSpriteRenderers.Length];
        for (int i = 0; i < initialJackpotSprites.Length; i++)
        {
            initialJackpotSprites[i] = jackpotSpriteRenderers[i].sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        setHUDTexts();

        if (score >= nextBall)
        {
            balls += 1;
            nextBall += scoreCapToGainBall;
        }

        if (multiplicator >= multiplicatorJackport && !jackpotActive)
        {
            startJackpot();
            jackpotActive = true;
        }
    }

    private void setHUDTexts()
    {
        scoreText.text = score.ToString();
        multiplicatorText.text = "X" + multiplicator.ToString();
        ballsLeftText.text = balls.ToString();
        if (noMoreBalls)
        {
            gameOverText.text = "GAME OVER";
        }
        else
        {
            gameOverText.text = "";
        }
    }

    public void loseBall()
    {
        if (!noMoreBalls)
        {
            balls -= 1;
            restartTurn();
            if (balls <= 0)
            {
                noMoreBalls = true;
                Debug.Log("Player has lost");
            }
            else
            {
                Debug.Log("Player lost 1 ball: " + balls + " left");
            }
        }
    }

    public bool hasNoMoreBalls()
    {
        return noMoreBalls;
    }

    public void augmentScore(int amount)
    {
        score += amount * multiplicator;
    }

    public void augmentMultiplicator(int amount)
    {
        multiplicator += amount;
        if (multiplicator > maxMultiplicator)
        {
            multiplicator = maxMultiplicator;
        }
    }

    public void restartTurn()
    {
        multiplicator = 1;
        if (jackpotActive)
        {
            jackpotActive = false;
            stopJackpot();
        }
        resetEvent.Invoke();
    }

    private void startJackpot()
    {
        jackpot.GetComponent<demoLetterCycle>().StartCoroutine("CycleText");
    }

    private void stopJackpot()
    {
        jackpot.GetComponent<demoLetterCycle>().StopAllCoroutines();

        for (int i = 0; i < initialJackpotSprites.Length; i++)
        {
            jackpotSpriteRenderers[i].sprite = initialJackpotSprites[i];
        }
    }
}
