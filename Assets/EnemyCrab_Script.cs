using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab_Script : MonoBehaviour
{
    [SerializeField]
    float enemyHealth = 1;

    public BoxCollider2D crab_Collider;
    public GameObject enemyExplosionGO;

    [SerializeField]
    Animator crabAnimator;

    private void Awake()
    {
        crabAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "Bullet") || (collision.gameObject.tag == "BulletCharged"))
        {
            //APPLY DAMAGE
            enemyHealth -= collision.gameObject.GetComponent<Bullet>().damage;
            crabAnimator.SetTrigger("isHit");

            if(enemyHealth <= 0)
            {
                GameObject crabExplosion = Instantiate(enemyExplosionGO, gameObject.transform.position, gameObject.transform.rotation, null);
                Destroy(gameObject);
            }
        }
    }

   
}
