using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaCombate : MonoBehaviour
{

    [SerializeField] private EnemigoIsometrica main;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float velocidadCombate;
    [SerializeField] private float distanciAtaque;
    [SerializeField] private float danhoAtaque;

    [SerializeField] private Animator anim;


    private void Awake()
    {

        main.Combate = this;
        
    }
    //OnEnable: Se ejecuta cuando se activa el SCRIPT.
    //Se puede ejecutar VARIAS VECES. (cuando el script se active cuando detecte al player)
    private void OnEnable()
    {
        agent.speed = velocidadCombate;
        agent.stoppingDistance = distanciAtaque;
        
    }
    private void Update()
    {


        //Solo si existe un objetivo y es alcanzable.....
        if(main.Target != null && agent.CalculatePath(main.Target.position, new NavMeshPath()))
        {
            //Procuraremos que SIEMPRE estemos enfocando al player
            EnfocarObjetivo();
            //Voy persiguiento al target en todo momento (calculando su posicion 
            agent.SetDestination(main.Target.position);

            //Cuando tenga al objetivo a distancia de ataque....
            if(!agent.pathPending && agent.remainingDistance <= distanciAtaque)
            {
                
                //Atacar
                anim.SetBool("attacking", true);
                agent.isStopped = true;
                

            }
        }
        else
        {
            //Deshabilitamos script de combate
            main.ActivarPatrulla();

            
        }
        
        
    }

    private void EnfocarObjetivo()
    {
        //1.Calculo la direccion al objetivo
        Vector3 direccionATarget = (main.Target.transform.position - transform.position).normalized;

        //Pongo la "y" a 0 para que no se vuelque.
        direccionATarget.y = 0;

        //Transformo una direccion en una rotation
        Quaternion rotationATarget = Quaternion.LookRotation(direccionATarget);

        //Aplico la rotacion
        transform.rotation = rotationATarget;
    }

    
    #region Ejecutados por evento de animación
    private void Atacar()
    {
        //Hacaer daño al target.
        main.Target.GetComponent<PlayerIsometrica>().RecibirDanho(danhoAtaque);

    }
    private void FinAnimacionAtaque()
    {
        anim.SetBool("attacking", false);
        agent.isStopped = false;
        Debug.Log("FinAnimacionAtaque llamado");
    }
    #endregion
}
