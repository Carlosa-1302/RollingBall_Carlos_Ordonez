using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedCubos : MonoBehaviour
{
    private bool iniciarTimer;
    int timer = 0;

    // 1. crear un timer para que empiece a contar una vez iniciado
    // 2. hacer que el timer cuente hasta 2 segundos.
    // 3. una vez que el timer haya contado 2 segundos .......volver el timeScale a 1.

    private void Update()
    {
        if(iniciarTimer)
        {
            timer++;
            if(timer >=2)
            {

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.2f;
            iniciarTimer = true; //para iniciar cuenta para que el tiempo vuelva a 100%
        }
    }
}
