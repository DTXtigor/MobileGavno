using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class StaffOnly : MonoBehaviour
{
    const int PassCount = 8;
    [SerializeField] private bool[] Pass = new bool[PassCount];
    [SerializeField] private EPanel[] Switchers;

    [SerializeField] private Transform Pos;
    private Vector3 Started;
    private bool Teleported = false;
    private bool Passed = false;

    [Header("Completed")]
    public bool completed = false;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject shadowCouple;
    [SerializeField] private Material shadowMaterial;


    public GameObject shadowRoom;
    public bool isActiveShadowRoom;
    private void Start()
    {
        Started = transform.position;
        shadowRoom = GameObject.Find("ShadowRoom"); 
        bool state = intToBool(PlayerPrefs.GetInt("isActiveShadowRoom", 0));
        shadowRoom.SetActive(state);
        isActiveShadowRoom = state;
    }

    private bool intToBool(int value)
    {
        if (value == 0) return false;
        return true;
    }

    private int boolToInt(bool value)
    {
        if (value) return 1;
        return 0;
    }
    private void Teleport()
    {
        FindAnyObjectByType<PlayerMove>().GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(BackRb());
        if (Passed && !Teleported && !completed)
        {
            FindAnyObjectByType<PlayerMove>().transform.position += Pos.position - transform.position;
            transform.position = Pos.position;
            Teleported = true;
            shadowRoom.SetActive(true);
            isActiveShadowRoom = true;
        }
        if (!Passed && Teleported && !completed)
        {
            transform.position = Started;
            FindAnyObjectByType<PlayerMove>().transform.position -= Pos.position - transform.position;
            Teleported = false;
            shadowRoom.SetActive(false);
            isActiveShadowRoom = false;
        }
    }
    private void Check()
    {
        for (int i = 0; i < PassCount; i++)
        {
            if (Switchers[i]._isOn != Pass[i]){ Passed = false; return; }
        }
        Passed = true;
    }
    public void CheckPass()
    {
        Check();
        Teleport();
    }

    IEnumerator BackRb()
    {
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<PlayerMove>().GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Return()
    {
        completed = true;
        transform.position = Started;
        backWall.GetComponent<MeshRenderer>().material = shadowMaterial;
        shadowCouple.SetActive(false);
    }
}
