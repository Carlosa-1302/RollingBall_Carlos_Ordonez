using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMinimapa : MonoBehaviour
{
    [SerializeField] private PlayerIsometrica player;

    private Vector3 distanciaAPlayer;
    void Start()
    {
        distanciaAPlayer = transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + distanciaAPlayer;
    }
}
