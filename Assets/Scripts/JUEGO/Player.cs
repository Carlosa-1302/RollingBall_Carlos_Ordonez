using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vidas;

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadCorrer;
    [SerializeField] private float alturaSalto;
    [SerializeField] private float factorGravedad;
    [SerializeField] private float fuerzaSalto;
    private Vector3 movimientoVertical;



    [Header("Deteccion Suelo")]

    [SerializeField] private float radioDeteccion;
    [SerializeField] private Transform pies;
    [SerializeField] private LayerMask queEsSuelo;


    private CharacterController controller;
    private Animator animator;
    private bool estaSaltando;
    private bool estaCayendo;


    [Header("Esquivar")]
    [SerializeField] private float velocidadEsquive;
    [SerializeField] private float duracionEsquive;
    private float velocidadOriginal;
    private bool estaEsquivando = false;

    Vector3 direccionFrontal;
    Vector3 direccionEsquive;
    //Me sirve tanto para la gravedada como para los saltos


    [Header("TextoUI")]
    [SerializeField] TMP_Text TextoMonedas;
    [SerializeField]private int monedas;

    [Header("Armas")]
    [SerializeField] private int ammo;
    [SerializeField] private GameObject[] Armas;
    [SerializeField] private bool[] tieneArma;
    private GameObject equiparArma;
     bool estaCambiandoArma;
    int ArmaNº = -1;

    public int ArmaNº1 { get => ArmaNº; set => ArmaNº = value; }



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
        Dodge();
        CambiarArma();
    }
    void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        Vector3 input = new Vector3(h, v, 0);


        //transform.eulerAngles = new Vector3(0, anguloRotacion, 0);


        if (estaEsquivando)
        {
            controller.Move(direccionEsquive * Time.deltaTime);
        }
        else if (input.magnitude > 0)
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
        if(resultado && estaSaltando) 
        {
            estaSaltando = false;
            animator.SetBool("jumping", false);
            animator.SetTrigger("land");
        }
        
        
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
            estaSaltando=true;
            animator.SetBool("jumping", true);
            animator.SetTrigger("jump");
        }
    }
    public void Dodge()
    {
        
      if(Input.GetKeyDown(KeyCode.Q) && !estaEsquivando && !estaCambiandoArma)
      {
            direccionEsquive = transform.forward * velocidadEsquive;
            estaEsquivando=true;
            animator.SetTrigger("dodge");

            Invoke(nameof(FinalizarEsquive), duracionEsquive);
            
            
      }
    }

    private void FinalizarEsquive()
    {
        //velocidadMovimiento = velocidadOriginal;
        estaEsquivando = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moneda"))
        {
            monedas++;
            TextoMonedas.SetText("Monedas: " + monedas);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Arma"))
        {
            Items item = other.GetComponent<Items>();


            int ArmaNº = item.Valor;
            tieneArma[ArmaNº] = true;


            Destroy(other.gameObject);
        }
        
    }
    public void RecibirDanho(float danhoEnemigo)
    {
      //vidas -= danhoEnemigo;
    }
    public void CambiarArma()
    {
        if (estaCambiandoArma) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ArmaNº1 = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ArmaNº1 = 1;
        }
        else if ((Input.GetKeyDown(KeyCode.Alpha3)))
        {
            ArmaNº1 = 2;
        }
        if(ArmaNº1 >= 0 && ArmaNº1 < Armas.Length && tieneArma[ArmaNº1])
        {
            if(equiparArma != null)
            {
                Armas[ArmaNº1].SetActive(false);
                equiparArma.SetActive(false);

            }
            
            equiparArma = Armas[ArmaNº1];
            Armas[ArmaNº1].SetActive(true);


            animator.SetTrigger("swap");
            estaCambiandoArma = true;
            Invoke("TerminarCambiarArma",1);
        }
    }
    private void TerminarCambiarArma()
    {
        estaCambiandoArma = false; 
    }


}
