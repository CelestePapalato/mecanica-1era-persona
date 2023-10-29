using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private float pathrecalculationRate;

    private NavMeshAgent agent;
    private GameObject jugador;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("recalculatePath", .2f, pathrecalculationRate);
    }

    private void Update()
    {
        transform.LookAt(jugador.transform);
    }

    private void recalculatePath()
    {

        agent.destination = jugador.transform.position;
    }

}
