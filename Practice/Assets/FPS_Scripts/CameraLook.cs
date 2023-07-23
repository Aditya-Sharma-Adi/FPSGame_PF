using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform camPos;
    public Transform currentPos;

    [SerializeField] Transform rightHand;

    [SerializeField] float smoothMovement;

    private void Start()
    {
        smoothMovement = 5;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = currentPos.position;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothMovement);
        transform.position = smoothPosition;
    }

}
