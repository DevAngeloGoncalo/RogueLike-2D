using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;                                                    //Para ser acessado de outros scripts
    public float moveSpeed;                                                                     //Velocidade
    private Vector2 moveImput;                                                                  //Valores de Entrada
    public Rigidbody2D theRB;                                                                   //Colisões "Solido"
    public Transform gunArm;                                                                    //MiraArma "Mouse"
    private Camera cam;                                                                         //Otimização para câmera
    public Animator anim;                                                                       //Animação
    public GameObject bulletToFire;                                                             //Atirar
    public Transform firePoint;                                                                 //Definir ponto de onde a bala vai sair
    public float timeBetweenShots;                                                              //Tempo entre disparos
    private float shotCounter;                                                                  //Contagem de Disparos 
    private bool canFire;                                                                       //Pode Atirar
    public SpriteRenderer bodySR;                                                               //Sprite do corpo
    private float activeMoveSpeed;                                                              //velocidade do jogador atual
    public float dashSpeed = 8f, dashLengh = .5f, dashCoolDown = 1f, dashInvincibility = .5f;   //velocidade do dash, distancia, CD e invencibilidade na execução.4

    [HideInInspector]   //Faz com que dashCounter não seja visível no Unity mesmo sendo pública.
    public float dashCounter;
    private float dashCoolCounter;

    [HideInInspector]
    public bool canMove = true;                                                                 //Utilizado para não se mover no fim das fases

    private void Awake() //Serve bem para ativar e desativar itens no jogo
    {
        instance = this;

        activeMoveSpeed = moveSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused) //Não pausado
        {
            //Identifica as direções no plano cartesiano
            moveImput.x = Input.GetAxisRaw("Horizontal");
            moveImput.y = Input.GetAxisRaw("Vertical");

            moveImput.Normalize(); //Para não somar as duas direções e ele não andar mais rapido que o normal

            //Não funciona com colisões (Time.deltaTime é tempo de um frame para o outro)
            //transform.position += new Vector3(moveImput.x * Time.deltaTime *moveSpeed, moveImput.y * Time.deltaTime * moveSpeed, 0f);

            //Funciona para colisões
            theRB.velocity = moveImput * activeMoveSpeed;

            //Encontrar o mouse no game
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

            //Rotação Personagem
            //Para rotacionar um personagem basta definir em transform Scale X para 1 ou -1
            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one; //Mesma coisa que Vector3(1f, 1f, 1f);
                gunArm.localScale = Vector3.one;
            }

            //rotate gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

            //Encontra o cálculo do angulo
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            //Unity utiliza 4 termos para a rotação, no Debug mostra isso.
            //Quaternion.Euler transforma os 4 termos em um Vector3(x, y, z).
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            //Laço para deixar atirar
            //Time.time is the amount of time the game has ran for, so it is greater than 0.
            //Time.time parece ser uma boa opção para substituir o time.deltaTime
            if (Time.time - shotCounter > timeBetweenShots)
            {
                canFire = true;
            }

            //Clicar e Atirar --- Mouse1 = 0, Scroll = 1, mouse2 = 2;
            if (Input.GetMouseButtonDown(0))
            {
                if (canFire == true)
                {
                    //Bala aparece e com a direção correta
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = Time.time;
                    canFire = false;
                    AudioManager.instance.PlaySFX(12);   //Elemento 12 = som de disparo
                }
            }

            //Atirar e segurar
            if (Input.GetMouseButton(0))
            {
                //Contador de Disparos por frame
                if (canFire == true)
                {
                    //Bala aparece e com a direção correta
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = Time.time;
                    canFire = false;
                    AudioManager.instance.PlaySFX(12);   //Elemento 12 = som de disparo
                }
            }

            //Tecla espaço dao o dash
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLengh;

                    //Ativa a animação
                    anim.SetTrigger("dash");

                    PlayerHealthController.instance.makeIvincible(dashInvincibility); //chamar função no dashInvincibility

                    AudioManager.instance.PlaySFX(8);   //Elemento 8 = som de dash
                }
            }

            //Desativar dash
            //Se o dash counter é maior que 0 ele ainda está no dash
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCoolDown;
                }
            }

            //
            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }

            //Verificar se está se movendo
            //Vector2.zero = Valor de x e y = 0
            if (moveImput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);     //Escrever do mesmo jeito que no unity
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        //Para parar de andar
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false); //Para a animação ser anulada
        }
    }
}
