using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //GetComponentInParent<NavMeshAgent>
    }

    // Update is called once per frame
    void Update()
    {
        //agent.velocity.magnitude ---> Velocidad actual...
        //agent.speed --> es maxima velocidad que tengo configurada
        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);
    }
}
