using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDialogo : MonoBehaviour
{
    //PRATÓN SINGLE-TON
    //1. Solo existe una unica instancia de SistemaDialogo
    //2. Es accesible DESDE CUALQUIER PUNTO del programa.

    //public static SistemaDialogo trono;
    public static SistemaDialogo sistema;


    //Awake se ejecuta antes del Start() idependientemente de que 
    //el gameObject este activo o no.
    void Awake()
    {
        //Si el trono esta libre...
        /*if (trono == null)
        {
            //Me hago con el trono, y entonces SistemaDialogo SOY YO (this).
            trono = this;
        }
        else
        {
            //Me han quitado el trono, por lo tanto me mato (soy un falso rey)
            Destroy(this.gameObject);  
        }*/

        if(sistema == null)
        {
            sistema = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void IniciarDialogo(DialogoSO dialogo)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
