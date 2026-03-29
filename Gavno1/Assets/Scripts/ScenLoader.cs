using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenLoader : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
