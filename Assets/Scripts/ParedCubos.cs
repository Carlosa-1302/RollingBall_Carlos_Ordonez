using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedCubos : MonoBehaviour
{
    private bool iniciarTimer;
    [SerializeField]private float timer = 0;




    [SerializeField] private Rigidbody[] rbs;//el corchete es para poner el plural





    // 1. crear un timer para que empiece a contar una vez iniciado
    // 2. hacer que el timer cuente hasta 2 segundos.
    // 3. una vez que el timer haya contado 2 segundos .......volver el timeScale a 1.

    private void Update()
    {
        if(iniciarTimer == true)
        {

            timer += 1 * Time.unscaledDeltaTime; // UnscaledDeltaTime: deltaTime PERO no afectado por la escala del tiempo
            if (timer >= 2)
            {
                Time.timeScale = 1;

                //Recorro el array de rbs para volverles a poner gravedad
                for (int cubos = 0; cubos < rbs.Length; cubos++)//recordatorio el for puedes cambiar LA "i" por cualquier nombre del for
                {
                  rbs[cubos].useGravity = true;

                }
            }
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.2f;

            iniciarTimer = true; //el bool para iniciar cuenta para que el tiempo vuelva a 100% y que se pueda ejecutar en el update
        }
    }
}
