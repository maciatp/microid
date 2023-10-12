using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
//using UnityEngine.WSA;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Script : MonoBehaviour
{
    public CharacterController2D characterController2D;

    public GameObject titleScreen;
    public GameObject settingsScreen;
    public GameObject gameScreen;
    public GameObject endScreen;
    public GameObject pauseScreen;
    public GameObject objectivesScreen;
    public GameObject creditsScreen;


    public GameObject touchControls;
   

    //public new List<GameObject> enemiesExtra = new List<GameObject>();
    public GameObject enemiesExtra;

    public GameObject face_Extra;

    public GameObject UI_Fuel;

    public GameObject fuelObtainedText_GO;
    public GameObject objectivesFloating;

    public AudioClip fuel_Sound;
    public AudioClip title_Sound;
    public AudioClip endGame_Sound;
    public AudioSource managerAudioSource;

    private IEnumerator coroutine; //??? es necesario?

    private void Awake()
    {
        //EXTRA FACE PREPARING
        if(face_Extra.gameObject.activeSelf != false)
        {
            face_Extra.gameObject.SetActive(false);
        }

        //TITLE SCREEN
        if(titleScreen.gameObject.activeSelf != false)
        {
            titleScreen.gameObject.SetActive(false);
            
        }
        //SETTINGS SCREEN
        if(settingsScreen.gameObject.activeSelf != false)
        {
            settingsScreen.gameObject.SetActive(false);
        }
        //END SCREEN
        if(endScreen.gameObject.activeSelf != false)
        {
            endScreen.gameObject.SetActive(false);
        }
        //GAME SCREEN
        if (gameScreen.transform.gameObject.activeSelf != false)
        {
            gameScreen.transform.gameObject.SetActive(false);
        }
        //PAUSE SCREEN
        if(pauseScreen.activeSelf != false)
        {
            pauseScreen.SetActive(false);
        }

        //fuel UI
        if (UI_Fuel.GetComponent<UnityEngine.UI.Image>().color != Color.black)
        {
            UI_Fuel.GetComponent<UnityEngine.UI.Image>().color = Color.black;


        }
        if (UI_Fuel.activeSelf != true)
        {
            UI_Fuel.SetActive(true);
        }

        if(fuelObtainedText_GO.activeSelf != false)
        {
            fuelObtainedText_GO.SetActive(false);
        }

        if(objectivesScreen.activeSelf != false)
        {
            objectivesScreen.SetActive(false);
        }
        if(creditsScreen.activeSelf != false)
        {
            creditsScreen.SetActive(false);
        }

        if(objectivesFloating.activeSelf != false)
        {
            objectivesFloating.SetActive(false);
        }



        //TOUCH CONTROLS UI
        if (gameScreen.transform.GetChild(1).gameObject.activeSelf == true)
        {
            gameScreen.transform.GetChild(1).gameObject.SetActive(false);
        }


        characterController2D = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        characterController2D.gameObject.GetComponent<PlayerInput>().enabled = false;

        managerAudioSource = gameObject.GetComponent<AudioSource>();


        //LAUNCH GAME FUNCTION
        LaunchGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController2D.powerUpsTaken >= 2)
        {
            enemiesExtra.gameObject.SetActive(true);
            GameObject.Find("Banner_Text").GetComponent<TMPro.TextMeshPro>().text = "You are going left!";
        }
        if ((characterController2D.powerUpsTaken >= 3) && (face_Extra != null))
        {
            face_Extra.gameObject.SetActive(true);
        }

        //if((characterController2D.hasFuelTaken == true) && (UI_Fuel.GetComponent   //!= Color.white))
        //{
        //    //UI_Fuel.GetComponent<Image>().tintColor = Color.white;
        //}

    }

    public void FuelTaken()
    {
        UI_Fuel.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        fuelObtainedText_GO.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(8).GetComponent<AudioSource>().clip = fuel_Sound;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(8).GetComponent<AudioSource>().pitch = 1;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(8).GetComponent<AudioSource>().Play();
    }


    public void LaunchGame()
    {
        titleScreen.SetActive(true);
       characterController2D.gameObject.GetComponent<PlayerInput>().enabled = false;

        managerAudioSource.clip = title_Sound;
        managerAudioSource.Play();
    }

    public void StartGame()
    {
        if(objectivesScreen.activeSelf != false)
        {
            objectivesScreen.SetActive(false);
        }



        titleScreen.SetActive(false);
        endScreen.SetActive(false);
        managerAudioSource.Stop();

        //DOY CONTROL AL JUGADOR
        characterController2D.enabled = true;
        characterController2D.gameObject.GetComponent<PlayerInput>().enabled = true;

        //ACTIVO EL CONTADOR QUE HArÁ DESAPARECER A OBJECTIVESFLOATING

        coroutine = ObjectivesFloatingDisappear(3f);
        StartCoroutine(coroutine);

        //ACTIVO HUD
        gameScreen.gameObject.SetActive(true);
        GameObject.Find("UI_PowerUp1").gameObject.GetComponent<Animator>().enabled = true;
        GameObject.Find("UI_PowerUp2").gameObject.GetComponent<Animator>().enabled = true;
        GameObject.Find("UI_PowerUp3").gameObject.GetComponent<Animator>().enabled = true;
        gameScreen.transform.GetChild(1).gameObject.SetActive(true); //Esto es para touch controls?? comprobar

#if UNITY_IOS
        touchControls.gameObject.SetActive(true);
        Debug.Log("IPHONe");

#else
        touchControls.gameObject.SetActive(false);
#endif
        //if (Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    touchControls.gameObject.SetActive(false);
        //}
        //else if (Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    touchControls.gameObject.SetActive(true);
        //}
    }

    private IEnumerator ObjectivesFloatingDisappear(float objectivesTime)
    {
        objectivesFloating.SetActive(true);

        objectivesFloating.gameObject.GetComponent<Animator>().enabled = false;

        yield return new WaitForSeconds(objectivesTime);

        objectivesFloating.gameObject.GetComponent<Animator>().enabled = true;

    }


    public void OpenObjectivesScreen()
    {
        if(objectivesScreen.activeSelf == true)
        {
            objectivesScreen.SetActive(false);
        }else
        {
            objectivesScreen.SetActive(true);

        }
            
    }

    public void OpenCreditsScreen()
    {
        creditsScreen.SetActive(true);
        titleScreen.SetActive(false);

        if(objectivesScreen.activeSelf != false)
        {
            objectivesScreen.SetActive(false);
        }
    }

    public void OpenSettings()
    {
        if(objectivesScreen.activeSelf != false)
        {
            objectivesScreen.SetActive(false);
        }

        titleScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void GoBackToMenu()
    {
        titleScreen.SetActive(true);
        settingsScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void ResetScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Microid_Scene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void GameFinished()
    {
        //WHEN PLAYER MOVES TO SHIP'S TRIGGER WITH FUEL
        Debug.Log("GAME FINISHED");

        characterController2D.gameObject.SetActive(false);
        GameObject.Find("Ship").GetComponent<Animator>().SetBool("GameFinished", true);
        GameObject.Find("Ship").GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        GameObject.Find("Ship").GetComponent<SpriteRenderer>().sortingOrder = 15;
        gameScreen.SetActive(false);

        managerAudioSource.clip = endGame_Sound;
        managerAudioSource.Play();

    }

    public void EndScreenVisible()
    {
        endScreen.gameObject.SetActive(true);
        characterController2D.gameObject.GetComponent<PlayerInput>().enabled = false;
    }
}
