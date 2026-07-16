using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    [SerializeField] private float MaxFadeDuration = 15;
    [SerializeField] private float MinFadeDuration = 10;

    private Renderer objRenderer;
    private Material fadeMaterial;
    private float currentFadeTime = 0f;
    private float fadeDuration;

    void Start()
    {
        fadeDuration = Random.Range(MinFadeDuration, MaxFadeDuration);
        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            fadeMaterial = new Material(objRenderer.material);
            objRenderer.material = fadeMaterial;

            fadeMaterial.SetFloat("_Mode", 3);
            fadeMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            fadeMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            fadeMaterial.SetInt("_ZWrite", 0);
            fadeMaterial.DisableKeyword("_ALPHATEST_ON");
            fadeMaterial.EnableKeyword("_ALPHABLEND_ON");
            fadeMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            fadeMaterial.renderQueue = 3000;
        }

        // Запускаем исчезновение
        StartCoroutine(FadeOutAndDestroy());
    }

    System.Collections.IEnumerator FadeOutAndDestroy()
    {
        currentFadeTime = 0f;

        Color startColor = fadeMaterial.color;
        float startAlpha = startColor.a;

        while (currentFadeTime < fadeDuration)
        {
            currentFadeTime += Time.deltaTime;

            float t = currentFadeTime / fadeDuration;

            Color newColor = startColor;
            newColor.a = Mathf.Lerp(startAlpha, 0f, t);

            fadeMaterial.color = newColor;

            yield return null;
        }

        // гарантируем полную прозрачность
        Color finalColor = startColor;
        finalColor.a = 0f;
        fadeMaterial.color = finalColor;

        Destroy(gameObject);
    }
}
