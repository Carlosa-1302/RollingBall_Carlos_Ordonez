using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodillo : MonoBehaviour
{
    
    [SerializeField]Vector3 direccionRodillo;
    [SerializeField] private float fuerzaR;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddTorque(direccionRodillo * fuerzaR,ForceMode.VelocityChange);
        //rb.AddTorque(direccionRodillo * 5, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
