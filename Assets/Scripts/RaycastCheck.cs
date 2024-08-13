using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RaycastCheck : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    public void CheckWin()
    {
        /*
            //randomly select the next level
            levelCount++;
            if(levelCount <= 3)
            {
                currentLevel = Random.Range(0, 3);
            }
            else if(levelCount > 3 && levelCount <= 6)
            {
                currentLevel = Random.Range(3, 6);
            }
            else if(levelCount > 6 && levelCount <= 9)
            {
                currentLevel = Random.Range(6, 9);
            }
            else
            {
                currentLevel = Random.Range(6, 9);
            }
       */
    }

    public void CheckIfHit()
    {
        FireBullet.FireNow.Invoke();
    }

}
