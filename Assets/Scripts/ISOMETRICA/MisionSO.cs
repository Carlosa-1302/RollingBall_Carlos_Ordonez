using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Mision")] 
public class MisionSO : ScriptableObject
{
    public string ordenInicial;//mensaje inicial.
    public string ordenFinal;
    public bool tieneRepeticion;
    public int totalRepeticiones;
    public int indiceMision;
    [NonSerialized]
    public int repeticionActual=0;
}
