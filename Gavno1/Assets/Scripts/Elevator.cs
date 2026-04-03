using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Elevator : MonoBehaviour
{
    private Animator[] _Doors;

    [SerializeField] private int _CurrentLevel = 0;
    [SerializeField] private float _TimeForLevel = 1;
    [SerializeField] private float _ShakeEffect = 1f;
    [SerializeField] private float _TimeForClosingDoors = 1;
    [SerializeField] private bool InWay = true;
    [SerializeField] private float _FloorHeight = 8.4f;
    public List<Transform> _objects = new List<Transform>();
    private float _TimeShake;
    private void Start()
    {
        _Doors = transform.gameObject.GetComponentsInChildren<Animator>();
    }

    public void ToLevel(int Level)
    {
        Debug.Log("To level " + Level);
        if (Level == _CurrentLevel) { SwitchDoors(true); return; }
        StopAllCoroutines();

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
        ChangeLevel(Level);
    }

    private void SwitchDoors(bool state)
    {
        foreach (Animator a in _Doors)
        {
            a.SetBool("IsOpen", state);
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
