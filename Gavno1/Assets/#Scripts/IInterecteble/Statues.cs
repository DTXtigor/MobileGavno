using Unity.VisualScripting;
using UnityEngine;

public class Statues : ICore
{
    [SerializeField] private float heightToUp = 2.0f;
    [SerializeField] private float speedToUp = 2.0f;
    [SerializeField] private float speedToRotate = 2.0f;
    [SerializeField] private float magnitudeShake = 1f;
    public int targetPlace = 0;
    public int currentPlace = 0;
    
    [HideInInspector] public bool isUp = false;
    private bool IsMoving = false;
    private bool IsRotating = false;
    private GameObject anchor;
    private Statues otherStatue;
    private CheckStatueDoor checkStatueDoor;
    private ShakeCamera shakeCamera;

    private Vector3 startPos;
    private Transform StartParent;

    private float targetY;

    override public void Start()
    {
        base.Start();
        startPos = transform.position;
        StartParent = transform.parent;
        checkStatueDoor = transform.parent.GetComponentInChildren<CheckStatueDoor>();
        shakeCamera = FindAnyObjectByType<ShakeCamera>();
    }
    public override void PressButton()
    {
        if (checkStatueDoor.isMovingStatues) return;
        IsMoving = true;
        isUp = !isUp;
    }

    private void FixedUpdate()
    {
        if (isUp && IsMoving)
        {
            transform.position = Vector3.Lerp(transform.position, startPos + Vector3.up * heightToUp, speedToUp);
            if ((transform.position - (startPos + Vector3.up * heightToUp)).sqrMagnitude < 0.1 * 0.1)
            {
                IsMoving = false;
                StartToChangePosition();
            }
        }
        else if (!isUp && IsMoving)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, speedToUp);
            if ((transform.position - startPos).sqrMagnitude < 0.1 * 0.1) IsMoving = false; 
        }

        if (IsRotating)
        {
            anchor.transform.rotation = Quaternion.Lerp(anchor.transform.rotation, Quaternion.Euler(0, 180, 0), speedToRotate * Time.deltaTime);
            otherStatue.transform.position = Vector3.Lerp(otherStatue.transform.position, new Vector3(otherStatue.transform.position.x, otherStatue.targetY, otherStatue.transform.position.z), speedToRotate * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), speedToRotate * Time.deltaTime);

            float asa = Mathf.Sin(Mathf.Abs(anchor.transform.rotation.y));
            if (asa>0.5) asa -= (asa - 0.5f) * 2;
            shakeCamera.TriggerShackeTick(asa * magnitudeShake);
            if (Quaternion.Angle(anchor.transform.rotation, Quaternion.Euler(0, 180, 0)) < 1)
            {
                IsRotating = false;

                otherStatue.transform.SetParent(StartParent);
                otherStatue.IsMoving = true;
                otherStatue.isUp = false;

                transform.SetParent(StartParent);
                IsMoving = true;
                isUp = false;

                Vector3 a = otherStatue.startPos;
                otherStatue.startPos = startPos;
                startPos = a;

                int p = otherStatue.currentPlace;
                otherStatue.currentPlace = currentPlace;
                currentPlace = p;

                checkStatueDoor.Checking();

                Destroy(anchor);

                checkStatueDoor.isMovingStatues = false;
            }
        }
    }

    private void StartToChangePosition()
    {
        foreach (Statues statue in FindObjectsByType<Statues>())
        {
            if (statue.isUp && statue != this)
            {
                anchor = Instantiate(new GameObject(), (statue.transform.position + transform.position) / 2, Quaternion.identity);
                statue.transform.SetParent(anchor.transform);
                transform.SetParent(anchor.transform);
                IsRotating = true;
                otherStatue = statue;

                targetY = statue.startPos.y + heightToUp;
                statue.targetY = startPos.y + statue.heightToUp;

                checkStatueDoor.isMovingStatues = true;
                return;
            }
        }
    }
}
