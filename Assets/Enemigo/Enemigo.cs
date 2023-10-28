using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    Vida vida;

    private void Start()
    {
        vida = GetComponent<Vida>();
    }

    public void RaycastDamage(GameObject other)
    {
        if (other.CompareTag("Weapon"))
        {
            vida.damage(gameObject);
        }
    }
}
