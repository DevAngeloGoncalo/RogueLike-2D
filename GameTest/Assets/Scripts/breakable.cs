using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    //Array para ter mais de um sprate
    public GameObject[] brokenPieces;       //Variavel para os sprates
    public int maxPieces = 5;               //Para dropar 5 peças
    public bool shouldDropItem;             //Se pode dropar
    public GameObject[] itemsToDrop;        //Itens que vão dropar
    public float itemDropPercent;           //Chance de dropar
    //public int breakSound;                //Possivel implementação para organizar melho caso tenha muitos itens quebraveis
    //Minhas formas
    public int boxHelth = 75;              //Vida caixa de 100

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Quebrar caixa
    /*private void OnCollisionEnter2D(Collider other)
    {
        if (other.tag == "Player")
        {
            //Se o dash counter é maior que 0 ele ainda está no dash
            if (PlayerController.instance.dashCounter > 0)
            {
                Destroy(gameObject);

                int piecesToDrop = Random.Range(0, maxPieces);              //Número de peças aleatório

                //Loop maroto para gerar aleatóriedade no drop
                for (int i = 0; i < piecesToDrop; i++)
                {
                    int randomPieces = Random.Range(0, brokenPieces.Length);    //Peças aleatórias
                    Instantiate(brokenPieces[randomPieces], transform.position, transform.rotation);   //Jogar o sprate onde estava a caixa
                }
            }
        }
    }*/

    //Função de quebra
    public void smash()
    {
        Destroy(gameObject);

        AudioManager.instance.PlaySFX(0);   //Elemento 0 no unity que indica som de caixa quebrando
                                            //Show broken pieces
                                            //Mostra objetos quebrados
        int piecesToDrop = Random.Range(0, maxPieces);              //Número de peças aleatório

        //Loop maroto para gerar aleatóriedade no drop
        for (int i = 0; i < piecesToDrop; i++)
        {
            int randomPieces = Random.Range(0, brokenPieces.Length);    //Peças aleatórias
            Instantiate(brokenPieces[randomPieces], transform.position, transform.rotation);   //Jogar o sprate onde estava a caixa 
        }
        //drop itens
        if (shouldDropItem)
        {
            float dropChance = Random.Range(0f, 100f);

            if (dropChance < itemDropPercent)
            {
                int randomItem = Random.Range(0, itemsToDrop.Length);

                Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
            }
        }
    }
    //Destruir objetos ao tocar
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            smash();
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
        {
            //Se o dash counter é maior que 0 ele ainda está no dash
            if (PlayerController.instance.dashCounter > 0)
            {
                smash();
            }
        }
    }
    public void DamageBox(int damage)
    {
        boxHelth -= damage;

        AudioManager.instance.PlaySFX(2);       //Elemento 2 = Som de dano 

        //Para de dar dano quando for menor ou igual a 0
        if (boxHelth <= 0)
        {
            smash();
        }
    }

    //Antigo Udemy
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Se o dash counter é maior que 0 ele ainda está no dash
            if(PlayerController.instance.dashCounter > 0)
            {
                smash();
            }
        }

        if(other.tag == "PlayerBullet")
        {
            smash();
        }
    }*/
}
