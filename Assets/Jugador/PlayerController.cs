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
    private float velocidad = 60f;
    [Header("Drag")]
    [SerializeField]
    private float drag = 25f;
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
        if(Time.timeScale == 0)
        {
            return;
        }

        mover();
        agregarDrag();
        cambiarWeapon();
    }

    void mover()
    {
        if((Input.GetAxisRaw("Horizontal") == 0 &&
            Input.GetAxisRaw("Vertical") == 0))
        {
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Para que el jugador se mueva en dirección a la cámara, usamos los vectores proporcionados
        // por el transform y los sumamos
        Vector3 movimientoJugador = transform.right * x + transform.forward * z;
        movimientoJugador = movimientoJugador.normalized * velocidad * Time.deltaTime;

        rb.AddForce(movimientoJugador, ForceMode.VelocityChange);

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
