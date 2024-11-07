using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaManual : MonoBehaviour
{
    [Header("Distancia")]
    [SerializeField] private ArmaSO misDatos;
    [SerializeField] private LayerMask queEsDanhable;//va a atravesar todo objeto a los que si sea dañable como un zombie detras del arbol, el arbol no es dañable pero el zombie si entonces pasa del arbol y daña al zombie aunque este detras
    [SerializeField] private ParticleSystem ParticleSystem;

    [Header("Melee")]
    [SerializeField] TrailRenderer trailEffect;
    [SerializeField] BoxCollider AreaMelee;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //"MainCamera".
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ParticleSystem.Play(); //Ejecuto el sistema de Particulas
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, misDatos.distanciaAtaque))
            {
                Debug.Log(hitInfo.transform.name);//muestro el nombre de a quien he impactado
            }
        }
    }
    private void GolpearMelee()
    {

    }
}
