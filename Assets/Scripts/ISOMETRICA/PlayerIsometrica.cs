using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerIsometrica : MonoBehaviour, IIDanhable
{
    [SerializeField] private float vidas = 200;
    [SerializeField] private float distanciaInteraccion = 2f;
    [SerializeField] private float distanciaAtaque = 2f;
    [SerializeField] private float danhoAtaque = 10f;

    private int totalCoins;
    private PlayerVisual visualSystem;


    private Camera cam;
    private NavMeshAgent agent;
    private Animator anim;



    //Alamceno el ultimo transform que clické con el ratón.
    private Transform objetivoActual;
    private Transform ultimoClick;

    public PlayerVisual VisualSystem { get => visualSystem; set => visualSystem = value; }
    public int TotalCoins { get => totalCoins; set => totalCoins = value; }

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
                if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
                {
                    //le decimos al agente (nosotros que tiene como destino el punto de impacto
                    agent.SetDestination(hitInfo.point);

                    //Actualizo el ultimo hit con el transform que acabo de clickear.
                    ultimoClick = hitInfo.transform;
                }
                
            }

        }
        if(ultimoClick)
        {
            VisualSystem.StopAttacking();
            if(ultimoClick.TryGetComponent(out IInteractuable interactuable))
            {
                agent.stoppingDistance = distanciaInteraccion;
                if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    interactuable.Interactuar(transform);
                    ultimoClick = null;
                }
            }
        }
        else if (ultimoClick.TryGetComponent(out IIDanhable _))
        {
            objetivoActual = ultimoClick;
            agent.stoppingDistance = distanciaAtaque;
            if(!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
            {
                EnfocarAPlayer();
                visualSystem.StartAttacking();
            }
        }
    }
    private void EnfocarAPlayer()
    {
        Vector3 direccionATarget = (objetivoActual.transform.position - transform.position).normalized;
        direccionATarget.y = 0f;
        Quaternion rotacionATarget = Quaternion.LookRotation(direccionATarget);
        transform.rotation = rotacionATarget;
    }

    public void Atacar()
    {
        objetivoActual.GetComponent<IIDanhable>().RecibirDanho(danhoAtaque);
    }

    public void RecibirDanho(float danho)
    {
        vidas -= danho;
        if (vidas <= 0)
        {
            Destroy(this);
            visualSystem.EjecutarAnimacionMuerte();
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
