using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Printer : MonoBehaviour
{
    private int currentImage = -1;
    [SerializeField] private GameObject[] buttons;

    [SerializeField] private GameObject printer;
    [SerializeField] private float shakeDuration = 5f;
    [SerializeField] private float shakeMagnitude = 0.1f;
    public bool isPrinting = false;

    public Sprite[] images;
    [SerializeField] private Transform spawnPositionList;
    [SerializeField] private Transform targetPositionList;
    [SerializeField] private GameObject emptyList;

    public GameObject newList;

    [SerializeField] private GameObject[] err;

    public void Print()
    {
        if (currentImage == -1)
        {
            err[0].SetActive(true);
            return;
        }
        if (newList != null)
        {
            err[1].SetActive(true);
            return;
        }
        isPrinting = true;
        StartCoroutine(Shake());
        newList = Instantiate(emptyList, spawnPositionList.position, spawnPositionList.rotation);
        newList.GetComponentInChildren<Image>().sprite = images[currentImage];
        newList.GetComponent<IList>().idList = currentImage;
        StartCoroutine(Printing());
        foreach(GameObject er in err) er.SetActive(false);
    }

    private IEnumerator Printing()
    {
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            newList.transform.position = Vector3.Lerp(spawnPositionList.position, targetPositionList.position, elapsed / shakeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        newList.transform.position = targetPositionList.position;
        isPrinting = false;
    }
    private IEnumerator Shake()
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public void ChangeImage(int index)
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().color = button.GetComponent<Button>().colors.normalColor;
        }
        if(index == currentImage)
            {
            currentImage = -1;
            return;
        }
        currentImage = index;
        buttons[currentImage].GetComponent<Image>().color = buttons[currentImage].GetComponent<Button>().colors.pressedColor;       
    }
}
