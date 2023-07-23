using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerLook : NetworkBehaviour
{
    [SerializeField] Transform targetAim;
    [SerializeField] Transform crossHairAim;

    public Camera cam;
    [SerializeField] float yVal, xRotation, smoothAim = 5;

    [SerializeField] NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;

    public float mouseSensitivity = 100;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            networkCharacterControllerPrototypeCustom.Rotate(networkInputData.aimLookVector.x);
            xRotation -= networkInputData.aimLookVector.y * Time.deltaTime * networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
            xRotation = Mathf.Clamp(xRotation, -50, 50);
            cam.gameObject.transform.localRotation = Quaternion.Euler(xRotation, yVal, 0);
            targetAim.position = Vector3.Lerp(targetAim.position, crossHairAim.position, smoothAim);
        }
    }

}
