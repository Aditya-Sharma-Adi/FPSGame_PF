using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAiming : MonoBehaviour
{
    [SerializeField] Animator ani;
    [SerializeField] CameraLook camLook;
    [SerializeField] Transform scopeAim;

    private void Update()
    {
        TakeAim();
    }

    void TakeAim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ani.SetBool("IsAiming", true);
            camLook.currentPos = scopeAim;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            camLook.currentPos = camLook.camPos;
            ani.SetBool("IsAiming", false);
        }
    }

}
