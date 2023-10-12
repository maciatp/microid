using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trigger_Script : MonoBehaviour
{
    public GameObject gameObjectToActivate1;
    public GameObject gameObjectToActivate2;
    public GameObject gameObjectToActivate3;

    private void Awake()
    {
        if(gameObjectToActivate1.activeSelf != false)
        {
            gameObjectToActivate1.SetActive(false);
        }
        if(gameObjectToActivate2.activeSelf != false)
        {
            gameObjectToActivate2.SetActive(false);
        } 
        if(gameObjectToActivate3.activeSelf != false)
        {
            gameObjectToActivate3.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObjectToActivate1.SetActive(true);
            gameObjectToActivate2.SetActive(true);
            gameObjectToActivate3.SetActive(true);
        }
    }
}
