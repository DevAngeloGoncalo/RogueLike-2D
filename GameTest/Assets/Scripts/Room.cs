using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered, openWhenEnemiesCleared;       //Portas se fecham ao entrar no ambiente e se abrem quando não tem inimigos
    public GameObject[] doors;          //Para portas
    //Listas são melhores para inimigos
    public List<GameObject> enemies = new List<GameObject>();   //Listar inimigos no room
    //private bool roomActive;    //Verificar quartos ativos

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verificar se os inimigos ja foram mortos
        //Count é usado em Listas, Length em array
        if(enemies.Count > 0 && /*(roomActive = true) && */openWhenEnemiesCleared)
        {
            //Verifica se todos os inimigos
            for(int i = 0; i < enemies.Count; i++)
            {
                //Caso seja nulo a posição, será removido
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;    //Usado para diminuir os elementos vazios do unity
                }
            }

            //Caso a contagem de inimigos for 0 porta liberada
            if(enemies.Count == 0)
            {
                foreach (GameObject door in doors)
                {
                    //Desativa porta na cena
                    door.SetActive(false);

                    //Para portas não fecharem quando voltarmos para o quarto
                    closeWhenEntered = false;
                }
            }
        }
        
    }

    //Para identificar quando o player entra na sala
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag ==  "Player")
        {
            CameraController.instance.ChangeTarget(transform);

            //Para fechar porta
            if(closeWhenEntered == true)
            {
                //Precisar de um loop para sempre verificar
                //Vai percorrer todas as portas
                foreach(GameObject door in doors)
                {
                    //Ativa porta na cena
                    door.SetActive(true);
                }
            }
            //Quarto ativo
            //roomActive = true;
        }
    }

    //Desativa o quarto quando jogador sair do quarto
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //roomActive = false;
        }
    }
}
