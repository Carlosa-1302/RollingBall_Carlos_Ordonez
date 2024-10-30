using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using Input = UnityEngine.Input;

public class ThirdPerson : MonoBehaviour
{

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadCorrer;
    [SerializeField] private float alturaSalto;
    [SerializeField] private float factorGravedad = 9.81f;



    [Header("Deteccion Suelo")]

    [SerializeField] private float radioDeteccion;
    [SerializeField] private Transform pies;
    [SerializeField] private LayerMask queEsSuelo;


    private CharacterController controller;
    private Animator animator;

    //Me sirve tanto para la gravedada como para los saltos
    private Vector3 movimientoVertical;



    [SerializeField]private float fuerzaSalto;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;// bloquea el raton en centro de la pantalla y lo oculta
       
    }

    // Update is called once per frame
    void Update()
    {
        MoverYRotar();
        AplicarGravedad();
        if (EnSuelo())
        {
            movimientoVertical.y = 0;
          Saltar();

        }
    }
     void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        Vector3 input = new Vector3(h, v, 0);

        float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        //transform.eulerAngles = new Vector3(0, anguloRotacion, 0);
        
        Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;

        //Roto el cuerpo de forma constante con la rotacion "y" de la camara
        transform.rotation =Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        if (input.magnitude > 0)
        {

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);

            animator.SetBool("walking", true);
            animator.SetBool("running", false);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(movimiento * velocidadCorrer * Time.deltaTime);

            animator.SetBool("walking", false);
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("running", false);
        }

    }
    
    private void AplicarGravedad()
    {
        movimientoVertical.y += factorGravedad * Time.deltaTime;

        controller.Move(movimientoVertical*Time.deltaTime);

    }
    private bool EnSuelo()
    {
        bool resultado = Physics.CheckSphere(pies.position,radioDeteccion,queEsSuelo);
        return resultado;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pies.position,radioDeteccion);
    }
    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimientoVertical.y = Mathf.Sqrt(-2 * factorGravedad * alturaSalto);
        }
    }


    
}


