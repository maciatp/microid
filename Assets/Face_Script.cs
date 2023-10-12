using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face_Script : MonoBehaviour
{
    public GameObject explosionFX;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BulletCharged")
        {
            GameObject explosion = Instantiate(explosionFX, gameObject.transform.position, transform.rotation, null);
            Destroy(gameObject);
        }
    }
}
