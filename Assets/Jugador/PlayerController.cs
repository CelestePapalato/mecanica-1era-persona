using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("LayerMask del suelo")]
    [SerializeField]
    private LayerMask capaPiso;
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
    CapsuleCollider col;

    private Weapon[] weapons;
    private int indexWeaponActiva;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        weapons = GetComponentsInChildren<Weapon>();
        activarWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        mover();
        agregarDrag();
        cambiarWeapon();
    }

    void mover()
    {
        if((Input.GetAxisRaw("Horizontal") == 0 &&
            Input.GetAxisRaw("Vertical") == 0) || !onFloor())
        {
            return;
        }

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
        Vector3 velocidadExcedida = rb.velocity * -1;
        velocidadExcedida.y = 0;
        velocidadExcedida *= 1f / (1f - Time.deltaTime * drag);
        rb.AddForce(velocidadExcedida, modoDrag);
    }

    private void cambiarWeapon() {

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f) {

            int indexNuevaWeapon = (weapons.Length - 1 == indexWeaponActiva) ? 0 : indexWeaponActiva + 1;
            activarWeapon(indexNuevaWeapon);
        }

    }

    private void activarWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].enabled = (i == index);
        }

        indexWeaponActiva = index;

        GameManager.instance.updateArmaActualUI(weapons[index].getIsHitscan());
    }

    private bool onFloor()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, capaPiso);
    }
}
