using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;                 //Velocidade
    public Rigidbody2D theRB;               //Colisões "Solido"
    public float rangeToChasePlayer;        //DDistancia para seguir(atacar) player
    private Vector3 moveDirection;          //Informar direção
    public Animator anim;                   //Animação
    public int health = 150;                //Vida representada por números inteiros
    public GameObject[] deathSplatter;      //Corpo morto no cenário "[]" array para radomizar o corpo
    public GameObject hitEffect;            //Particulas quando toma bala
    public bool shouldShoot;                //Para verificar se o inimigo é o que atira ou não
    public GameObject bullet;               //Balas inimigos
    public Transform firePoint;             //Ponto que sai a bala
    public float fireRate;                  //Cadencia
    private float fireCounter;              //Velocidade dos disparos
    public SpriteRenderer theBody;          //Cria um espaço para adicionar o corpo ao corpo dentro do unity
    public float shootRange;                //Distância dos disparos

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //(inimigo para de atirar quando vc morre)
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)    //Verificar se o inimigo está na tela e se o player está na cena
        {
            //Vai veificar a posição do Inimigo e a do Player
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize(); //Para não somar as duas direções e ele não andar mais rapido que o normal
                                       //Velocidade do inimigo 
            theRB.velocity = moveDirection * moveSpeed;

            //Verifica se pode atirar
            //Condição que vai verificar a distancia entre o player e o inimigo.
            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            { 
                //Atira
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(13);   //Elemento 13 = som de disparo
                    //fireCounter = Time.time;
                }
            }
        }
        else
        {
            //Inimigo para de andar quando player morre
            theRB.velocity = Vector2.zero;
        }
        
        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);     //Escrever do mesmo jeito que no unity
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        //Para inimigo virar para o jogador
        if (PlayerController.instance.gameObject.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
    //Atribuir dano ao inimigo
    public void DamageEnemy(int damage)
    {
        health -= damage;

        AudioManager.instance.PlaySFX(2);       //Elemento 2 = Som de dano 

        Instantiate(hitEffect, transform.position, transform.rotation);

        //Para de dar dano quando for menor ou igual a 0
        if(health <= 0)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(1);   //Elemento 1 = Som de morte de esqueleto

            //vai selecionar um splatter aleatorio entre os elementos 0 e 3 que é o maior valor
            int selectedSplatter = Random.Range(0, deathSplatter.Length);
            int rotation = Random.Range(0, 3); //cria um valor aleatorio para variavel rotation entre 0 e 3
            Instantiate(deathSplatter[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f)); //o valor calculado será multiplicado para obter uma orientação diferente

            //Instantiate(deathSplatter, transform.position, transform.rotation); //Utilizado qunado se tem apenas um splatter
        }
    }
}
