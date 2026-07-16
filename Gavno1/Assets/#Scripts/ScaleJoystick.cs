using UnityEngine;

public class ScaleJoystick : MonoBehaviour
{
    [SerializeField] private float maxScale = 2f;
    [SerializeField] private float minScale = 0.5f;

    private float currentScale;

    private void Start()
    {
        Scale();
    }

    public void Scale()
    {
        currentScale = PlayerPrefs.GetFloat("JoysticSize", 0.5f);
        transform.localScale = Vector3.one * Mathf.Clamp(currentScale * maxScale, minScale, maxScale);
    }
}
