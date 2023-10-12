using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Script : MonoBehaviour
{
    public Animator door_animator;
    public BoxCollider2D door_Collider;
    public GameObject otherDoor;
    public bool isDoorOpen = false;

    private void Awake()
    {
        door_animator = gameObject.GetComponent<Animator>();
        door_Collider = gameObject.GetComponent<BoxCollider2D>();
        door_animator.enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(((collision.gameObject.tag == "Bullet") || (collision.gameObject.tag == "BulletCharged")) && (isDoorOpen == false))
        {
            //OPEN DOOR
            OpenDoor();


        }
    }

    private void OpenDoor()
    {
        door_animator.enabled = true;
        isDoorOpen = true;
        door_animator.SetBool("IsClosed", false);
        
    }

    public void Door_PlayerCanPass()
    {
        door_Collider.isTrigger = true;
        door_animator.enabled = false;

    }

    public void Door_Closed()
    {
        door_Collider.isTrigger = false;
        door_animator.enabled = false;
        isDoorOpen = false;
        door_animator.SetBool("IsClosed", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "Player") && (otherDoor.GetComponent<Door_Script>().isDoorOpen == false))
        {
            otherDoor.GetComponent<Door_Script>().OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.tag == "Player")
        {
            //CLOSE DOOR
            //door_animator.SetTrigger("CloseDoor");
            door_animator.SetBool("IsClosed", true);
            door_animator.enabled = true;
        }
        

        
    }

}
