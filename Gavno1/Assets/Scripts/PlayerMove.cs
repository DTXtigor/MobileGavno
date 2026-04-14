using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Transform _camera;
    private JoystickMovment _joystickMovment;

    // UI
    public GameObject[] UIPlayer;
    [SerializeField] private Transform CamPos;
    private bool MovingCam;

    [SerializeField] private float _speedMove;

    private Vector3 _targetVelocity;

    [SerializeField] private float _sensivity;
    [HideInInspector] public bool _InInterface = false;
    [SerializeField] private float forceMultiplier = 5f;    

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _joystickMovment = FindAnyObjectByType<JoystickMovment>();
    }

    void Update()
    {
        if (!_InInterface) Move();
        if (MovingCam)
        {
            _camera.position = Vector3.Lerp(_camera.position, CamPos.position, Time.deltaTime * 2);
            _camera.rotation = Quaternion.Lerp(_camera.rotation, CamPos.rotation, Time.deltaTime * 3);
            if (Vector3.Distance(_camera.position, CamPos.position) < 0.1f)
            {
                MovingCam = false;
                _InInterface = false;
            }
        }
    }

    private void Move()
    {
        if (_characterController.enabled)
        _targetVelocity = new Vector3(_joystickMovment._InputVector.x, _joystickMovment._InputVector.y, 0);
        _targetVelocity = transform.right * _targetVelocity.x + transform.forward * _targetVelocity.y;
    
        _characterController.Move(_targetVelocity * _speedMove * Time.deltaTime);
    }

    public void BackToBody()
    {
        MovingCam = true;
        foreach (var item in UIPlayer)
        {
            item.SetActive(true);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic) return;

        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.linearVelocity = pushDir * forceMultiplier;
    }
}
