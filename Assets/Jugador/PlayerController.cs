using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Constantes
    [Header("Movimiento")]
    [SerializeField]
    private float velocidad = 15f;
    [Header("Drag")]
    [SerializeField]
    private float drag = 5f;
    [SerializeField]
    private ForceMode modoDrag;

    // Componentes
    Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mover();
        agregarDrag();
        
    }

    void mover()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Para que el jugador se mueva en dirección a la cámara, usamos los vectores proporcionados
        // por el transform y los sumamos
        Vector3 movimientoJugador = transform.right * x + transform.forward * z;
        movimientoJugador = movimientoJugador.normalized * velocidad * Time.deltaTime;

        // Añadimos en el eje y del vector dirección el valor actual de la velocidad del rigidbody en y
        // Esto es porque usamos el modo Velocity Change, por lo que si es 0 la velocidad que el rb reciba
        // en ese eje se anula. 
        Vector3 direccion = new Vector3(movimientoJugador.x, rb.velocity.y, movimientoJugador.z);

        rb.AddForce(direccion, ForceMode.VelocityChange);

    }

    private void agregarDrag()
    {
        // Inicializamos el vector velocidadExcedida como el opuesto a la velocidad del rigidbody
        Vector3 velocidadExcedida = rb.velocity * -1;

        // Seteamos el valor de velocidad excedida del eje y en 0 para no afectar a la gravedad
        velocidadExcedida.y = 0;

        //Multiplicamos la velocidad excedida por esta fracción. El objetivo es
        // suavizar el efecto de la desacelerada que hará la bola
        // El denominador nunca será cero porque para eso el deltaTime debe ser 1,
        // y por su naturaleza es particularmente imposible que suceda.
        velocidadExcedida *= 1f / (1f - Time.deltaTime * drag);

        // Aplicamos la aceleración que detendrá a la bola
        rb.AddForce(velocidadExcedida, modoDrag);

        /*Otros métodos para limitar la velocidad del rigidbody:
            >>> Vector3.ClampMagnitude
            >>> Vector3.SmoothDamp
            >>> Operaciones de resta y suma directas con la velocidad del rigidbody
            >>> No es recomendable asignarle un nuevo vector al rigidbody
        */
    }
}
