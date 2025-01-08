
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaPatrulla : MonoBehaviour
{
    [SerializeField] private Transform ruta;

    private List<Vector3> listadoPuntos = new List<Vector3>();

    [SerializeField]private NavMeshAgent agent;

    private int indiceDestinoActual = 0; // Marca el punto al cual debo ir.

    private Vector3 destinoActual;// Marca el destino al cual debo ir.

    //Diferencia con array
    private Transform[] array = new Transform[50];// el array solo tiene el limito hasta 50 pero con la LISTA se actualiza los puntos que crees mas adelante

    private void Awake() //Funciona antes del Start.
    {
        
        foreach(Transform puntos in ruta)
        {
            listadoPuntos.Add(puntos.position);
            //añado todos los puntos de ruta al listado.
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PatrullarYEsperar());

    }

    // Update is called once per frame
    private IEnumerator PatrullarYEsperar()//Corrutina
    {
        //Por Siempre.
        while(true)
        {
            CalcularDestino();//Tendré que calcular el nuevo destino
            //Ir a Destino.

            agent.SetDestination(destinoActual);
            yield return new WaitUntil(  ()  => agent.remainingDistance <= 0);//Expresion lambda.//Una forma de optimizar codigo para ahorrarse un metodo


            //se espera al llegar
            yield return new WaitForSeconds(Random.Range(0.25f, 3f));
        }

    }

    private void CalcularDestino()
    {
        indiceDestinoActual++;

        if(indiceDestinoActual > listadoPuntos.Count)//el Count es lo mismo que ".Lenght"
        {
            indiceDestinoActual = 0;
        }

        //Mi destino es dentro del listado de puntos aquel con el nuevo indice calculado.
        destinoActual = listadoPuntos[indiceDestinoActual];
    }
}
