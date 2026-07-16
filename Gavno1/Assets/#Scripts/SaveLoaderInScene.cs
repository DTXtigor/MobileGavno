using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoaderInScene : MonoBehaviour
{
    private Elevator elevator;
    private PlayerMove playerMove;
    private DoorOpener[] doors;
    private CheckStatueDoor[] statueDoors;
    private EPanel[] swithers;
    private StaffOnly staffOnly;
    private Folder folder;

    public int SecondsToAutosave = 30;

    [SerializeField] private bool autoSave = true;  
    [SerializeField] private bool resetToStart = false;

    private void Awake()
    {
        elevator = FindAnyObjectByType<Elevator>();
        playerMove = FindAnyObjectByType<PlayerMove>();
        doors = FindObjectsByType<DoorOpener>();
        statueDoors = FindObjectsByType<CheckStatueDoor>();
        swithers = FindObjectsByType<EPanel>();
        staffOnly = FindAnyObjectByType<StaffOnly>();
        folder = FindAnyObjectByType<Folder>();

        if (resetToStart) PlayerPrefs.DeleteAll();

        StartCoroutine(Autosave());

        Time.timeScale = 1;
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        FindAnyObjectByType<ScenLoader>().SaveSettings();
        SceneManager.LoadScene(1);
    }
    public void SaveAll()
    {
        PlayerPrefs.SetInt("ElevatorFloor", elevator._CurrentLevel);
        PlayerPrefs.SetFloat("PlayerX", playerMove.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", playerMove.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", playerMove.transform.position.z);
        PlayerPrefs.SetInt("isActiveShadowRoom", BoolToInt(staffOnly.isActiveShadowRoom));
        PlayerPrefs.SetInt("IsWin", BoolToInt(folder.win));

        foreach (DoorOpener door in doors) PlayerPrefs.SetInt("IsOpenDoor" + door.id, BoolToInt(door.IsOpen));
        foreach (DoorOpener door in doors) PlayerPrefs.SetInt("IsLockedDoor" + door.id, BoolToInt(door.IsLocked));

        foreach (CheckStatueDoor door in statueDoors) PlayerPrefs.SetInt("IsFinishedDoor" + door.id, BoolToInt(door.IsFinished));

        foreach (EPanel swither in swithers) PlayerPrefs.SetInt("StateSwitchers" + swither.id, BoolToInt(swither._isOn));

        PlayerPrefs.Save();
    }

    private IEnumerator Autosave()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(SecondsToAutosave);
            if (autoSave) SaveAll();
        }
    }

    private int BoolToInt(bool b)
    {
        if (b) return 1;
        else return 0;
    }
}
