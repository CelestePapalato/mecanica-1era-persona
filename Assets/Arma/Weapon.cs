using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Bala balaPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private bool isHitscan;

    [SerializeField]
    private float fireRate = 0.25f;

    [Header("Si el arma es un proyectil")]
    [SerializeField]
    private float fuerza;

    [Header("Si el arma es un hitscan")]
    [SerializeField]
    private float weaponRange;
    [SerializeField]
    private Camera cam;

    private bool canShoot = true;

    private LineRenderer lineRenderer;
    private float lineDuration = 0.07f;

    private void Start()
    {
        if (isHitscan)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;

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
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            // Controlamos la cadencia de tiro con una corutina
            StartCoroutine(controlarCadencia());
            Bala bala = Instantiate(balaPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.AddRelativeForce(transform.up * fuerza, ForceMode.Impulse);
        }
    }

    private void disparoHitscan()
    {
        // Si el jugador puede disparar y presionó el botón
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            // Controlamos la cadencia de tiro con una corutina
            StartCoroutine(controlarCadencia());

            // activamos el lineRenderer
            StartCoroutine(temporizadorLineRenderer());

            // Obtenemos el punto origen del disparo (centro de la cámara) para el raycast
            Vector3 origen = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;

            // El primer vértice del lineRenderer será la posición del spawnPoint
            lineRenderer.SetPosition(0, spawnPoint.position);

            // Si el raycast le dio a algo
            if (Physics.Raycast(origen, cam.transform.forward, out hit, weaponRange))
            {

                Debug.Log(hit.collider.gameObject.name);

                // El segundo vértice del lineRenderer será el punto donde golpeó el raycast
                lineRenderer.SetPosition(1, hit.point);

                Instantiate(balaPrefab, hit.point, Quaternion.identity);

            }
            // Si el raycast no le dio a nada, el segundo vértice del line Renderer será
            // el rango máximo del tiro
            else
            {
                lineRenderer.SetPosition(1, origen + cam.transform.forward * weaponRange);
            }

        }
    }

    private IEnumerator temporizadorLineRenderer()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }

    private IEnumerator controlarCadencia()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public bool getIsHitscan()
    {
        return isHitscan;
    }
}
