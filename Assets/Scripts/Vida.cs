using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField]
    int vida = 3;

    bool isPlayer = false;

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool shouldDamage = (isPlayer && other.gameObject.CompareTag("Enemigo")) || (!isPlayer && other.CompareTag("Bullet"));

        if (shouldDamage)
        {
            damage(gameObject);
        }
    }

    public void damage(GameObject caller)
    {
        if(caller != gameObject)
        {
            return;
        }

        vida--;
        if (vida == 0)
        {
            if (isPlayer)
            {
                return;
            }
            Destroy(gameObject);
            GameManager.instance.updateEnemyCount();
        }
    }
}
