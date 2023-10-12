using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{

	public MasterInputActions inputActions;
	public float dmgValue = 4;
	public Transform attackCheck;
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
	public bool isTimeToCheck = false;

	public Transform firePoint;
	public Transform firePoint_Crouched;
	
	public GameObject bullet_GO;
	public GameObject chargedBullet_GO;
	public GameObject cam;



	[SerializeField]
	bool isChargingButtonPressed = false;
	[SerializeField]
	bool isChargingBeam = false;
	[SerializeField]
	bool isCharged = false;
	[SerializeField]
	float chargingCounter = 0;
	[SerializeField]
	float chargingTimeSpan = 1.5f;

	public AudioSource audioSource;
	public AudioClip basicShot_Sound;
	public AudioClip shootCharged_Sound;

	//STRINGS
	[Header("Strings")]
	[SerializeField]
	string shoot = "Shoot";
	string jump = "Jump";
	string dash = "Dash";
	string isCharging_Bool = "IsCharging";
	

	//
	PlayerMovement_Script playerMovement_Script_;
	CharacterController2D characterController2D_;

	private void Awake()
	{
		animator = gameObject.GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		playerMovement_Script_ = gameObject.GetComponent<PlayerMovement_Script>();
		characterController2D_ = gameObject.GetComponent<CharacterController2D>();
		
		audioSource = gameObject.GetComponent<AudioSource>();


	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//if (Input.GetKeyDown(KeyCode.X) && canAttack)
		//{
		//	canAttack = false;
		//	animator.SetBool("IsAttacking", true);
		//	StartCoroutine(AttackCooldown());
		//}

		//if (Input.GetKeyDown(KeyCode.V))
		//{
		//	GameObject throwableWeapon = Instantiate(throwableObject, shootLauncher.transform.position, Quaternion.identity) as GameObject; 
		//	Vector2 direction = new Vector2(transform.localScale.x, 0);
		//	throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction; 
		//	throwableWeapon.name = "ThrowableWeapon";
		//}


		if((isChargingButtonPressed) && (characterController2D_.powerUpsTaken >= 3) && (!isCharged))
        {
			chargingCounter += Time.deltaTime;
			isChargingBeam = true;
			
			animator.SetBool(isCharging_Bool, true);
			

			if(chargingCounter >= chargingTimeSpan)
            {
				isCharged = true;
				isChargingBeam = false;
				animator.SetBool("IsCharged", true);
				animator.SetLayerWeight(1, 1);
				
				Debug.Log("Laser Charged");
            }

        }


		//SHOOT CHARGED
		if((!isChargingButtonPressed) && (isCharged))
        {
			ShootCharged();
			
			chargingCounter = 0;
			isCharged = false;
			isChargingBeam = false;
			
			
			animator.SetBool(isCharging_Bool, false);
			animator.SetBool("IsCharged", false);
				animator.SetLayerWeight(1, 0);
		}
		else if((!isChargingButtonPressed) && (!isCharged))
        {
			chargingCounter = 0;
			isChargingBeam = false;
			
			
			animator.SetBool(isCharging_Bool, false);
		}
	}

	private void MeleeAttack()
	{
		//Attack Animation
		canAttack = false;
		animator.SetBool("IsAttacking", true);
		StartCoroutine(AttackCooldown());
	}

	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(0.25f);
		canAttack = true;
	}

	private void ShootBullet()
	{
		//ONLY SHOOT IF POWER UP TAKEN PROPERLY
		if(characterController2D_.powerUpsTaken >= 2)
        {
			audioSource.clip = basicShot_Sound;
			audioSource.Play();

			if (!playerMovement_Script_.crouch)
			{//SHOOT ANIMATION AND INSTANTIATE
				animator.SetTrigger(shoot);
				GameObject bullet = Instantiate(bullet_GO, firePoint.transform.position, firePoint.rotation) as GameObject;
				bullet.name = "Bullet";
			}
			else
			{
				GameObject bullet = Instantiate(bullet_GO, firePoint_Crouched.transform.position, firePoint_Crouched.rotation) as GameObject;
				bullet.name = "Bullet";
			}
		}
		
		
	}

	private void ShootCharged()
    {
		if(characterController2D_.powerUpsTaken >= 3)
        {
			animator.SetTrigger(shoot);
			GameObject chargedBullet = Instantiate(chargedBullet_GO, firePoint.transform.position, firePoint.rotation) as GameObject;

			chargedBullet.name = "ChargedBullet";

			
			audioSource.clip = shootCharged_Sound;
			audioSource.loop = false;
			audioSource.Play();
		}
		
    }



	

	public void DoDashDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
				{
					dmgValue = -dmgValue;
				}
				collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
				cam.GetComponent<CameraFollow>().ShakeCamera();
			}
		}
	}

	void OnMeleeAttack(InputValue buttonValue)
    {
        MeleeAttack();
    }

   

    public void OnShoot(InputValue buttonValue)
    {
       

        //CHARGED SHOT?
        if (buttonValue.isPressed)
        {
            //Debug.Log("Estoy pulsando");
            isChargingButtonPressed = true;

			//DE ESTA MANERA NO DISPARA DOS VECES AL DISPARAR CARGADO.
			Shoot();
		}
        else
        {
            //Debug.Log("He soltado");
            isChargingButtonPressed = false;
        }
    }

    public void Shoot()
    {
        if (!isCharged)
        {
            ShootBullet();
        }
        else
        {
            ShootCharged();
        }
    }



    //void OnChargeShot(InputValue buttonValue)
    //   {
    //	ShootCharged();
    //	Debug.Log("disparo cargado!");

    //	if(buttonValue.isPressed)
    //       {

    //       }
    //   }


}
