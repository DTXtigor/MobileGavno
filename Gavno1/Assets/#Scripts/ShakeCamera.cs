using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private Transform StartPos;
    IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(StartPos.transform.position.x + x, StartPos.transform.position.y + y, StartPos.transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = StartPos.transform.position;
    }
    public void TriggerShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    public void TriggerShackeTick(float magnitude)
    {
        StartCoroutine(ShackeTick(magnitude));
    }

    IEnumerator ShackeTick(float magnitude)
    {
        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;
        transform.position = new Vector3(StartPos.transform.position.x + x, StartPos.transform.position.y + y, StartPos.transform.position.z);
        yield return null;
        
        yield return new WaitForSeconds(0.05f);
        transform.position = StartPos.transform.position;
    }
}
