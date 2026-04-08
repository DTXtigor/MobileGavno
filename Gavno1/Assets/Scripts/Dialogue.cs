using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dialogue : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float TextSpeed = 0.05f;
    [SerializeField] private float TimeBeginNewLine = 3f;

    [HideInInspector] public string[] _currentLanguage;

    private bool _isPrinting = false;
    private bool _started = false;
    private int _index = 0;
    private TMP_Text Text;
    private bool _skipped = false;
    private bool Onces = true;

    private void Start()
    {
        Text = GetComponentInChildren<TMP_Text>();
        Text.text = string.Empty;
    }
    public void StartDialogue()
    {
        _started = true;
        _index = 0;
        StartCoroutine(PrintString());

    }

    private void Update()
    {
        if (!_started) return;

        if (_isPrinting && _skipped)
        {
            StopAllCoroutines();
            Text.text = _currentLanguage[_index];
            _index++;
            _isPrinting = false;
            _skipped = false;
        }
        if (!_isPrinting && Onces)
        {
            if (!Onces) return;
            Onces = false;
            StartCoroutine(StartNewLine());
        }
        if (!_isPrinting && _skipped)
        {
            StopAllCoroutines();
            StartCoroutine(PrintString());
            _skipped = false;
        }
    }
    IEnumerator StartNewLine()
    {
        yield return new WaitForSeconds(TimeBeginNewLine);
        StartCoroutine(PrintString());
    }
    IEnumerator PrintString()
    {

        if (_index == _currentLanguage.Length)
        {
            _started = false;
            Text.text = string.Empty;
            yield return null;
        }
        if (_started)
        {
            Onces = true;
            _isPrinting = true;
            Text.text = string.Empty;
            foreach (char c in _currentLanguage[_index].ToCharArray())
            {
                Text.text += c;
                yield return new WaitForSeconds(TextSpeed);
            }
            _isPrinting = false;
            _index++;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _skipped = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _skipped = false;
    }
}
