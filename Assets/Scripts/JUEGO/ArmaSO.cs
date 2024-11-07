using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Arma")]
public class ArmaSO : ScriptableObject
{
    public enum TipoArma { Melee,Distancia}

    public TipoArma tipo;
    //DATOS
    public int balasCarador;
    public int balasBolsas;
    public float cadenciaAtaque;//1.5s puedes atacar
    public float distanciaAtaque;
    public float danhoAtaque;

}
