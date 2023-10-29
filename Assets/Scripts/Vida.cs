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
            GameManager.instance.updateVidaJugadorUI(vida);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool shouldDamage = (isPlayer && other.gameObject.CompareTag("Enemigo")) || (!isPlayer && other.CompareTag("Bullet"));

        if (shouldDamage)
        {
            damage(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    private void damage(GameObject caller)
    {
        if(caller != gameObject)
        {
            return;
        }

        vida--;

        if (isPlayer)
        {
            GameManager.instance.updateVidaJugadorUI(vida);
        }

        if (vida == 0)
        {
            if (isPlayer)
            {
                GameManager.instance.perder();
                return;
            }
            GameManager.instance.updateEnemyCount();
            Destroy(gameObject);
        }
    }
}
