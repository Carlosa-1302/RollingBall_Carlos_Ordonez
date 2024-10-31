using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadCorrer;
    [SerializeField] private float alturaSalto;
    [SerializeField] private float factorGravedad;



    [Header("Deteccion Suelo")]

    [SerializeField] private float radioDeteccion;
    [SerializeField] private Transform pies;
    [SerializeField] private LayerMask queEsSuelo;


    private CharacterController controller;
    private Animator animator;

    //Me sirve tanto para la gravedada como para los saltos
    private Vector3 movimientoVertical;



    [SerializeField] private float fuerzaSalto;


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
            animator.SetBool("landing",false);
            animator.SetBool("jumping",false );
            movimientoVertical.y = 0;
            Saltar();

        }
    }
    void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        Vector3 input = new Vector3(h, v, 0);


        //transform.eulerAngles = new Vector3(0, anguloRotacion, 0);



        if (input.magnitude > 0)
        {
            float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            Quaternion rotacionSuave = Quaternion.Euler(0,anguloRotacion, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionSuave, 10 * Time.deltaTime);

            //transform.eulerAngles = new Vector3(0, anguloRotacion, 0);
             Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;
            

            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(movimiento * velocidadCorrer * Time.deltaTime);

                animator.SetBool("walking", false);
                animator.SetBool("running", true);
            }
            else
            {
                controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);
                animator.SetBool("walking", true);
                animator.SetBool("running", false);
            }
            

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

        controller.Move(movimientoVertical * Time.deltaTime);

    }
    private bool EnSuelo()
    {
        bool resultado = Physics.CheckSphere(pies.position, radioDeteccion, queEsSuelo);
        animator.SetBool("landing",true);
         
        return resultado;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pies.position, radioDeteccion);
    }
    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimientoVertical.y = Mathf.Sqrt(-2 * factorGravedad * alturaSalto);
            animator.SetBool("jumping", true);
        }
    }



}
