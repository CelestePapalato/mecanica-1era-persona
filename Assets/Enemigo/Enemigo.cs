using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    int vida = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            vida--;
            if(vida == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
