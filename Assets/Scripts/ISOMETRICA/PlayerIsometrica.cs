using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerIsometrica : MonoBehaviour
{
    private Camera cam;
    private NavMeshAgent agent;



    //Alamceno el ultimo transform que clické con el ratón.
    private Transform ultimoClick;
    private Animator anim;
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponent<Animator>();
        //anim = transform.Find("Mesh Object").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {

            Movimiento();
        }

        ComprobarInteraccion();
    }


    private void Movimiento()
    {
        //si Clickp con el mouse izd
        if (Input.GetMouseButtonDown(0))
        {
            //Creo un rayo desde la camara a la posicion del raton
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //Y si ese rayo impacta en algo
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                //le decimos al agente (nosotros que tiene como destino el punto de impacto
                agent.SetDestination(hitInfo.point);

                //Actualizo el ultimo hit con el transform que acabo de clickear.
                ultimoClick = hitInfo.transform;
            }

        }
    }
    private void ComprobarInteraccion()
    {
        //Si existe un interractuable al cual clické y lleva consigo el script NPC 
        if(ultimoClick != null && ultimoClick.TryGetComponent(out IInteractuable interactuacle))
        {
            //Actualizo distancia de parada para no comerme al NPC
            agent.stoppingDistance = 2f;

            //Mira a ver si hemos llegado a dicho destino.
            if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                //Y por lo tanto, interactuo con el NPC

                interactuacle.Interactuar(transform);

                //Me olvido de cual fue el ultimo click, por que solo quiero interactuar UNA VEZ.
                ultimoClick = null;
            }
        }
        //Si no es un NPC, si no que es un click en el suelo...
        else if(ultimoClick)
        {
            //Reseteo el stoppingDistance original.
            agent.stoppingDistance = 0f;
        }
    }

    public void HacerDanho(float danhoAtaque)
    {
        Debug.Log("Me hacen pupa: " +  danhoAtaque);
    }
}
