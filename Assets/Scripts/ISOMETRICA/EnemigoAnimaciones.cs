using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoAnimaciones : MonoBehaviour
{
    [SerializeField] private EnemigoIsometrica enemigoIsometrica;
    [SerializeField] private NavMeshAgent agent;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemigoIsometrica.EnemigoVisualSystem = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Actualizo el parametro "velocity" en funcion de la velocidad del agente

        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);
    }
    public void EjecutarAnimacionMuerte()
    {
        anim.SetTrigger("death");
    }
}
