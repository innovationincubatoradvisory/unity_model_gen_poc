using UnityEngine;

public class DynamicBackgroundColor : MonoBehaviour
{
    public Color backgroundColor = Color.grey;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        UpdateBackgroundColor();
    }

    public void SetBackgroundColor(string colorJson)
    {
        Color newColor = JsonUtility.FromJson<Color>(colorJson);
        backgroundColor = newColor;
        UpdateBackgroundColor();
    }

    private void UpdateBackgroundColor()
    {
        mainCamera.backgroundColor = backgroundColor;
    }
}
