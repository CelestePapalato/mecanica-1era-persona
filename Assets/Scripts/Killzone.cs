using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.perder();
            return;
        }

        if (other.gameObject.CompareTag("Enemigo"))
        {
            GameManager.instance.updateEnemyCount();
        }
        Destroy(other.gameObject);
    }
}
