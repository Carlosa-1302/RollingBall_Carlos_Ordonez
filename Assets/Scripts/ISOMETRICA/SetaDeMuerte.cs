using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaDeMuerte : MonoBehaviour, IInteractuable
{
    private Outline outline;

    [SerializeField] private MisionSO mision;

    [SerializeField] private EventManagerSO eventManager;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Interactuar()
    {
        mision.repeticionActual++;  //Aumentamos en un la repeticion de esta Mision


        //Todavia quedan setas por recoger
        if (mision.repeticionActual < mision.toalRepeticions)
        {
            eventManager.ActualizarMision(mision);
        }
        else//Ya hemos terminado de recotger todas las setas
        {
            eventManager.TerminarMision(mision);
        }
        
        Destroy(gameObject);
    }

    private void OnMouseEnter() => outline.enabled = true;//Es una forma de ponerlo con Lamba
    

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
