using UnityEngine;
using UnityEngine.UI;

public class PlayerPick : MonoBehaviour
{
    public int currentSlot = -1;

    [HideInInspector] public bool rotatingCamera;
    private Transform cameraPlayer;
    [SerializeField] private float SpeedMoveCamera = 2;
    [SerializeField] private Transform cameraPosition;
    private PlayerMove playerMove;

    public Image[] slotImage;
    public GameObject[] slotItem = new GameObject[4];

    [SerializeField] private Color selectedColor;
    [HideInInspector] public Color defaultColor;

    public bool loadInventoryOnStart = true;
    public bool resetInventoryOnStart = false;

    [SerializeField] private Image startImage;

    [HideInInspector] public int currentIdItemInHand;

    private void Start()
    {
        defaultColor = slotImage[0].color;
        cameraPlayer = Camera.main.transform;
        playerMove = GetComponent<PlayerMove>();

        if (resetInventoryOnStart)
        {
            for (int i = 0; i < slotItem.Length; i++)
            {
                PlayerPrefs.DeleteKey("Inventory " + i);
                slotItem[i] = null;
                slotImage[i].color = defaultColor;
            }
        }
        if (loadInventoryOnStart) LoadInventory();
    }
    public void SelectSlot(int i)
    {
        foreach (var image in slotImage) { image.color = defaultColor; }
        

        if (slotItem[i] && currentSlot == -1)
        {
            if (playerMove._InInterface) return;
            slotImage[i].color = selectedColor;
            currentSlot = i;
            slotItem[i].GetComponent<IPickable>().outInventory();
            if (slotItem[i].GetComponent<IList>()) currentIdItemInHand = i;
        }
        else if (currentSlot == i)
        {
            currentSlot = -1;
            slotItem[i].GetComponent<IPickable>().inInventory();
        }
        else if (slotItem[i] && currentSlot != -1)
        {
            slotItem[currentSlot].GetComponent<IPickable>().inInventory();
            slotImage[i].color = selectedColor;
            currentSlot = i;
            slotItem[i].GetComponent<IPickable>().outInventory();
            if (slotItem[i].GetComponent<IList>()) currentIdItemInHand = i;
        }
        else if (!slotItem[i] && currentSlot != -1)
        {
            slotItem[currentSlot].GetComponent<IPickable>().inInventory();
            currentSlot = -1;
        }
    }
    public void UseAndDestroyItem()
    {
        if (currentSlot != -1)
        {
            Destroy(slotItem[currentSlot]);
            slotItem[currentSlot] = null;
            PlayerPrefs.DeleteKey("Inventory " + currentSlot);
            slotImage[currentSlot].color = defaultColor;
            slotImage[currentSlot].sprite = startImage.sprite;
            currentSlot = -1;
        }
    }

    public void CleanItem(int slot)
    {
        slotItem[slot] = null;
        PlayerPrefs.DeleteKey("Inventory " + slot);
        slotImage[slot].color = defaultColor;
        slotImage[slot].sprite = startImage.sprite;
        currentSlot = -1;      
    }
    private void Update()
    {
        if (rotatingCamera)
        {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        cameraPlayer.rotation = Quaternion.Lerp(cameraPlayer.rotation, cameraPosition.rotation, Time.deltaTime * SpeedMoveCamera);
    }

    private void LoadInventory()
    {
        for (int i = 0; i < slotItem.Length; i++)
            {
                int id = PlayerPrefs.GetInt("Inventory " + i, -1);
            if (id != -1)
            {
                foreach (var itemAll in FindObjectsByType<IPickable>())
                {
                    if (itemAll.id == id)
                    {
                        GameObject item = itemAll.gameObject;
                        slotItem[i] = item;
                        item.transform.position = new Vector3(0, -10, 10);
                        break;
                    }
                }
            }
        }
    }
}
