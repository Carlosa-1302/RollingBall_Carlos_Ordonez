using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallsDetector : MonoBehaviour
{
    [SerializeField] private float tiempoDifuminado;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstaculo"))
        {
           Material materialPared = other.GetComponent<MeshRenderer>().material;
            Color transparente = new Color(materialPared.color.r, materialPared.color.g, materialPared.color.b, 0.25f);
            materialPared.DOColor(transparente, tiempoDifuminado);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            Material materialPared = other.GetComponent<MeshRenderer>().material;
            Color opaco = new Color(materialPared.color.r, materialPared.color.g, materialPared.color.b, 1f);
            materialPared.DOColor(opaco, tiempoDifuminado);
        }
    }
}
