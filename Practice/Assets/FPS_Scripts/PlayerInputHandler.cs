using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    Vector2 moveInputVector;
    Vector2 mouseinput;
    bool isJump;
    bool isFire;
    bool isAim;
    private void Update()
    {
        if (!playerMovement.Object.HasInputAuthority)
            return;

        mouseinput.x = Input.GetAxis("Mouse X");
        mouseinput.y = Input.GetAxis("Mouse Y");

        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            isJump = true;

        if (Input.GetMouseButtonDown(0))
            isFire = true;
        if (Input.GetMouseButtonDown(1))
            isAim = true;
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        networkInputData.aimLookVector = mouseinput;
        networkInputData.movementInput = moveInputVector;
        networkInputData.isJumping = isJump;
        networkInputData.isFiring = isFire;
        networkInputData.isAiming = isAim;

        isAim = false;
        isFire = false;
        isJump = false;
        return networkInputData;
    }

}
