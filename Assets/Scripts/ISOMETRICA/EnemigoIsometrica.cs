using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemigoIsometrica : MonoBehaviour, IIDanhable
{

    [SerializeField] private float vidasIniciales;

    [SerializeField] private BillBoard localCanvas;

    [SerializeField] Image barraVida;

    [SerializeField] Outline outline;

    [SerializeField] Collider coll;




    private EnemigoAnimaciones enemigoVisualSystem;

    
    private NavMeshAgent agent;

    private SistemaPatrulla patrulla;

    private SistemaCombate combate;

    private Transform target;

    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public SistemaCombate Combate { get => combate; set => combate = value; }
    public SistemaPatrulla Patrulla { get => patrulla; set => patrulla = value; }
  
    public EnemigoAnimaciones EnemigoVisualSystem { get => enemigoVisualSystem; set => enemigoVisualSystem = value; }
    public Transform Target { get => target; set => target = value; }

    private float vidasActuales;
    private bool muerto;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        vidasActuales = vidasIniciales;
    }
    private void Start()
    {
        patrulla.enabled = true;
    }

    public void RecibirDanho(float danho)
    {
        if (muerto) return;

        vidasActuales -= danho;
        barraVida.fillAmount = vidasActuales / vidasIniciales;
        if (vidasActuales <= 0)
        {
            muerto = true;
            Muerte();
        }
    }
   
    private void Muerte()
    {
        Destroy(localCanvas.gameObject);
        Destroy(coll);
        Destroy(combate);
        Destroy(patrulla.gameObject);
        Destroy(gameObject, 5);
        EnemigoVisualSystem.EjecutarAnimacionMuerte();
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    public void activarCombate(Transform target)
    {
        
        patrulla.enabled = false;//Desactivamos patrulla
        combate.enabled = true;//Activar Combate
        this.target = target;//Definimos target.
    }

    public void ActivarPatrulla()
    {
        //Deshabilitar combate
        combate.enabled = false;
        //Activar Patrulla
        patrulla.enabled = true;
    }

}
