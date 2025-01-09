using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaCombate : MonoBehaviour
{

    [SerializeField] private EnemigoIsometrica main;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float velocidadCombate;
    private void Awake()
    {

        main.Combate = this;
        
    }
    //OnEnable: Se ejecuta cuando se activa el SCRIPT.
    //Se puede ejecutar VARIAS VECES. (cuando el script se active cuando detecte al player)
    private void OnEnable()
    {
        agent.speed = velocidadCombate;
        
    }
    private void Update()
    {
        //Voy persiguiento al target en todo momento (calculando su posicion 
        agent.SetDestination(main.Target.position);
        
    }
}
