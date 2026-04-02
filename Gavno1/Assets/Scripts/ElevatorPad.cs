using UnityEngine;
public class ElevatorPad : IInteractable
{
    private PlayerMove _player;
    private Transform _Camera;

    [SerializeField] private Transform _CamPos;

    private bool MovingCam;

    [SerializeField] private float SpeedMoveCamera;

    override public void PressButton()
    {
        SwitchAllOther(false);
        _player._InInterface = true;
        MovingCam = true;
    }

    private void Update()
    {
        if (MovingCam)
        {
            MoveCamera();
            if (Vector3.Distance(_Camera.position, _CamPos.position) < 0.1f)
            {
                MovingCam = false;
                SwitchAllNeeded(true);
            }
        }
    }
    private void Start()
    {
        _Camera = Camera.main.transform;
        _player = FindAnyObjectByType<PlayerMove>();
    }

    private void MoveCamera()
    {
        _Camera.position = Vector3.Lerp(_Camera.position, _CamPos.position, Time.deltaTime * SpeedMoveCamera);
        _Camera.rotation = Quaternion.Lerp(_Camera.rotation, _CamPos.rotation, Time.deltaTime * SpeedMoveCamera);
    }
}