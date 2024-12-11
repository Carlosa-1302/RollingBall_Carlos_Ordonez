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
        //El dialogo actual que tenemos que tratar es el que me pasan por parámetro.
        dialogoActual = dialogo;
        marcoDialogo.SetActive(true);
        StartCoroutine(CompletarFrase());
    }
    //Sirve para escribir frase letra por letra
    private void EscribirFrase()
    {

    }
    //Sirver para autocompletar la frase
    private IEnumerator CompletarFrase()
    {
        //Limpio el texto.
        textoDialogo.text =string.Empty;

        //Desmenuzo la frase actual por caracteres por separado.
        char[] fraseEnLetras = dialogoActual.frases[indiceFraseActual].ToCharArray();

        foreach(char letra in fraseEnLetras)
        {
            //1.Incluir la letra en el texto
            textoDialogo.text += letra;
            //2. Esperar
            yield return new WaitForSeconds(0.02f);

        }
    }
    private void SiguienteFrase()
    {

    }
    private void FinalizarDialogo()
    {

    }

    
}
