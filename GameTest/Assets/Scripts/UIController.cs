using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI; //Para acesssar UI no UNITY
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public static UIController instance;    //Para ter acesso de outros scripts

    //Referências UI elements
    public Slider healthSlider;
    public Text healthText;

    public GameObject deathScreen;

    public Image fadeScreen;    //Jogo de fade com imagem preta ao mudar de cena
    public float fadeSpeed;     //Velocidade da transição
    private bool fadeToBlack, fadeOutBlack;  //Transição    

    public string newGameScene, mainMenuScene;  //Botões para tela de morte

    public GameObject pauseMenu;    //Referência ao pause

    private void Awake() //Serve bem para ativar e desativar itens no jogo
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if(fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene); 
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
