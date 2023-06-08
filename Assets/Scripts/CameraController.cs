using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private eCameraMode currentType;
    [SerializeField] private HandleMode orbitalMode;
    [SerializeField] private HandleMode firstPlayerMode;
   
    private Rigidbody _rb;
    private Transform _cameraTransform;
    private HandleMode _currentMode;

    public event Action<eCameraMode> ModeSwiched;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.freezeRotation = true;

        _cameraTransform = mainCamera.transform;

        SetStartMode();
    }

    private void SetStartMode()
    {
        _currentMode = currentType switch
        {
            eCameraMode.Orbit => orbitalMode,
            eCameraMode.FirstPerson => firstPlayerMode,
            _ => _currentMode
        };
        ModeSwiched?.Invoke(currentType);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentType = currentType is eCameraMode.Orbit ? eCameraMode.FirstPerson :  eCameraMode.Orbit;
            SetStartMode();
        }
        _currentMode.HandleCameraMode(_cameraTransform, mainCamera);
    }

    private void FixedUpdate()
    {
        _currentMode.HandleCameraModeRb(_cameraTransform, _rb);
    }
}
