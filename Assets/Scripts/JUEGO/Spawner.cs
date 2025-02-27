using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField]private Transform[] puntosSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnear());
    }

    // Update is called once per frame
    private IEnumerator Spawnear()
    {
        while(true)
        {
            Instantiate(enemigoPrefab, puntosSpawn[Random.Range(0, 7)].position, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
}
