using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using Input = UnityEngine.Input;

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
        animator = GetComponentInChildren<Animator>();
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


        Vector3 input = new Vector3(h, v,0);

        float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        transform.eulerAngles = new Vector3(0, anguloRotacion, 0);
        
        Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;

        if (input.magnitude > 0)
        {

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);

            animator.SetBool("walking", true);
            animator.SetBool("running", false);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(movimiento * velocidadCorrer * Time.deltaTime);

            animator.SetBool("walking", false);
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("running", false);
        }

    }

}


