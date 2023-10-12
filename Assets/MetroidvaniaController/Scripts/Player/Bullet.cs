using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector2 direction;
	public bool hasHit = false;
	public float speed = 10f;
	
	public float damage = 1f;

	public Rigidbody2D bullet_Rb;

	public GameObject bulletImpactFX_GO;

    private void Awake()
    {
		bullet_Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
		bullet_Rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		//if ( !hasHit)
		//GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//Instancio la explosión de la bala
		GameObject bulletImpact = Instantiate(bulletImpactFX_GO, gameObject.transform.position, transform.rotation, null);

		if (collision.gameObject.tag == "Enemy")
		{
			//OLD CODE
			//collision.gameObject.SendMessage("ApplyDamage", Mathf.Sign(direction.x) * 2f);
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}
	}
}
