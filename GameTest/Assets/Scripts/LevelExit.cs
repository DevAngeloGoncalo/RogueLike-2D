using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Biblioteca para acessar as cenas  
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    //Utilizado para trocar de cena quando completar o nível

    public string levelToLoad;  //Carregar o nivel


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Só vai ativar quando o player entrar na Trigger Area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Não é necessário pois agora se tem um gerenciador de troca de cenas
            //SceneManager.LoadScene(levelToLoad);

            StartCoroutine(LevelManager.instance.LevelEnd()); 
        }
    }
}
