using UnityEngine;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour
{
    public Sprite[] images;
    [SerializeField] private Image imageView;
    [SerializeField] private GameObject parentImage;

    public void ShowImage(int i)
    {           
        imageView.sprite = images[i];
        parentImage.SetActive(true);
    }
}
