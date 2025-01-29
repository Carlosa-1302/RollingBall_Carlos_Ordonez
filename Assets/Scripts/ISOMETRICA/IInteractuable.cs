using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interfaz: Es unba serie de metodos que se han de implementar
//En aquellas entidades que, en este casi, sean interactuables.
public interface IInteractuable 
{
    public void Interactuar(Transform interactuador);
}
