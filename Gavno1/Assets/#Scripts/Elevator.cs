using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Elevator : MonoBehaviour
{
    private Animator[] _Doors;

    public int _CurrentLevel = 0;
    [SerializeField] private float _TimeForLevel = 1;
    [SerializeField] private float _ShakeEffect = 1f;
    [SerializeField] private float _TimeForClosingDoors = 1;
    [SerializeField] private bool InWay = true;
    [SerializeField] private float _FloorHeight = 8.4f;

    [Header("Buttons")]
    [SerializeField] private Animator[] _Buttons;
    [SerializeField] private Animator[] _Texts;
    [SerializeField] private GameObject[] _Floors;
    [SerializeField] private int[] _Stages;
    [SerializeField] private Material _Active, _Inactive;


    public List<Transform> _objects = new List<Transform>();
    private float _TimeShake;
    private FloorPanel floorPanel;
    private void Awake()
    {
        _Doors = transform.gameObject.GetComponentsInChildren<Animator>();
        FindAnyObjectByType<ScenLoader>().SwapGameStage += UpdateButton;
        floorPanel = FindAnyObjectByType<FloorPanel>();

        _CurrentLevel = PlayerPrefs.GetInt("ElevatorFloor", -1);
        if (_CurrentLevel != -1) SpawnToFloor(_CurrentLevel);
        else
        {
            _CurrentLevel = 0;
            foreach (var level in _Floors)
            {
                level.SetActive(false);
            }
            _Floors[_CurrentLevel].SetActive(true);
        }

    }
    public void SpawnToFloor(int Level)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, Level * _FloorHeight, transform.localPosition.z);
        SwitchDoors(true);
        floorPanel.ChangeFloor(Level + 1);

        foreach (var level in _Floors)
        {
            level.SetActive(false);
        }
        _Floors[Level].SetActive(true);
    }
    public void ToLevel(int Level)
    {
        _Buttons[Level].SetTrigger("Pressed");
        _Texts[Level].SetTrigger("Pressed");
        if (Level == _CurrentLevel) 
        { 
            SwitchDoors(true);
            StopAllCoroutines();
            floorPanel.ChangeFloor(Level+1);
            return;
        }
        
        StopAllCoroutines();
        if (PlayerPrefs.GetInt("GameStage") < _Stages[Level]) return;

        _TimeShake = _TimeForLevel * Mathf.Abs(Level - _CurrentLevel);

        StartCoroutine(ShakeElevator(Level));
        SwitchDoors(false);


    }


    private IEnumerator ShakeElevator(int Level)
    {
        if (!InWay) yield return new WaitForSeconds(_TimeForClosingDoors);
        InWay = true;
        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < _TimeShake)
        {
            float x = Random.Range(-1f, 1f) * _ShakeEffect;
            float y = Random.Range(-1f, 1f) * _ShakeEffect;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        SwitchDoors(true);
        _CurrentLevel = Level;
        InWay = false;

        foreach (var level in _Floors)
        {
            level.SetActive(false);
        }
        _Floors[Level].SetActive(true);
        ChangeLevel(Level);
        floorPanel.ChangeFloor(Level+1);
    }

    private void UpdateButton()
    {
        for (int i = 0; i < _Buttons.Length; i++)
        {
            if (PlayerPrefs.GetInt("GameStage", 0) >= _Stages[i]) _Buttons[i].GetComponent<Renderer>().material = _Active;
            else _Buttons[i].GetComponent<Renderer>().material = _Inactive;
        }
    }
    private void SwitchDoors(bool state)
    {
        foreach (Animator a in _Doors)
        {
            if (a.CompareTag("Door"))a.SetBool("IsOpen", state);
        }
    }

    private void ChangeLevel(int floor)
    {
        foreach (Transform t in _objects)
        {
            t.position = new Vector3(t.position.x, floor * _FloorHeight, t.position.z);
            Debug.Log("Moving " + t.name + " to floor " + floor);
        }
        transform.localPosition = new Vector3(transform.localPosition.x, floor * _FloorHeight, transform.localPosition.z);
    }
}
