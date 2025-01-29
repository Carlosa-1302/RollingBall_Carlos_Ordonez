using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractuable
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private MisionSO miMision;
    [SerializeField] private DialogoSO miDialogo1;
    [SerializeField] private DialogoSO miDialogo2;
    [SerializeField] private DialogoSO dialogoActual;
    [SerializeField] private float duracionRotacion;
    [SerializeField] private Transform cameraPoint;

    private void Awake()
    {
        dialogoActual = miDialogo1;
    }
    private void OnEnable()
    {
        eventManager.OnTerminarMision += CambiarDialogo;
    }

    private void CambiarDialogo(MisionSO misionTerminada)
    {
        if(miMision == misionTerminada)
        {
            dialogoActual = miDialogo2;
        }
    }

    public void Interactuar(Transform interactuador)
    {
        //Poco a poco voy Rotando hacie el interactuador y cuando termine de rotarme.... se inicia la interaccion
        
        transform.DOLookAt(interactuador.position, duracionRotacion, AxisConstraint.Y).OnComplete(IniciarInteraccion);
    }

    public void Interactuar()
    {
        
    }

    private void IniciarInteraccion()
    {
        SistemaDialogo.sistema.IniciarDialogo(dialogoActual, cameraPoint);
        Debug.Log("Hola!");
    }
    private void OnDisable()
    {
        eventManager.OnTerminarMision -= CambiarDialogo;
        //esto es para desuscribirse del evento por que sino luego salta error
    }
}
