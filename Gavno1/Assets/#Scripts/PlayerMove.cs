using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    private Transform _camera;
    private JoystickMovment _joystickMovment;
    private Animator _animator;
    private PlayerPick playerPick;

    // UI
    public GameObject[] UIPlayer;
    [SerializeField] private Transform CamPos;
    private bool MovingCam;

    [SerializeField] private float speed;
    [SerializeField] private float speedStoping;

    [SerializeField] private float _sensivity;
    public bool _InInterface = false;
    [SerializeField] private float forceMultiplier = 5f;

    private Transform parentCamera;

    private Rotation rotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _camera = Camera.main.transform;
        _joystickMovment = FindAnyObjectByType<JoystickMovment>();
        _animator = GetComponentInChildren<Animator>();
        playerPick = GetComponent<PlayerPick>();
        parentCamera = _camera.parent;
        rotation = FindAnyObjectByType<Rotation>();

        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX", 0), PlayerPrefs.GetFloat("PlayerY", 0), PlayerPrefs.GetFloat("PlayerZ", 0));
    }

    void FixedUpdate()
    {
        if (!_InInterface) Move();
        else _animator.SetBool("IsMoving", false);
        if (MovingCam)
        {
            _camera.position = Vector3.Lerp(_camera.position, CamPos.position, Time.deltaTime * 2); 
            _camera.rotation = Quaternion.Lerp(_camera.rotation, CamPos.rotation, Time.deltaTime * 3);
            if ((_camera.position - CamPos.position).sqrMagnitude <= 0.02 * 0.02 && Quaternion.Dot(CamPos.rotation, _camera.rotation) > 0.95)
            {
                MovingCam = false;
                _camera.position = CamPos.position;
                foreach (Transform t in GetComponentsInChildren<Transform>()) { t.gameObject.layer = gameObject.layer; }
            }
            else if ((_camera.position - CamPos.position).sqrMagnitude <= 0.1 * 0.1 && Quaternion.Dot(CamPos.rotation, _camera.rotation) > 0.8)
            {
                _InInterface = false;
                rotation.enabled = true;
                _camera.SetParent(CamPos);
            }
        }
    }

    private void Move()
    {
        playerPick.rotatingCamera = false;
        Vector3 input = _joystickMovment._InputVector;
        input = (transform.right * input.x + transform.forward * input.y) * speed * Time.deltaTime;
        if (input != Vector3.zero) rb.linearVelocity = new Vector3(input.x, rb.linearVelocity.y, input.z);
        else rb.linearVelocity = new Vector3(Mathf.Lerp(rb.linearVelocity.x, 0, Time.deltaTime * speedStoping), rb.linearVelocity.y, Mathf.Lerp(rb.linearVelocity.z, 0, Time.deltaTime * speedStoping));

        if (input == Vector3.zero) _animator.SetBool("IsMoving", false);
        else _animator.SetBool("IsMoving", true);
        _animator.SetFloat("x", _joystickMovment._InputVector.x);
        _animator.SetFloat("y", _joystickMovment._InputVector.y);
    }

    public void BackToBody()
    {
        MovingCam = true;

        IPlayer player = GetComponent<IPlayer>();
        if (player.Interect && player.Interect.GetComponent<ICorePanel>()) player.Interect.GetComponent<ICorePanel>().BackCamera();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        playerPick.rotatingCamera = false;
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic) return;

        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.linearVelocity = pushDir * forceMultiplier;
    }
}
