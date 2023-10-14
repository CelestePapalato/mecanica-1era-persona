using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Bala balaPrefab;
    [SerializeField]
    private Transform spawnPoint;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(balaPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
