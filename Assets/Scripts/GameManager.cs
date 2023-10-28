using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    TMP_Text text;

    [SerializeField]
    TMP_Text armaActual;

    int totalEnemyCount;
    int currentEnemyKilled = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemigo").Length;
        updateUI();
    }
    
    public void updateEnemyCount()
    {
        currentEnemyKilled++;
        updateUI();
    }

    void updateUI()
    {
        text.text = currentEnemyKilled.ToString() + " / " + totalEnemyCount.ToString();
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
