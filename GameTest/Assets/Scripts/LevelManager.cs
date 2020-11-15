using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;    //Para ter acesso de outros scripts
    public float waitToLoad = 4;            //Tempo para load
    public string nextLevel;                //Nome proximo nivel

    public bool isPaused;                    //Pausado true

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))    //Caso o Esc seja pressionado
        {
            PauseUnpause();
        }
    }

    //IEnumerator é como uma rotina, espera um certo tempo para executar algo
    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayLevelWin();

        //Para não se mover
        PlayerController.instance.canMove = false;
        
        //Transição
        UIController.instance.StartFadeToBlack(); 

        //Espera a quantidade de segundos para ir para o próximo nivel
        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(nextLevel);
    }

    //Para o jogo congelar quando estiver pausado
    public void PauseUnpause()
    {
        if(!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);    //Mostra o menu de pausa

            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);   //Dispausa

            isPaused = false;

            Time.timeScale = 1f;
        }
    }
}
