using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaInteracciones : MonoBehaviour
{
    [SerializeField] private float distanciaInteraccion;

    private Camera cam;
    private Transform InteracctuableActual;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    private void  Update()
    {
        DeteccionInteractuable();
    }

    private void DeteccionInteractuable()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, distanciaInteraccion))
        {
            if (hit.transform.TryGetComponent(out CajaMunicion scriptCaja))
            {
                InteracctuableActual = scriptCaja.transform;

                scriptCaja.transform.GetComponent<Outline>().enabled = true;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    scriptCaja.AbrirCaja();
                }
            }
        }

        else if (InteracctuableActual != null)
        {
            InteracctuableActual.GetComponent<Outline>().enabled = false;

            InteracctuableActual = null;
        }
    }
}
