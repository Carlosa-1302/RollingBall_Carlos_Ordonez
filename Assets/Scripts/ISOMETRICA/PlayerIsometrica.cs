using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerIsometrica : MonoBehaviour
{
    private Camera cam;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
       agent =  GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //si Clickp con el mouse izd
        if(Input.GetMouseButtonDown(0))
        {
            //Creo un rayo desde la camara a la posicion del raton
           Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //Y si ese rayo impacta en algo
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                //le decimos al agente (nosotros que tiene como destino el punto de impacto
                agent.SetDestination(hitInfo.point);
            }

        }
    }
}
