using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel_Script : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController2D>().hasFuelTaken = true;
            GameObject.Find("GameManager").GetComponent<GameManager_Script>().FuelTaken();
            Destroy(gameObject);
        }
    }
}
