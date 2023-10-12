using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Object_Script : MonoBehaviour
{
    [SerializeField]
    int health = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            health--;
            if(health <= 0)
            {
                gameObject.transform.parent.parent.GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
        }
    }
}
