using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable_Script : MonoBehaviour
{
    public BoxCollider2D collectable_Collider;
    public AudioClip powerUp_Sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController2D>().powerUpsTaken ++;

            collision.gameObject.GetComponent<CharacterController2D>().PowerUpCollectedSound( powerUp_Sound);


            if(collision.gameObject.GetComponent<CharacterController2D>().powerUpsTaken == 1)
            {
                GameObject.Find("UI_PowerUp1").gameObject.GetComponent<Image>().color = Color.white;
                

            }

             else if (collision.gameObject.GetComponent<CharacterController2D>().powerUpsTaken == 2)
            {
                GameObject.Find("UI_PowerUp2").gameObject.GetComponent<Image>().color = Color.white;
            }

            else if (collision.gameObject.GetComponent<CharacterController2D>().powerUpsTaken == 3)
            {
                GameObject.Find("UI_PowerUp3").gameObject.GetComponent<Image>().color = Color.white;
            }
            Destroy(gameObject);
        }
    }

}
