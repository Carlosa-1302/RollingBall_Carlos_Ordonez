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
    [SerializeField] private float distanciaDeteccion;

    private NavMeshAgent agent;
    public Animator anim;



    private PlayerIsometrica player;
    private bool ventanaAbierta;
    private bool puedoDanhar = true;
    private bool estaMueriendo  = false;





    //El enemigo tiene que persguir al Player



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindAnyObjectByType<PlayerIsometrica>();
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!estaMueriendo)
        Perseguir();
        if (ventanaAbierta && puedoDanhar)
        {
            DetectarImpacto();
        }
    }




    private void DetectarImpacto()
    {
        if (!puedoDanhar) return;

        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.position, radioAtaque, queEsdanhable);

        if (collsDetectados.Length > 0)
        {
            if (!puedoDanhar) return;
            for (int i = 0; i < collsDetectados.Length; i++)
            {
                PlayerIsometrica player = collsDetectados[i].GetComponent<PlayerIsometrica>();
                if (player != null)
                {
                    //player.RecibirDanho(danhoEnemigo); ATENTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO QUITA EL COMENTARIO
                    puedoDanhar = false;
                    break;
                }

                //collsDetectados[i].GetComponent<Player>().RecibirDanho(danhoEnemigo);
            }
            //puedoDanhar = false;
        }
    }
    private void Perseguir()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position,player.transform.position);
        if(distanciaAlJugador <= distanciaDeteccion)
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
        else
        {
            agent.isStopped = true;
            anim.SetBool("attacking" , false);
        }
        
    }

    public void FinAtaque()
    {
        agent.isStopped = false;
        anim.SetBool("attacking", false);
        puedoDanhar = true;

    }
    public void AbrirVentanaAtaque()
    {
        ventanaAbierta = true;
    }
    public void CerrarVentanaAtaque()
    {
        ventanaAbierta = false;//asdasdasd
    }
    public void RecibirDanho(float danhoRecibido)
    {
        
        Debug.Log("Daño recibido: " + danhoRecibido + ", Vida restante: " + VidaEnemigo);
        VidaEnemigo -= danhoRecibido;
        if (VidaEnemigo <= 0)
        {
            estaMueriendo = true;
            anim.SetBool("dying", true);
            agent.isStopped= true;
            Invoke(nameof(DestruirEnemigo), 2f);
        }
       
    }
    private void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}

