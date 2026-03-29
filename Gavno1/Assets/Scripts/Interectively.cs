using UnityEngine;

public class Interectively : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private float _interactionDistance = 2f;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        foreach (var interactable in FindObjectsByType<IInteractable>())
        {
            interactable.HideButton();
        }
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _interactionDistance, _interactableLayerMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.ShowButton();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_mainCamera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * _interactionDistance);
        }
    }
}
