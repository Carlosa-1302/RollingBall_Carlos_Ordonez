using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField]  Vector3 plataforma  ;
    float timer = 0;
    bool desactivarTimer = true;
    void Update()
    {

        transform.Translate(plataforma * 3 * Time.deltaTime, Space.World);
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            plataforma = plataforma * -1;
            timer = 0;
        }
    }
}
        /*if ( desactivarTimer  == true)
        {
            transform.Translate(plataforma * 3 * Time.deltaTime, Space.World);
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                plataforma = plataforma * -1;
                desactivarTimer = false;
            }
        }*/
