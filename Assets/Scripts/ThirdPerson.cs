using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ThirdPerson : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadCorrer;
    private CharacterController controller;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverYRotar();
    }
    private void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        Vector2 input = new Vector3(h, v).normalized;

        float angulo = MathF.Atan2(input.x, input.y)* Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        transform.eulerAngles = new Vector3(0, angulo, 0);

        if(input.magnitude > 0 )
        {
            Vector3 movimiento = Quaternion.Euler(0,angulo,0)*Vector3.forward;

            controller.Move( movimiento* velocidadMovimiento * Time.deltaTime );
        }

        
    }
}

