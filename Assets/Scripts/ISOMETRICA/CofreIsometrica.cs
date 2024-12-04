using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreIsometrica : MonoBehaviour
{
    private Outline outline;
    // Start is called before the first frame update
    void Start()
    {
       outline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
        Debug.Log("esta dentro");
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
