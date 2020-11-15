using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBoxOrder : MonoBehaviour
{
    private SpriteRenderer theSR;   //Sprite Renderer

    // Start is called before the first frame update
    void Start()
    {
        //Aleatorizar Ordem Layer
        theSR = GetComponent<SpriteRenderer>();

        theSR.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f); //Layer são inteiros
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
