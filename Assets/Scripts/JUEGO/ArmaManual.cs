using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaManual : MonoBehaviour
{

    [SerializeField] private int armaActual = -1;
    

    [Header("Distancia")]
    [SerializeField] private ArmaSO misDatos;
    [SerializeField] private LayerMask queEsDanhable;//va a atravesar todo objeto a los que si sea dañable como un zombie detras del arbol, el arbol no es dañable pero el zombie si entonces pasa del arbol y daña al zombie aunque este detras
    [SerializeField] private ParticleSystem ParticleSystem;

    [Header("Melee")]
    [SerializeField] TrailRenderer trailEffect;
    [SerializeField] BoxCollider AreaMelee;

    private bool ventanaAbierta;
    private bool puedoDanhar = true;
    private Animator anim;



    private Camera cam;
    private Player player;

    private int municionActual;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //"MainCamera".
        player = GetComponentInParent<Player>();
        municionActual = misDatos.balasCargador;

        if (player != null)
        {
            anim = player.GetComponentInChildren<Animator>();
            
        }
            player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)  )
        {
            
            if (misDatos != null)
            {
                if (misDatos.tipo == ArmaSO.TipoArma.Melee)
                {
                    GolpearMelee();
                }
                else if (misDatos.tipo == ArmaSO.TipoArma.Distancia)
                {
                    DispararDistancia();
                }
            }
            

        }
    }

    private void GolpearMelee()
    {
        anim.SetTrigger("swing");
        trailEffect.enabled = true;
        AreaMelee.enabled = true;
        Invoke("DetenerGolpeMelee", 0.5f);
        
    }
    private void DetenerGolpeMelee()
    {
        trailEffect.enabled = false;
        AreaMelee.enabled = false;
    }
    public void DispararDistancia()
    {
        if (municionActual > 0)
        {
            anim.SetTrigger("shot");
            ParticleSystem.Play();    

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, misDatos.distanciaAtaque))
            {
                //1.Generea un metodo RecibirDanho(Float) en el script Enemigo
                //2. Desde este punto, ponte en comunicacion con el enemigo impactado para ejecutar su metodo "RecibirDanho"
                //3. Para ello, Necesitaras un daño: vien en el ScriptableObject
                //4. Completa el metodo "RecibirDanho(float)", para que el enemigo reciba daño (se le resta vidas), y si muere, Destroy 
                //hitInfo.transform.GetComponent<Enemigo>().VidaEnemigo1-=misDatos.danhoAtaque; //encapsular no hace falta
                if(hitInfo.transform.CompareTag("Enemigo"))
                {
                    hitInfo.transform.GetComponent<Enemigo>().RecibirDanho(misDatos.danhoAtaque);
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
