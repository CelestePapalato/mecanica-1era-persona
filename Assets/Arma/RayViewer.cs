using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{

    public float weaponRange;

    [SerializeField]
    private Camera cam;

    void Update()
    {
        Vector3 origen = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(origen, cam.transform.forward * weaponRange, Color.red);
    }

}
