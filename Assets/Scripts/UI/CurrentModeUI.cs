using TMPro;
using UnityEngine;

public class CurrentModeUI : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private TMP_Text modeText;

    private void OnEnable()
    {
        cameraController.ModeSwiched += ShowMode;
    }

    private void OnDisable()
    {
        cameraController.ModeSwiched -= ShowMode;
    }

    private void ShowMode(eCameraMode mode)
    {
        modeText.text = mode.ToString();
    }
}
