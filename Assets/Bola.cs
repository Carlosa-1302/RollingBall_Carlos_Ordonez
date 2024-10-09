using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    //se llama a la variable y pones nombre
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //este es mas optimo
        rb = GetComponent<Rigidbody>();

        rb.mass = 5;
        rb.isKinematic = true;
        rb.drag = 4f;


        //esto es menos optimo
        GetComponent<Rigidbody>().mass = 5;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().drag = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3 (h,0, 0) * 100, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3 (0,0,y) * 100,ForceMode.Force);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector3 (0,1,0) * 5, ForceMode.Impulse);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            Destroy(other.gameObject);
        }
    }
}
