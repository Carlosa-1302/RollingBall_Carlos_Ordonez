using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermediario : MonoBehaviour
{
    private Enemigo enemigoScript;
    // Start is called before the first frame update
    void Start()
    {
        enemigoScript = GetComponentInParent<Enemigo>();
    }

    public void AbrirVentanaAtaque()
    {
        if (enemigoScript != null) 
        {
            enemigoScript.AbrirVentanaAtaque();
        }
    }
    public void CerrarVentanaAtaque()
    {
        if (enemigoScript != null)
        {
            enemigoScript.CerrarVentanaAtaque();
        }
    }
    public void FinAtaque()
    {
        if (enemigoScript != null)
        {
            enemigoScript.FinAtaque();
        }
    }
}
