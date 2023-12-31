using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Variables de la partida")]
    [SerializeField]
    int tiempoDeJuego = 30;

    [Header("Spawn de enemigos")]
    [SerializeField]
    private Enemigo enemigo_prefab;

    [SerializeField]
    private GameObject posicionesSpawn;

    [Header("Interfaz de usuario")]
    [SerializeField]
    TMP_Text tiempoUI;

    [SerializeField]
    TMP_Text vidaJugador;

    [SerializeField]
    TMP_Text enemyCount;

    [SerializeField]
    TMP_Text armaActual;

    [SerializeField]
    Canvas canvasFinDePartida;

    [SerializeField]
    TMP_Text textoVictoria;

    [SerializeField]
    TMP_Text textoDerrota;

    [Header("Pausa")]
    [SerializeField]
    Canvas pausaCanvas;
    [SerializeField]
    TMP_Text textoSensibilidad;

    int totalEnemyCount = 0;
    int currentEnemyKilled = 0;
    float tiempoRestante;

    bool finPartida = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pausaCanvas.enabled = false;
        canvasFinDePartida.enabled = false;
        ComenzarJuego();
        updateUI();
    }

    private void Update()
    {
        if (finPartida)
        {
            return;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            pausarPartida();
        }
    }
    private void ComenzarJuego()
    {
        foreach (Transform child in posicionesSpawn.transform.GetComponentsInChildren<Transform>())
        {
            if (child != posicionesSpawn.transform)
            {
                Instantiate(enemigo_prefab, child.position, Quaternion.identity);
                totalEnemyCount++;
            }
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
        perder();
    }

    public void updateEnemyCount()
    {
        currentEnemyKilled++;
        updateUI();
        if(currentEnemyKilled >= totalEnemyCount)
        {
            ganar();
        }
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

    public void updateVidaJugadorUI(int vida)
    {
        vidaJugador.text = vida.ToString();
    }

    private void ganar()
    {
        Cursor.lockState = CursorLockMode.None;
        StopCoroutine(ComenzarCronometro());
        Time.timeScale = 0;
        textoDerrota.enabled = false;
        textoVictoria.enabled = true;
        canvasFinDePartida.enabled = true;
        finPartida = true;
    }

    public void perder()
    {
        Cursor.lockState = CursorLockMode.None;
        StopCoroutine(ComenzarCronometro());
        Time.timeScale = 0;
        textoDerrota.enabled = true;
        textoVictoria.enabled = false;
        canvasFinDePartida.enabled = true;
        finPartida = true;
    }

    public void recargarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    void pausarPartida()
    {
        if(Time.timeScale == 0)
        {
            pausaCanvas.enabled = false;
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        pausaCanvas.enabled = true;
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
    }

    public void updateSensitivityUI(Slider slider)
    {
        float value = slider.value;
        textoSensibilidad.text = "Sensibilidad: " + value.ToString();
    }
}
