using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "Player") && (collision.gameObject.GetComponent<CharacterController2D>().hasFuelTaken))
        {
            //FINISH GAME
            //GAME MANAGER TAKES CONTROL
            GameObject.Find("GameManager").GetComponent<GameManager_Script>().GameFinished();
        }
    }


    public void ActivateEndScreen()
    {
        GameObject.Find("GameManager").GetComponent<GameManager_Script>().EndScreenVisible();
    }
}
