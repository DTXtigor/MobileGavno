using UnityEngine;
public class ElevatorPad : IInteractable
{
    [SerializeField] private Transform _CamPos;
    [SerializeField] private Rotation _Rotation;

    private Transform _Camera;

    private Transform playerCamPos;
    private Transform playerCamRot;

    [SerializeField] private float SpeedMoveCamera;
    [SerializeField] private float smoothTime = 0.3f;

    private bool isMovingCamera = false;
    override public void PressButton()
    {
        SwitchAllOther(false);
        isMovingCamera = true;
        _Camera.SetParent(null);
        _Rotation.CanMoveCamera = false;
    }

    private void Update()
    {
        if (isMovingCamera)
        {
            MoveCamera();

            if (Vector3.Distance(_Camera.position, _CamPos.position) < 0.01f)
            {
                isMovingCamera = false;
                IsActive = true;
            }
        }
    }
    private void Start()
    {
        _Camera = Camera.main.transform;
    }

    private void MoveCamera()
    {
        _Camera.position = Vector3.Lerp(_Camera.position, _CamPos.position, Time.deltaTime * SpeedMoveCamera);
        _Camera.rotation = Quaternion.Lerp(_Camera.rotation, _CamPos.rotation, Time.deltaTime * SpeedMoveCamera);
    }
}