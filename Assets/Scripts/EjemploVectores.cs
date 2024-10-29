using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemploVectores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(5, 3, 0);
        transform.eulerAngles = new Vector3(25, 90, 0);
        transform.localScale = Vector3.one * 2;
        //transform.localScale = new Vector3(2, 2, 2); es lo mismo que arriba de escalar
    }

    // Update is called once per frame
    void Update()
    {
        //MOVIMINETOS CINEMATICOS
        transform.position += new Vector3(1, 0, 0) * 5 * Time.deltaTime;
    }
}
