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
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //"MainCamera".
        player = GetComponentInParent<Player>();
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
    private void DispararDistancia()
    {
        if (misDatos.balasCarador > 0)
        {
            anim.SetTrigger("shot"); 
            ParticleSystem.Play();    

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, misDatos.distanciaAtaque))
            {
                Debug.Log(hitInfo.transform.name); 
            }

            misDatos.balasCarador--; 
        }
    }

}
