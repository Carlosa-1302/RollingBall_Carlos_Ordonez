using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaMisiones : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private ToogleMision[] tooglesMision;
    private void OnEnable()
    {
        eventManager.OnNuevaMision += EncenderToggleMision;
        eventManager.OnActualizarMision += ActualizarToggleMision;
        eventManager.OnTerminarMision += TerminarToggleMision;
    }

    private void TerminarToggleMision(MisionSO obj)
    {
        throw new System.NotImplementedException();
    }

    private void ActualizarToggleMision(MisionSO obj)
    {
        throw new System.NotImplementedException();
    }

    private void EcenderToggleMision(MisionSO mision)
    {
        //Alimentos el texto con el contenido de la mision
        tooglesMision[mision.indiceMision].TextoMision.text = mision.ordenInicial;
        //Y si tiene repeticion.....
        if(mision.TieneRepeticion)
        {
            tooglesMision[mision.indiceMision].TextoMision.text += "(" + mision.repeticionActual + "/" + mision.totalRepeticiones + ")";
        }
        tooglesMision[mision].gameObject.SetActive(true);//ENciendo el toggle para que se vea en Pantalla
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
