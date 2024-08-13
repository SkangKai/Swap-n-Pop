using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SwapWithPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform tempPosition;
    static bool winState = true, canClick = true;
    static int swapCount = 1, fireCount = 0;


    public void Start()
    {
        winState = true;
        canClick = true;
        fireCount = 0;
    }
    public int GetSwapCount()
    {
        return swapCount;
    }

    public void SetSwapCount(int x)
    {
        swapCount = x;
    }

    public bool GetWinState()
    {
        return winState;
    }
    public int GetFireCount()
    {
        return fireCount;
    }

    public void SetWinState(bool x)
    {
        winState = x;
    }

    public void SetCanClick(bool x)
    {
        canClick = x;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(swapCount);
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            RaycastHit rayHit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity) && rayHit.transform == this.transform)
            {
                tempPosition.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                tempPosition.rotation = Quaternion.Euler(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z);
                Quaternion tempRotation = player.transform.rotation;

                player.transform.rotation = transform.rotation;
                player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

                transform.rotation = tempRotation;
                transform.position = new Vector3(tempPosition.position.x, transform.position.y, tempPosition.position.z);

                
                
                

                swapCount--;
            }
        }
    }
}
