using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CheckStatueDoor : MonoBehaviour
{
    [SerializeField] private float TimeQueue = 1f;
    public int id;

    private bool destroys = false;
    private bool queuerController = false;
    private int queue = -1;
    private Statues[] statues;
    private ShakeCamera shakeCamera;
    public bool isMovingStatues = false;

    [SerializeField] private MeshDestroy[] meshDestroys;

    [SerializeField] private int maxQueue = 3;

    public bool IsFinished = false;

    private void Start()
    {
        shakeCamera = FindAnyObjectByType<ShakeCamera>();
        statues = transform.parent.GetComponentsInChildren<Statues>();

        IsFinished = intToBool(PlayerPrefs.GetInt("IsFinishedDoor" + id, 0));
        if (IsFinished)
        {
            foreach (var statue in meshDestroys) {Destroy(statue); }
        }
    }

    private bool intToBool(int value)
    {
        if (value == 0) return false;
        return true;
    }

    private void Update()
    {
        if (!destroys || !queuerController) return;


        foreach (MeshDestroy part in meshDestroys)
        {
            if (part.queueToDestroy == queue)
            {
                part.Break();
            }
            queuerController = false;
            StartCoroutine(Queue());
        }
        queue++;
        shakeCamera.TriggerShake(0.3f, 0.6f);
        if (queue == maxQueue) { destroys = false; IsFinished = true; }
    }
    public void Checking()
    {
        foreach(Statues statue in statues)
        {
            if (statue.currentPlace != statue.targetPlace && statue.targetPlace != -1) return;
        }
        destroys = true;
        queuerController = true;
    }

    IEnumerator Queue()
    {
        yield return new WaitForSeconds(TimeQueue);
        queuerController = true;
    }
}
