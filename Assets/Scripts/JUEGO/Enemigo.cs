using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private ArmaSO misDatos;
    [Header("Sistema de Combate")]
    [SerializeField] private float VidaEnemigo;
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

    Rigidbody[] huesos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>(); 
        player = GameObject.FindAnyObjectByType<Player>();

        huesos = GetComponentsInChildren<Rigidbody>(); //getComponentS cuidado con la S
        CambiarEstadoHuesos(true);
    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();
        if(ventanaAbierta && puedoDanhar )
        {
            DetectarImpacto();
        }
    }

    


    private void DetectarImpacto()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.position,radioAtaque,queEsdanhable);

        if(collsDetectados.Length > 0 )
        {
            for(int i = 0; i < collsDetectados.Length; i++)
            {
                collsDetectados[i].GetComponent<Player>().RecibirDanho( danhoEnemigo);
            }
            puedoDanhar=true;
        }
    }
    private void Perseguir()
    {
        agent.SetDestination(player.gameObject.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            anim.SetBool("attacking", true);
        }
        else
        {
            agent.isStopped = false;
            anim.SetBool("attacking", false);
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
        ventanaAbierta = false ;//asdasdasd
    }
    public void RecibirDanho(float danhoRecibido)
    {
        VidaEnemigo -= danhoRecibido;
        if(VidaEnemigo <= 0 )
        {
            CambiarEstadoHuesos(false);
        }
    }
    private void CambiarEstadoHuesos(bool estado)
    {
        for (int i = 0; i < huesos.Length; i++)
        {
            huesos[i].isKinematic = estado;
        }
    }
}
