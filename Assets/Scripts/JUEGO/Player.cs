using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float vidas;

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadCorrer;
    [SerializeField] private float alturaSalto;
    [SerializeField] private float factorGravedad;
    [SerializeField] private float fuerzaSalto;
    private Vector3 movimientoVertical;
    bool estoyEnTerceraPersona;



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
    [SerializeField] TMP_Text TextoVidas;
    [SerializeField]private int monedas;
    [SerializeField] private GameObject pantallaMuerte;
    [SerializeField] private TextMeshPro mensajeMuerto;
    private bool estaMuerto = false;
    private bool estaVivo = true;

    [Header("Armas")]
    [SerializeField] private int ammo;
    [SerializeField] private GameObject[] Armas;
    [SerializeField] private bool[] tieneArma;
    private GameObject equiparArma;
     bool estaCambiandoArma = false;
    int ArmaNº = -1;

    public int ArmaNº1 { get => ArmaNº; set => ArmaNº = value; }



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.Find("Mesh Object").GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;// bloquea el raton en centro de la pantalla y lo oculta
        TextoVidas.text = "Vidas" + vidas;

    }

    // Update is called once per frame
    void Update()
    {

        if (estaVivo)
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
        

        if(estaMuerto && Input.GetKeyDown (KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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

        if (input.magnitude > 0)
        {
         
            float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;
            if(estoyEnTerceraPersona)
            {

                Quaternion rotacionSuave = Quaternion.Euler(0,anguloRotacion, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionSuave, 10 * Time.deltaTime);

                //transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

            }
            else
            {
                estoyEnTerceraPersona = true;
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            }

            
            

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

    private void CambiarCamara()
    {
        
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
        
      if(Input.GetKeyDown(KeyCode.F) && !estaEsquivando )
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
        


        if (other.gameObject.CompareTag("Arma"))
        {
            Items item = other.GetComponent<Items>();


            int ArmaNº = item.Valor;
            tieneArma[ArmaNº] = true;


            Destroy(other.gameObject);
        }
        
    }
    public void RecibirDanho(float danhoEnemigo)
    {
        VidasPantalla();
      vidas -= danhoEnemigo;

        if (vidas <= 0)
        {
            Muerte();
        }
    }
    public void CambiarArma()
    {

        


        int ArmaNº = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ArmaNº = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ArmaNº = 1;
        }
        else if ((Input.GetKeyDown(KeyCode.Alpha3)))
        {
            ArmaNº = 2;
        }
        if (ArmaNº >= 0 && ArmaNº < Armas.Length && tieneArma[ArmaNº])
        {
            if (equiparArma != null)
            {
                Armas[ArmaNº].SetActive(false);
                equiparArma.SetActive(false);
            }

            equiparArma = Armas[ArmaNº];
            Armas[ArmaNº].SetActive(true);

            animator.SetTrigger("swap");
            estaCambiandoArma = true;

            Invoke("TerminarCambiarArma", 0.4f);
        }
    }
    private void TerminarCambiarArma()
    {
        estaCambiandoArma = false; 
    }
    private void Muerte()
    {
        estaVivo = false;
        estaMuerto = true;
        animator.SetTrigger("die");  

        
        Invoke("MostrarPantallaMuerte", 1f);
    }
    private void MostrarPantallaMuerte()
    {
        if(pantallaMuerte != null)
        {
            pantallaMuerte.SetActive(true);
            
        }
    }
    private void VidasPantalla()
    {
        TextoVidas.text = "Vidas: " + vidas;
    }
 

}
