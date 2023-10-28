using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    int tiempoDeJuego = 30;

    [SerializeField]
    private Enemigo enemigo_prefab;

    [SerializeField]
    private Transform[] posicionesSpawn;

    [SerializeField]
    TMP_Text tiempoUI;

    [SerializeField]
    TMP_Text enemyCount;

    [SerializeField]
    TMP_Text armaActual;

    int totalEnemyCount = 0;
    int currentEnemyKilled = 0;
    float tiempoRestante;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ComenzarJuego();
        updateUI();
    }
    
    private void ComenzarJuego()
    {
        foreach (Transform spawnPoint in posicionesSpawn)
        {
            Instantiate(enemigo_prefab, spawnPoint.position, Quaternion.identity);
            totalEnemyCount++;
        }

        StartCoroutine(ComenzarCronometro());        
    }

    private IEnumerator ComenzarCronometro()
    {
        tiempoRestante = tiempoDeJuego;
        while(tiempoRestante > 0)
        {
            tiempoUI.text = tiempoRestante.ToString();
            yield return new WaitForSeconds(1.0f);
            tiempoRestante--;
        }
    }

    public void updateEnemyCount()
    {
        currentEnemyKilled++;
        updateUI();
    }

    void updateUI()
    {
        enemyCount.text = currentEnemyKilled.ToString() + " / " + totalEnemyCount.ToString();
    }

    public void updateArmaActualUI(bool isHitscan)
    {
        armaActual.text = "Proyectil";

        if (isHitscan)
        {
            armaActual.text = "Raycast";
        }

    }
}
