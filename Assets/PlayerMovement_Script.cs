using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Script : MonoBehaviour
{
    public MasterInputActions inputActions;
    public CharacterController2D characterController2D;
    public Joystick joystick;


    //public static porque quiero que se pueda acceder no a esta variable en concreto, sino a la referencia estática desde todas las clases. Por eso la primera va en mayus?
    public static bool GameIsPaused = false;
    public GameObject UIpauseScreen;

    [SerializeField]
    private Vector2 moveInput;
    [SerializeField]
    private Vector2 moveDirection;

    public float runSpeed = 40;
    [SerializeField]
    public bool jump = false;
    [SerializeField]
    public bool crouch = false;
    public bool isBall = false;
    [SerializeField]
    bool dash = false;

    public Animator player_Animator;


    public AudioSource footStepsAudioSource;
    

    [Header("STRINGS")]

    string joystickY = "JoystickY";
    string isCrouching = "IsCrouched";
    string isBall_String = "IsBall";

    private void Awake()
    {
        characterController2D = gameObject.GetComponent<CharacterController2D>();
        player_Animator = gameObject.GetComponent<Animator>();
        // inputActions = gameObject.GetComponent<PlayerInput>();

        footStepsAudioSource = gameObject.transform.GetChild(7).GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ////TOUCH CONTROLS -> PERO HACE QUE NO RESPONDA EL TECLADO
        //if (joystick.Horizontal >= 0.2f)
        //{
        //    moveInput.x = 1;
        //}
        //else if (joystick.Horizontal <= -0.2f)
        //{
        //    moveInput.x = -1;
        //}
        //else //NO MOVEMENT
        //{
        //    moveInput.x = 0;
        //}


        moveDirection.x = moveInput.x * runSpeed;
        moveDirection.y = moveInput.y;
        player_Animator.SetFloat("Speed", Mathf.Abs(moveDirection.x));
        player_Animator.SetFloat(joystickY, moveDirection.y);


        if(moveDirection.y < -0.1f)
        {
            if(!crouch)
            {
                crouch = true;
                moveDirection.x = 0;
                player_Animator.SetBool(isCrouching, true);
            }
            //else //APLAZADO
            //{
            //    //morph ball
            //    isBall = true;
            //    player_Animator.SetBool(isBall_String, true);

            //}
        }
        else if((moveDirection.y >= 0.1f) || (Mathf.Abs(moveDirection.x) > 0.1f))
        {
            crouch = false;
            player_Animator.SetBool(isCrouching, false);
        }


    }

    //PAUSE MENU

    public void Resume()
    {
        UIpauseScreen.SetActive(false);
        player_Animator.enabled = true;
        Time.timeScale = 1;
        GameIsPaused = false;

        GameObject.Find("Pause_Button").transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Pause()
    {
        
        UIpauseScreen.SetActive(true);
        player_Animator.enabled = false;
        Time.timeScale = 0f;
        GameIsPaused = true;

        GameObject.Find("Pause_Button").transform.GetChild(1).gameObject.SetActive(false);
    }


    public void OnFall()
    {
        player_Animator.SetBool("IsJumping", true);
    }
    public void OnLanding()
    {
        //SET LANDING ANIMATION
        player_Animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        //SET CROUCHING ANIMATION
    }

    //SOUNDS

    public void StepSound()
    {
        footStepsAudioSource.Play();
    }


    //CONTROLS

    void OnMove(InputValue joystickValue)
    {
        moveInput = joystickValue.Get<Vector2>();
        //Debug.Log(moveInput);
    }
    public void OnJump(InputValue buttonValue)
    {
        Jump();
    }

    public void Jump()
    {
        if (characterController2D.powerUpsTaken >= 1)
        {
            jump = true;
        }
    }

    void OnDash(InputValue buttonValue)
    {
        dash = true;
    }
    private void FixedUpdate()
    {
        characterController2D.Move(moveDirection.x * Time.fixedDeltaTime, jump, dash);
        jump = false;
        dash = false;
       
    }
    void OnPause(InputValue buttonValue)
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

}
