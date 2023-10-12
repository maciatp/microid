using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu_Script : MonoBehaviour
{
    
    //public static porque quiero que se pueda acceder no a esta variable en concreto, sino a la referencia estática desde todas las clases. Por eso la primera va en mayus?
    public static bool GameIsPaused = false;

    public GameObject UIpauseMenu;


    private void Awake()
    {
       
    }

    private void Update()
    {
        
    }

    public void Resume()
    {
        //UIpauseMenu.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Pause()
    {
        //UIpauseMenu.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }



    void OnPause(InputValue buttonValue)
    {
        if(GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
