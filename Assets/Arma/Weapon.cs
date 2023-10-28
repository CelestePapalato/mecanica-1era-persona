using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private bool isHitscan;

    [Header("Si el arma es un proyectil")]
    [SerializeField]
    private Bala balaPrefab;
    [SerializeField]
    private float fuerza;

    [Header("Si el arma es un hitscan")]
    [SerializeField]
    private float weaponRange;
    [SerializeField]
    private float fireRate = 0.25f;
    [SerializeField]
    private Camera cam;

    private float shotTimer = 0f;

    private LineRenderer lineRenderer;
    private bool isLineRendering = false;
    private float lineTimer = 0f;
    private float lineDuration = 0.07f;

    private void Start()
    {
        if (isHitscan)
        {
            lineRenderer = GetComponent<LineRenderer>();
            controlarLineRenderer(false);

            RayViewer rayViewer = GetComponent<RayViewer>();
            if (rayViewer)
            {
                rayViewer.weaponRange = weaponRange;
            }
        }
    }

    void Update()
    {
        if (isHitscan)
        {
            temporizadorLineRenderer();
            disparoHitscan();
        }
        else
        {
            disparoProyectil();
        }
    }

    private void disparoProyectil()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bala bala = Instantiate(balaPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.AddRelativeForce(transform.up * fuerza, ForceMode.Impulse);
        }
    }

    private void controlarLineRenderer(bool value)
    {
        lineRenderer.enabled = value;
    }

    private void temporizadorLineRenderer()
    {
        if (!isLineRendering)
        {
            return;
        }

        lineTimer += Time.deltaTime;

        if (lineTimer > lineDuration)
        {
            lineTimer = 0f;
            isLineRendering = false;
            controlarLineRenderer(false);
        }
    }

    private void disparoHitscan()
    {
        shotTimer += Time.deltaTime;

        // Si el jugador puede disparar y presion� el bot�n
        if (Input.GetMouseButtonDown(0) && shotTimer > fireRate)
        {
            // seteamos el timer de la cadencia de tiro en 0
            shotTimer = 0f;

            // activamos el lineRenderer
            controlarLineRenderer(true);

            // Obtenemos el punto origen del disparo (centro de la c�mara) para el raycast
            Vector3 origen = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;

            // El primer v�rtice del lineRenderer ser� la posici�n del spawnPoint
            lineRenderer.SetPosition(0, spawnPoint.position);

            // Si el raycast le dio a algo
            if (Physics.Raycast(origen, cam.transform.forward, out hit, weaponRange))
            {

                Debug.Log(hit.collider.gameObject.name);

                // El segundo v�rtice del lineRenderer ser� el punto donde golpe� el raycast
                lineRenderer.SetPosition(1, hit.point);

                // Verificamos que la colisi�n con la que choc� el raycast sea un enemigo y le
                // restamos vida
                Enemigo enemigo = hit.collider.gameObject.GetComponent<Enemigo>();
                if (enemigo)
                {
                    enemigo.RaycastDamage(gameObject);
                }

            }
            // Si el raycast no le dio a nada, el segundo v�rtice del line Renderer ser�
            // el rango m�ximo del tiro
            else
            {
                lineRenderer.SetPosition(1, origen + cam.transform.forward * weaponRange);
            }

            isLineRendering = true;

        }
    }
}
