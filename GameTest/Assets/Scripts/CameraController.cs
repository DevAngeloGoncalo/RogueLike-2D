using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Caso seja da preferencia que a camera siga o personagem, esse método não é utilizado.
    //Aqui a camera vai seguir os rooms
    public static CameraController instance;    //Para ser acessada de outros scripts
    public float moveSpeeed;                    //Velocidade que a camera se movimenta
    public Transform target;                    //Ponto para a camera ir quando mudar de room

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
        if(target != null)
        {
            //Movimentação
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeeed * Time.deltaTime);
        }
    }

    //Para mudar o foco dos quartos
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
