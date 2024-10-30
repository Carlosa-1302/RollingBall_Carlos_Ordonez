using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private NavMeshAgent agent;

    private ThirdPerson player;
    //El enemigo tiene que persguir al Player



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindAnyObjectByType<ThirdPerson>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.gameObject.transform.position);
    }
}
