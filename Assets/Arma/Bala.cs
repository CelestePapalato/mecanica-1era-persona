using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField]
    private float fuerza = 5f;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    public float getFuerza()
    {
        return fuerza;
    }
}
