using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaAutomatica : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ArmaSO misDatos;


    


    [Header("Distancia")]
    [SerializeField] private Transform balaPos;
    [SerializeField] private GameObject bala;

    [SerializeField] private Transform balaCasePos;
    [SerializeField] private GameObject balaCase;

    private Camera cam;

    private Player player;

    private float timer;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GetComponentInParent<Player>();

        timer = misDatos.cadenciaAtaque;

        if (player != null)
        {
            anim = player.GetComponentInChildren<Animator>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetMouseButton(0) && timer >= misDatos.cadenciaAtaque)
        {
            Vector3 origenDisparo;
            Vector3 direccionDisparo;

            if (player.EnPrimeraPersona)
            {
                origenDisparo = cam.transform.position; // Frontal de la cámara en primera persona
                direccionDisparo = cam.transform.forward;
            }
            else
            {
                origenDisparo = balaPos.position; // Desde el arma en tercera persona
                direccionDisparo = balaPos.forward;
            }


            GameObject BalaSpawn = Instantiate(bala, origenDisparo, Quaternion.LookRotation(direccionDisparo));
            Rigidbody balaRigid = BalaSpawn.GetComponent<Rigidbody>();
            balaRigid.velocity = direccionDisparo * 50;

            GameObject BalaCasquillo = Instantiate(balaCase, balaCasePos.position, balaCasePos.rotation);
            Rigidbody balaCaseRigid = BalaCasquillo.GetComponent<Rigidbody>();
            Vector3 casquilloVec = balaCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
            balaCaseRigid.AddForce(casquilloVec, ForceMode.Impulse);
            balaCaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);



            anim.SetTrigger("shot");
            if (Physics.Raycast(cam.transform.position,cam.transform.forward,out RaycastHit hitInfo, misDatos.distanciaAtaque))
            {
                hitInfo.transform.GetComponentInChildren<Enemigo>().RecibirDanho(misDatos.danhoAtaque);
            }
            timer = 0;
        }
    }
}
