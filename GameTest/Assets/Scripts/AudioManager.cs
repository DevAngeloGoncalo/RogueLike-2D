using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;                        //Para ter acesso de outros scripts
    public AudioSource levelMusic, gameOverMusic, winMusic;     //Musicas dentro do jogo
    public AudioSource[] sfx;                                   //Efeitos especiais

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Iniciar musica quando morre
    public void PlayGameOver()
    {
        levelMusic.Stop();

        gameOverMusic.Play();
    }

    public void PlayLevelWin()
    {
        levelMusic.Stop();

        winMusic.Play();
    }

    //Efeitos sonoros
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();  //Começa pausado para não tocar todos
        sfx[sfxToPlay].Play();
    }
}
