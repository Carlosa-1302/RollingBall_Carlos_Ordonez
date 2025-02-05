using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Mision")] 
public class ToogleMision : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textoMision;
    private Toggle toggleVisual;

    public TMP_Text TextoMision { get => textoMision; set => textoMision = value; }
    public Toggle ToggleVisual { get => toggleVisual; set => toggleVisual = value; }

    private void Awake()
    {
        toggleVisual = GetComponent<Toggle>();
    }
}
