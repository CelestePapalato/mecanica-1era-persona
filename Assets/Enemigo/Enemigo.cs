using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private float velocidad;

    private Vida vida;
    private GameObject jugador;

    private void Start()
    {
        vida = GetComponent<Vida>();
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.LookAt(jugador.transform);
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}
