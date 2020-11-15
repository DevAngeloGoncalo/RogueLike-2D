using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenPiece : MonoBehaviour
{
    public float moveSpeed = 3f;            //Velocidade
    private Vector3 moveDirection;          //Direção
    public float deceleration = 5f;         //Para desacelerar as peças
    public float lifeTime = 3f;             //Tempo até as peças desaparecerem
    public SpriteRenderer theSR;            //Efeito de Fade do Sprite
    public float fadeSpeed = 2.5f;          //Velocidade do fade

    // Start is called before the first frame update
    void Start()
    {
        //Valores aleatórios para onde as peças vão
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //Direção no jogo
        transform.position += moveDirection * Time.deltaTime;

        //Lerp retorna um ponto interpolado entre um ponto inicial e final, ótima forma de desacelerar um objeto.
        //Pega a direção(velocidade tambem), depois Vector3.zero pois é o ultimo valor para descacelerar de forma decrescente, pós isso vem a velocidade de desaceleração.
        //O lerp nunca para, só fica extremamente lento e inperceptível após um tempo.
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        //Contagem de tempo
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            //Efeito de Fade
            //MoveTowards parece o Lerp, mas nesse caso, ele realmente acaba.
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, Mathf.MoveTowards(theSR.color.a, 0f, fadeSpeed * Time.deltaTime));

            //Quando chegar em 0 o fade as peças são destruidas
            if(theSR.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
