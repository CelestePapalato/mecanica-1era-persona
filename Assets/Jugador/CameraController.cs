using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float sensibilidad;

    [SerializeField]
    [Range(0f, 0.3f)]float suavizado;

    Vector2 suavidadV;

    // Componentes
    GameObject jugador;

    // Variables
    float rotacionEjeX;

    void Start()
    {
        jugador = this.transform.parent.gameObject;
        rotacionEjeX = transform.localEulerAngles.x;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        var movimiento = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        suavidadV.x = Mathf.SmoothStep(suavidadV.x, movimiento.x * sensibilidad, suavizado);
        suavidadV.y = Mathf.SmoothStep(suavidadV.y, movimiento.y * sensibilidad, suavizado);

        // Rotamos al jugador horizontalmente
        jugador.transform.Rotate(Vector3.up, suavidadV.x);

        // Rotamos verticalmente la cámara
        // 1 - Obtenemos la rotación objetivo de la cámara
        rotacionEjeX += suavidadV.y;

        // 2- Limitamos la rotación total a 90 grados
        rotacionEjeX = Mathf.Clamp(rotacionEjeX, -90, 90);

        // 3- Rotamos en valores locales
        transform.localRotation = Quaternion.Euler(-rotacionEjeX, 0, 0);

    }

    public void cambiarSensibilidad(Slider slider)
    {
        sensibilidad = slider.value;
    }

}
