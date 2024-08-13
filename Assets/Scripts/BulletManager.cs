using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    Enemy currentEnemy;
    static BulletManager instance;
    [SerializeField] TMP_Text winText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text turnText;
    [SerializeField] TMP_Text scoreText;

    [SerializeField] Button replayButton;

    public List<Bullet> bulletList = new List<Bullet>();
    [SerializeField] List<GameObject> levelList = new List<GameObject>();
    int levelCount = 0;
    int currentLevel = 0;
    int timer = 0, score = 0, thresholdRound = 7;
    bool canIterate = true, startCheckTimer = false, hasBeatenRound = false, cameraZooming = true, notFinalStep = true;
    float timerStart = 5, checkTimer = 0, checkTimerLimit = 3f, cameraTimerLimit = 2f, cameraTimer = 0;
    bool dontSwitch;

    public void start()
    {
        canIterate = true;
        dontSwitch = false;
    }

    public static BulletManager GetInstance()
    {
        return instance;
    }

    public void RegisterBullets(Bullet bullet)
    {
        bulletList.Add(bullet);
    }
    public void NoHit(Bullet toRemove)
    {
        bulletList.Remove(toRemove);
        if (bulletList.Count <= 0)
        {
            hasBeatenRound = true;
        }
    }

    public void Win()
    {
        if(dontSwitch)
        {
            return;
        }
        Debug.Log("Win");
        for (int i = 0; i < levelList.Count; i++)
        {
            if (i == currentLevel)
            {
                levelList[i].SetActive(false);
                break;
            }
        }

        //randomly select the next level
        if(levelCount < 10)
        {
            levelCount++;
            currentLevel++;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }

        //Activate new enemies
        for (int i = 0; i < levelList.Count; i++)
        {
            if (i == currentLevel)
            {
                levelList[i].SetActive(true);
                break;
            }
        }

        //reset values
        score++;
        scoreText.text = "Score: " + score.ToString();
        startCheckTimer = false;
        checkTimer = 0;
        canIterate = true;
        hasBeatenRound = false;
        notFinalStep = true;
        currentEnemy.GetComponent<SwapWithPlayer>().SetCanClick(true);
        turnText.text = "Moves: " + currentEnemy.GetComponent<SwapWithPlayer>().GetSwapCount().ToString();
    }
    public void Loss()
    {
        dontSwitch = true;
        winText.text = "You Lose";
        winText.enabled = true;
        turnText.enabled = false;
        timerText.enabled = false;
        replayButton.gameObject.SetActive(true);
        canIterate = false;
        Debug.Log("Lose");
    }
    public void SetCurrentEnemy(Enemy tryingToDrainMeOfMyEnergy)
    {
        currentEnemy = tryingToDrainMeOfMyEnergy;
    }

    public Enemy GetCurrentEnemy()
    {
        return currentEnemy;
    }

    public int GetCurrentLevel()
    {
        return levelCount;
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
        //currentEnemy.GetComponent<SwapWithPlayer>().SetSwapCount(2);
        turnText.text = "Moves: " + currentEnemy.GetComponent<SwapWithPlayer>().GetSwapCount().ToString();
        scoreText.text = "Score: " + score.ToString();
        turnText.enabled = true;
        scoreText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (cameraZooming)
        {
            cameraTimer += Time.deltaTime;
        }
        if (cameraTimer >= cameraTimerLimit)
        {
            cameraZooming = false;
        }


        if (startCheckTimer)
        {
            checkTimer += Time.deltaTime;
        }


        if (checkTimer >= checkTimerLimit)
        {
            if (hasBeatenRound)
            {
                Win();
            }
        }
        
        if(currentEnemy.GetComponent<SwapWithPlayer>().GetSwapCount() == 1 && currentLevel >= thresholdRound && notFinalStep)
        {
            player.GetComponent<RaycastCheck>().CheckIfHit();
            notFinalStep = false;
        }

        //check if win
        if (currentEnemy.GetComponent<SwapWithPlayer>().GetSwapCount() == 0)
        {
            turnText.text = "Moves: " + currentEnemy.GetComponent<SwapWithPlayer>().GetSwapCount().ToString(); //currrent turn text
            timerStart = 5;
            canIterate = false;
            startCheckTimer = true;
            player.GetComponent<RaycastCheck>().CheckIfHit();

            if (currentLevel >= thresholdRound)
            {
                currentEnemy.GetComponent<SwapWithPlayer>().SetSwapCount(2);
            }
            else
            {
                currentEnemy.GetComponent<SwapWithPlayer>().SetSwapCount(1);
            }
            currentEnemy.GetComponent<SwapWithPlayer>().SetCanClick(false);
        }

        //timer goes here
        if (canIterate && !cameraZooming)
        {
            timerStart -= Time.deltaTime;
            timer = (int)timerStart + 1;
            timerText.text = "Time: " + timer.ToString(); //timer text location
            timerText.enabled = true;
        }

        //timer runs out
        if (timerStart <= 0)
        {
            if(currentEnemy.GetComponent<SwapWithPlayer>().GetSwapCount() == 2)
            {
                Loss();
            }
            if (currentLevel >= thresholdRound)
            {
                currentEnemy.GetComponent<SwapWithPlayer>().SetSwapCount(2);
            }
            else
            {
                currentEnemy.GetComponent<SwapWithPlayer>().SetSwapCount(1);
            }
            player.GetComponent<RaycastCheck>().CheckIfHit();

            currentEnemy.GetComponent<SwapWithPlayer>().SetCanClick(false);
            timerStart = 5;
            startCheckTimer = true;
            canIterate = false;
        }
    }

    public void replayLevel()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("go to Main Menu");
    }

    public void pauseTimer()
    {
        if(canIterate)
        {
            canIterate = false;
        }
        else
        {
            canIterate = true;
        }
    }
}
