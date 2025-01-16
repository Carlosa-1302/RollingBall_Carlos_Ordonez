using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoIsometrica : MonoBehaviour
{
    private SistemaPatrulla patrulla;
    private SistemaCombate combate;

    private Transform target;

    public SistemaPatrulla Patrulla { get => patrulla; set => patrulla = value; }
    public SistemaCombate Combate { get => combate; set => combate = value; }
    public Transform Target { get => target; set => target = value; }
    private void Start()
    {
        patrulla.enabled = true;
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
