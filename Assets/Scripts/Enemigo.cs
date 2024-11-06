using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [Header("Sistema de Combate")]
    [SerializeField] private float danhoEnemigo;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsdanhable;

    private NavMeshAgent agent;
    private Animator anim;


    private Player player;
    private bool ventanaAbierta;
    private bool puedoDanhar = true;
    //El enemigo tiene que persguir al Player



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindAnyObjectByType<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();
    }




    private void DetectarImpacto()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.position,radioAtaque,queEsdanhable);
    }
    private void Perseguir()
    {
        agent.SetDestination(player.gameObject.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            anim.SetBool("attacking", true);
        }
    }

    private void FinAtaque()
    {
        agent.isStopped = false;
        anim.SetBool("attacking",false);
        puedoDanhar = true ;

    }
    private void AbrirVentanaAtaque()
    {
        ventanaAbierta = true ;
    }
    private void CerrarVentanaAtaque()
    {
        ventanaAbierta = false ;
    }
}
