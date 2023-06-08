using UnityEngine;

public class OrbitMode : HandleMode
{
    private float _rotationX;
    private float _rotationY;
    
    private readonly string _mouseXAxis = "Mouse X";
    private readonly string _mouseYAxis = "Mouse Y";
    private readonly string _mouseScroll = "Mouse ScrollWheel";

    public override void HandleCameraMode(Transform cameraTransform, Camera mainCamera)
    {
        HandleZoom(mainCamera);
    }

    public override void HandleCameraModeRb(Transform cameraTransform, Rigidbody rb)
    {
        if (Input.GetMouseButton(0))
        {
            HandleOrbitInput(rb);
        }
        TryResetVelocity(rb);
    }

    private void TryResetVelocity(Rigidbody rb)
    {
        if (rb.velocity.sqrMagnitude > 0f)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void HandleOrbitInput(Rigidbody rb)
    {
        var orbitInputX = Input.GetAxis(_mouseXAxis) * settings.orbitSensitivity;
        var orbitInputY = Input.GetAxis(_mouseYAxis) * settings.orbitSensitivity;

        _rotationX += orbitInputX;
        _rotationY -= orbitInputY;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(_rotationY, _rotationX, 0f);
        rb.MoveRotation(rotation);
    }

    private void HandleZoom(Camera mainCamera)
    {
        var zoomInput = Input.GetAxis(_mouseScroll);
        float zoomAmount = zoomInput * settings.zoomSpeed;

        float currentFOV = mainCamera.fieldOfView;
        float newFOV = currentFOV - zoomAmount;
        newFOV = Mathf.Clamp(newFOV, settings.maxZoomFOV, settings.minZoomFOV);

        mainCamera.fieldOfView = newFOV;
    }
}
