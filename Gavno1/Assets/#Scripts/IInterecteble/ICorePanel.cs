using UnityEngine;
public class ICorePanel : ICore
{
    private Transform cameraPlayer;
    private bool movingCamera = false;
    private GameObject[] UIPlayer;

    private PlayerMove playerMove;
    private Rotation rotation;

    [HideInInspector] public bool isFocused = false;    

    [SerializeField] private Transform camPos;
    [SerializeField] private float speedMoveCamera = 2;

    [SerializeField] CanvasGroup panel;

    public GameObject[] objectsOnFocused;

    [SerializeField]private GameObject player;
    override public void Start()
    {
        base.Start();
        cameraPlayer = Camera.main.transform;
        UIPlayer = FindAnyObjectByType<PlayerMove>().UIPlayer;
        playerMove = FindAnyObjectByType<PlayerMove>();
        rotation = FindAnyObjectByType<Rotation>();
        panel.interactable = false;
    }
    override public void PressButton()
    {
        if (playerMove._InInterface) return;
        movingCamera = true;
        playerMove._InInterface = true;
        foreach (GameObject obj in objectsOnFocused) obj.SetActive(true); 
        foreach (GameObject obj in UIPlayer) obj.SetActive(false);
        rotation.enabled = false;
        isFocused = true;
        panel.interactable = true;
        cameraPlayer.parent = playerMove.transform;

        foreach(Transform t in player.GetComponentsInChildren<Transform>()) t.gameObject.layer = LayerMask.NameToLayer("NoCamera");
        player.layer = LayerMask.NameToLayer("NoCamera");
    }
    public void BackCamera()
    {
        foreach (GameObject obj in objectsOnFocused) obj.SetActive(false); 
        foreach (GameObject obj in UIPlayer) obj.SetActive(true);
        isFocused = false;
        panel.interactable = false;
        movingCamera = false;
    }
    virtual public void FixedUpdate()
    {
        if (movingCamera) MoveCamera(); 
        if (movingCamera && (cameraPlayer.position - camPos.position).sqrMagnitude <= 0.02 * 0.02 && Quaternion.Dot(cameraPlayer.rotation, camPos.rotation) >= 0.95)
        {
            cameraPlayer.position = camPos.position;
            movingCamera = false;
        }
    }
    private void MoveCamera()
    {
        cameraPlayer.position = Vector3.Lerp(cameraPlayer.position, camPos.position, Time.deltaTime * speedMoveCamera);
        cameraPlayer.rotation = Quaternion.Lerp(cameraPlayer.rotation, camPos.rotation, Time.deltaTime * speedMoveCamera);
    }
}

