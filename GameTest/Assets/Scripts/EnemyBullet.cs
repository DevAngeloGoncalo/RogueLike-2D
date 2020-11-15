using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;    //Velocidade Bala
    private Vector3 direction;  //Direção para mirar no player

    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position; //Procura o jogador
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime; //Direção
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")   //dar dano ao player
        {
            PlayerHealthController.instance.DamagePlayer(); //vai charmar a função DamagePlayer em PlayerHelhController sempre que o player for atingido
        }
        Destroy(gameObject);

        AudioManager.instance.PlaySFX(4);   //Elemento 4 = som de impacto
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
