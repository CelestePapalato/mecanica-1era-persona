using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    int vida = 3;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.CompareTag("Bullet"))
        {
            damage();
            Destroy(other.gameObject);
        }
    }

    private void damage()
    {
        vida--;
        if (vida == 0)
        {
            Destroy(this.gameObject);
            GameManager.instance.updateEnemyCount();
        }
    }

    public void RaycastDamage(GameObject other)
    {
        Debug.Log(other.tag);

        if (other.CompareTag("Weapon"))
        {
            damage();
        }
    }
}
