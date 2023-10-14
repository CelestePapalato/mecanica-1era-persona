using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    TMP_Text text;

    int totalEnemyCount;
    int currentEnemyCount;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemigo").Length;
        currentEnemyCount = totalEnemyCount;
        updateUI();
    }
    
    public void updateEnemyCount()
    {
        currentEnemyCount--;
        updateUI();
    }

    void updateUI()
    {
        text.text = currentEnemyCount.ToString() + " / " + totalEnemyCount.ToString();
    }
}
