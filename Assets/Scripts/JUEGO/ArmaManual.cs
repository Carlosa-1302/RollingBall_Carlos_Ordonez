using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaManual : MonoBehaviour
{

    [SerializeField] private int armaActual = -1;
    

    [SerializeField] private ArmaSO misDatos;
    [SerializeField] private LayerMask queEsDanhable;//va a atravesar todo objeto a los que si sea dañable como un zombie detras del arbol, el arbol no es dañable pero el zombie si entonces pasa del arbol y daña al zombie aunque este detras
    [SerializeField] private ParticleSystem ParticleSystem;

    [Header("Distancia")]
    [SerializeField] private Transform balaPos;
    [SerializeField] private GameObject bala;

    [SerializeField] private Transform balaCasePos;
    [SerializeField] private GameObject balaCase;




    [Header("Melee")]
    [SerializeField] private TrailRenderer trailEffect;
    [SerializeField] private BoxCollider AreaMelee;
    [SerializeField] private GameObject weaponPoint;
    [SerializeField] private float tiempoEsperaEntreGolpes;
    private bool puedoDanharMelee = true;
    [SerializeField] private float duracionGolpe = 0.5f;


    private bool ventanaAbierta;
    



    private Animator anim;
    private Camera cam;
    private PlayerIsometrica player;
    private int municionActual;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //"MainCamera".
        player = GetComponentInParent<PlayerIsometrica>();

        

        if (AreaMelee != null)
        {
            AreaMelee.enabled = false;
        }


        if (player != null)
        {
            anim = player.GetComponentInChildren<Animator>();

        }
        municionActual = misDatos.balasCargador;
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)  )
        {
            
            //if (misDatos != null)
            //{
                if (misDatos.tipo == ArmaSO.TipoArma.Melee)
                {
                    GolpearMelee();
                }
                else if (misDatos.tipo == ArmaSO.TipoArma.Distancia)
                {
                    DispararDistancia();
                }
            //}
            

        }
    }

    private void GolpearMelee()
    {
        if (misDatos.tipo != ArmaSO.TipoArma.Melee) return;


        if(trailEffect != null)
        {
          trailEffect.enabled = true;
 
        }
        if(AreaMelee != null)
        {
          AreaMelee.enabled = true;

        }

        anim.SetTrigger("swing");
        
        
        Invoke("DetenerGolpeMelee", duracionGolpe);
        
    }
    private void DetenerGolpeMelee()
    {
        if (trailEffect != null)
        {
            trailEffect.enabled = false;
        }

        if (AreaMelee != null)
        {
            AreaMelee.enabled = false;
        }
        puedoDanharMelee = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!puedoDanharMelee || misDatos.tipo != ArmaSO.TipoArma.Melee) return;

        if (other.CompareTag("Enemigo"))
        {
            Enemigo enemigo = other.GetComponentInParent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirDanho(misDatos.danhoAtaque);
                puedoDanharMelee = false;
            }
        }

    }



    public void DispararDistancia()
    {
        if(misDatos.tipo != ArmaSO.TipoArma.Distancia) return;
        if (municionActual > 0)
        {
            GameObject BalaSpawn = Instantiate(bala, balaPos.position, balaPos.rotation);
            Rigidbody balaRigid = BalaSpawn.GetComponent<Rigidbody>();
            balaRigid.velocity = balaPos.forward * 50;

            GameObject BalaCasquillo = Instantiate(balaCase, balaCasePos.position, balaCasePos.rotation);
            Rigidbody balaCaseRigid = BalaCasquillo.GetComponent<Rigidbody>();
            Vector3 casquilloVec = balaCasePos.forward * Random.Range(-3,-2) + Vector3.up * Random.Range(2,3);
            balaCaseRigid.AddForce(casquilloVec,ForceMode.Impulse);
            balaCaseRigid.AddTorque(Vector3.up * 10,ForceMode.Impulse);



            anim.SetTrigger("shot");


            if (ParticleSystem != null)
            {
                ParticleSystem.Play();
            }


            if (Physics.Raycast(balaPos.position, balaPos.forward, out RaycastHit hitInfo, misDatos.distanciaAtaque))
            {

                //hitInfo.transform.GetComponent<Enemigo>().VidaEnemigo1-=misDatos.danhoAtaque; //encapsular no hace falta
                if (hitInfo.transform.CompareTag("Enemigo"))
                {
                    hitInfo.transform.GetComponentInChildren<Enemigo>().RecibirDanho(misDatos.danhoAtaque);
                }
                Debug.Log(hitInfo.transform.name);  //Muestra el nombre de a quien he impactado


            }
                municionActual--; 
        }
        else
        {
            Debug.Log("Sin balas");
        }
    }

}
