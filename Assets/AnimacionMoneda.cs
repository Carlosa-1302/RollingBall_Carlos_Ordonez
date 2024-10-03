using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionMoneda : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0,0,0 * 35 * Time.deltaTime,Space.World);//el space.world es para que use x,y,z del mundo no del objeto
        
        transform.Rotate (new Vector3(0,1,0) * 10 * Time.deltaTime,Space.World);
    }
}
