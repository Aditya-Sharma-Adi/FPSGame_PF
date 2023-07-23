using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGunPosition : MonoBehaviour
{
    [SerializeField] Transform gunPoint;


    private void Update()
    {
        if (gunPoint != null)
            gameObject.transform.position = gunPoint.position;
    }

}
