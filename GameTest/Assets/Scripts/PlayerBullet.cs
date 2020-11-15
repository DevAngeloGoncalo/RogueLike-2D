using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 9f;            //Velocidade Bala
    public Rigidbody2D theRB;           //Colisão (vai servir para guiar a bala)
    public GameObject impactEffect;     //Efeito Impacto
    public int damageToGive = 50;       //Balas causam 50 de dano

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Right pois ele sempre irá para a sua direita, sem precisar de calculos complicados
        theRB.velocity = transform.right * speed;
    }

    //O que fazer na colisão
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        //Vai aparecer animação
        Destroy(gameObject);
        //Vai desaparecer quando colidir

        AudioManager.instance.PlaySFX(4);   //Elemento 4 = som de impacto

        //Verifica se está atirando em um inimigo ou cenario
        if (other.tag == "Enemy")
        {
            //Busca o DamageEnemy na solução EnemyController
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }

        if (other.tag == "Box")
        {
            //Busca o DamageBox na solução breakable
            other.GetComponent<breakable>().DamageBox(damageToGive);
        }
    }
    //Destrói as balas quando saem do campo de visão
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
