using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            Vector3 moveDirection = transform.right * networkInputData.movementInput.x + transform.forward * networkInputData.movementInput.y;
            moveDirection.Normalize();
            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            if (networkInputData.isJumping)
                networkCharacterControllerPrototypeCustom.Jump();
        }
    }
}
