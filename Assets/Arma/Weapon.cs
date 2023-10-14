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
            Bala bala = Instantiate(balaPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.AddRelativeForce(transform.up * bala.getFuerza(), ForceMode.Impulse);
        }
    }
}
