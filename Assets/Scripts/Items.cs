using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [Header("Velocidad Rotación")]
    [SerializeField] private float velocidadRotacion;
    //sadasd

    //[Header("Tipo Items")]
    [SerializeField] private enum Type { Ammo, Moneda, Arma, Vida }
    [SerializeField] private Type tipo;
    [SerializeField] private int valor;

    public int Valor { get => valor; set => valor = value; }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0,0,0 * 35 * Time.deltaTime,Space.World);//el space.world es para que use x,y,z del mundo no del objeto
        
        transform.Rotate (new Vector3(0,1,0) * velocidadRotacion * Time.deltaTime,Space.World);
    }
    
}
