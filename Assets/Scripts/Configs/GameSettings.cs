using UnityEngine;

[CreateAssetMenu(menuName = "Settings", order = 51)]
public class GameSettings : ScriptableObject
{
    public float orbitSensitivity = 3f;
    public float zoomSpeed = 10f;
    public float maxZoomFOV = 40f;
    public float minZoomFOV = 60f;
    public float firstPersonSensitivity = 2f;
    public float firstPersonSpeed = 2f;
    public float fovChangeSpeed = 5f;
}
