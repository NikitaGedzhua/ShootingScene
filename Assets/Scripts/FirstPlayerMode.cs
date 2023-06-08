using System.Collections;
using UnityEngine;

public class FirstPlayerMode : HandleMode
{
    private float _rotationX;
    private float _rotationY;
    private bool _isTransitioningFOV;
    
    private readonly string _horizontalAxis = "Horizontal";
    private readonly string _verticalAxis = "Vertical";
    private readonly string _mouseXAxis = "Mouse X";
    private readonly string _mouseYAxis = "Mouse Y";

    public override void HandleCameraMode(Transform cameraTransform, Camera mainCamera)
    {
        HandleRotation(cameraTransform);
        TryTransitionCameraFOV(mainCamera);
    }

    public override void HandleCameraModeRb(Transform cameraTransform, Rigidbody rb)
    {
        HandleMovement(cameraTransform, rb);
    }

    private void HandleMovement(Transform cameraTransform, Rigidbody rb)
    {
        var moveHorizontal = Input.GetAxis(_horizontalAxis);
        var moveVertical = Input.GetAxis(_verticalAxis);

        var movement = cameraTransform.right * moveHorizontal + cameraTransform.forward * moveVertical;
        movement *= settings.firstPersonSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void HandleRotation(Transform cameraTransform)
    {
        float rotateHorizontal = Input.GetAxis(_mouseXAxis) * settings.firstPersonSensitivity;
        float rotateVertical = Input.GetAxis(_mouseYAxis) * settings.firstPersonSensitivity;

        _rotationX += rotateHorizontal;
        _rotationY -= rotateVertical;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(_rotationY, _rotationX, 0f);
        cameraTransform.rotation = rotation;
    }
    
    private void TryTransitionCameraFOV(Camera mainCamera)
    {
        if (Mathf.Abs(mainCamera.fieldOfView - settings.minZoomFOV) > 0.01f && !_isTransitioningFOV)
        {
            StartCoroutine(TransitionFOV(mainCamera));
        }
    }
       
    private IEnumerator TransitionFOV(Camera mainCamera)
    {
        _isTransitioningFOV = true;
        var targetFOV = settings.minZoomFOV;
        var currentFOV = mainCamera.fieldOfView;

        while (Mathf.Abs(currentFOV - targetFOV) > 0.01f)
        {
            currentFOV = Mathf.Lerp(currentFOV, targetFOV, settings.fovChangeSpeed * Time.deltaTime);
            mainCamera.fieldOfView = currentFOV;
            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
        _isTransitioningFOV = false;
    }
}
