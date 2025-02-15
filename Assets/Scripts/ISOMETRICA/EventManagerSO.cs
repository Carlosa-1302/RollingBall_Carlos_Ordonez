using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventManager")]

public class EventManagerSO : ScriptableObject
{
    
    public event Action<MisionSO> OnNuevaMision;
    public event Action<MisionSO> OnActualizarMision;
    public event Action<MisionSO> OnTerminarMision; //lo que esta dentro del action es para pasar informacion extra
    public void NuevaMision(MisionSO mision)
    {
        //Lanza/Dispara/Notifica el evento/Notificancion CON PARAMETROS


        // ?. -> Invocacion Segura se asegura que hay subscriptores
        OnNuevaMision?.Invoke(mision);
    }
    internal void ActualizarMision(MisionSO mision)
    {
        OnActualizarMision?.Invoke(mision);
    }
    public void TerminarMision(MisionSO mision)
    {
        OnTerminarMision?.Invoke(mision);
    }
    
}
