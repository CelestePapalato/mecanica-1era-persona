using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Constantes
    [SerializeField]
    float sensibilidad = 5f;
    [SerializeField]
    [Range(0f, 0.3f)] float suavizado = 0.2f;

    // Componentes
    GameObject jugador;

    // Variables
    float rotacionEjeX;

    Vector2 suavidadV;
    Vector2 currentVelocity;

    void Start()
    {
        jugador = this.transform.parent.gameObject;
        rotacionEjeX = transform.localEulerAngles.x;
    }

    void Update()
    {
        var movimiento = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")).normalized;
        movimiento *= sensibilidad;

        // Suavizamos el cambio
        suavidadV = Vector2.SmoothDamp(suavidadV, movimiento, ref currentVelocity, suavizado);

        // Rotamos al jugador horizontalmente
        jugador.transform.Rotate(Vector3.up, suavidadV.x);

        // Rotamos verticalmente la cámara
        // 1 - Obtenemos la rotación actual de la cámara
        rotacionEjeX += suavidadV.y;

        // 2- Limitamos la rotación total a 90 grados
        rotacionEjeX = Mathf.Clamp(rotacionEjeX, -90, 90);

        // 3- Rotamos en valores locales
        transform.localRotation = Quaternion.Euler(-rotacionEjeX, 0, 0);
    }
}
