using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemaDialogo : MonoBehaviour
{
    [SerializeField] private GameObject marcoDialogo; //Marco a habilitar/deshabilitar

    [SerializeField] private TMP_Text textoDialogo; // El texto donde se veran reflejados los dialogos

    public static SistemaDialogo sistema;


    private bool escribiendo;
    private int indiceFraseActual = 0;

    private DialogoSO dialogoActual; //Para saber en todo momento cual es el dialogo con el que estamos trabajando








    //PRATÓN SINGLE-TON
    //1. Solo existe una unica instancia de SistemaDialogo
    //2. Es accesible DESDE CUALQUIER PUNTO del programa.

    //public static SistemaDialogo trono;


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
        Time.timeScale = 0;
        //El dialogo actual que tenemos que tratar es el que me pasan por parámetro.
        dialogoActual = dialogo;
        marcoDialogo.SetActive(true);
        StartCoroutine(EscribirFrase());
    }
    //Sirve para escribir frase letra por letra
    private IEnumerator EscribirFrase()
    {
        escribiendo = true;

        //Limpio el texto.
        textoDialogo.text = string.Empty;

        //Desmenuzo la frase actual por caracteres por separado.
        char[] fraseEnLetras = dialogoActual.frases[indiceFraseActual].ToCharArray();

        foreach (char letra in fraseEnLetras)
        {
            //1.Incluir la letra en el texto
            textoDialogo.text += letra;
            //2. Esperar

            //WaitForSecondsRealTime: No se para si el tiempo está congelado.
            yield return new WaitForSecondsRealtime(dialogoActual.tiempoEntreLetras);

        }
        escribiendo = false;
    }
    //Sirver para autocompletar la frase
    private void CompletarFrase()
    {
        //Si me piden completar la frase entera, en el texto pongo la frase entera
        textoDialogo.text = dialogoActual.frases[indiceFraseActual];
        //Y paro las Corrutinas que puedan estar vivas.
        StopAllCoroutines();

        escribiendo = false ;
    }
    //Se ejecuta al hacer click al boton para pasar a la siguiente frase
    public void SiguienteFrase()
    {
        //Si no estoy escribiendo....
        if(!escribiendo)
        {

            indiceFraseActual++;//Entonces, avanzo a la siguiente frase.

            //Si aun me quedan frases por sacar...
            if(indiceFraseActual < dialogoActual.frases.Length)
            {
                //La escribo
                StartCoroutine(EscribirFrase());
            }
            else
            {
                FinalizarDialogo();
            }
        }
        else
        {
            CompletarFrase();
        }
        
    }
    private void FinalizarDialogo()
    {
        Time.timeScale = 1;
        marcoDialogo.SetActive(false);//Cerramos el marco de dialogo.
        indiceFraseActual = 0; //Para que en posteriores dialogos empezemos dede indice 0.

        //escribiendo = false;

        dialogoActual = null; // Ya no tengo dialogo que escribir.
    }

    
}
