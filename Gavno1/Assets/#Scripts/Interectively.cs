using UnityEngine;

public class IPlayer : MonoBehaviour
{
    private Camera cameraPlayer;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private float interactionDistance = 2f;

    [HideInInspector] public ICore Interect;
    public GameObject MainInteractButton;

    private void Start()
    {
        cameraPlayer = Camera.main;
        MainInteractButton.SetActive(false);
    }

    void FixedUpdate()
    {
        Interect = null;
        MainInteractButton.SetActive(false);
        foreach (ICore interactable in FindObjectsByType<ICore>())
        {
            interactable.IsActive = false;
        }
        Ray ray = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayerMask))
        {
            ICore interactable = hit.collider.GetComponent<ICore>();
            if (interactable == null) interactable = hit.collider.GetComponentInParent<ICore>();
            if (interactable != null && interactable.CheckingState())
            {
                Interect = interactable;
                interactable.IsActive = true;
                if (!interactable.GetComponent<ICorePanel>() || (interactable.GetComponent<ICorePanel>() && !interactable.GetComponent<ICorePanel>().isFocused)) MainInteractButton.SetActive(true);
                MainInteractButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = interactable.ButtonText;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (cameraPlayer != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(cameraPlayer.transform.position, cameraPlayer.transform.forward * interactionDistance);
        }
    }

    public void PressButton()
    {
        if (Interect != null) { Interect.PressButton(); }          
    }
}
