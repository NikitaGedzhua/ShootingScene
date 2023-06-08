using UnityEngine;

public abstract class HandleMode : MonoBehaviour
{
    [SerializeField] protected GameSettings settings;
    
    public abstract void HandleCameraMode(Transform cameraTransform, Camera mainCamera);
    public abstract void HandleCameraModeRb(Transform cameraTransform, Rigidbody rb);
}
