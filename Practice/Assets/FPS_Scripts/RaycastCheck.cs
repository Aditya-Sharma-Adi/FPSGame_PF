using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] LayerMask layerMask;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 300, layerMask))
            {
                Debug.Log("GameObject Name..... " + hit.collider.name);
            }
        }
    }
}
