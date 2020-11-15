using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    //Objetivo de reconhecer comando de tecla para prosseguir.

    public float waitForAnyKey = 2f;    //Após 2 sec poderá apertar

    public GameObject anyKeyText;

    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitForAnyKey > 0)
        {
            waitForAnyKey -= Time.deltaTime; //Contador para diminuir os 2 segundos de espera

            if(waitForAnyKey <= 0)
            {
                anyKeyText.SetActive(true);
            }
        }
        else
        {
            //Volta para o menu

            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScene);    
            }
        }
    }
}
