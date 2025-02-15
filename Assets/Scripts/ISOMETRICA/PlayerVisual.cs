using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerVisual : MonoBehaviour
{
    //[SerializeField] private EventManagerSO gM;
    //[SerializeField] private int visualID;
    [SerializeField] private PlayerIsometrica player;
    [SerializeField] private NavMeshAgent agent;
    private Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        //GetComponentInParent<NavMeshAgent>
        player.VisualSystem = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        //agent.velocity.magnitude ---> Velocidad actual...
        //agent.speed --> es maxima velocidad que tengo configurada
        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);
    }
    private void LanzarAtaque()
    {
        player.Atacar();
    }
    public void EjecutarAnimacionMuerte()
    {
        anim.SetTrigger("death");
    }
    public void StartAttacking()
    {
        anim.SetBool("attacking", true);
    }
    public void StopAttacking() 
    {   
        anim.SetBool("attacking", false);
    }
}
