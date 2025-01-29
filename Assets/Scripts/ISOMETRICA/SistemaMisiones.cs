using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaMisiones : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private ToogleMision[] tooglesMision;
    private void OnEnable()
    {
        //Me suscribo 
        eventManager.OnNuevaMision += EncenderToggleMision;
        eventManager.OnActualizarMision += ActualizarToggleMision;
        eventManager.OnTerminarMision += TerminarToggleMision;
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
    private void ActualizarToggleMision(MisionSO mision)
    {
        tooglesMision[mision.indiceMision].TextoMision.text = mision.ordenInicial;
        tooglesMision[mision.indiceMision].TextoMision.text += "(" + mision.repeticionActual + "/" + mision.totalRepeticiones + ")";
    }

    private void TerminarToggleMision(MisionSO mision)
    {
        tooglesMision[mision.indiceMision].ToggleVisual.isOn = true;//Al terminar la mision "Checkeamos" el toggle
        tooglesMision[mision.indiceMision].TextoMision.text = mision.ordenFinal;// Ponemos el texto de Victoria
    }


    
}
