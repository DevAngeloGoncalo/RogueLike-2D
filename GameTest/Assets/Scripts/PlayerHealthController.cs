using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;      //Para ter acesso de outros scripts

    public int currentHealth;                           //Vida do personagem
    public int maxHealth;                               //Vida Máxima

    //Ivencibilidade para quando nao der pra ver a bala 
    public float damageInvincLengh = 1f;  //1 segundo invencivel                   
    private float invincCount;

    private void Awake() //Serve bem para ativar e desativar itens no jogo
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;          //Quando o jogo inicia, a vida máxima do personagem é a vida que ele tem.

        UIController.instance.healthSlider.maxValue = maxHealth;    //Valor da barra de vida vai ser a vida máxima
        UIController.instance.healthSlider.value = currentHealth;   //Barra de vida com a vida atual
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();    //Escreve na barra de vida
    }

    // Update is called once per frame
    void Update()
    {
        //Necessário para não tomar dano injusto
        if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            //para quando nao sofrer dano e o tempo for menor ou igual a zero nao ficar trnsparente
            if(invincCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }
        }
    }

    //Receber dano dos inimigos
    public void DamagePlayer()
    {
        //Verifica o contador
        if(invincCount <= 0)
        {
            AudioManager.instance.PlaySFX(11);   //Elemento 11 = som de machucado
            currentHealth--;  //currentHealth -= 1; mesma coisa

            invincCount = damageInvincLengh;    //tempo invencivel

            //para quando sofrer dano ficar transparente
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);  //desativa o player quando sua vida cheagr a 0 ou menos
                UIController.instance.deathScreen.SetActive(true);      //Chama em UIController a cena de morte

                AudioManager.instance.PlayGameOver();                   //Chama o Audio Manager e toca música de morte
                AudioManager.instance.PlaySFX(9);                       //Elemento 4 = som de morte
            }

            UIController.instance.healthSlider.value = currentHealth;   //Barra de vida com a vida atual
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();    //Escreve na barra de vida
        }
    }
    
    //Ficar invensivel no dash
    public void makeIvincible(float length)     //Quanto tempo queremos ficar invencivel
    {
        invincCount = length;

        //para quando sofrer dano ficar transparente
        PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
    }

    //Ganho de vida
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        //Atualiza a vida
        UIController.instance.healthSlider.value = currentHealth;   //Barra de vida com a vida atual
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();    //Escreve na barra de vida
    }
}
