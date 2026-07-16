using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Monitor : ICorePanel
{
    [SerializeField]private RawImage startScreen;
    private VideoPlayer videoPlayer;

    private bool isOn = false;
    private bool isStartScreen = false;
    [SerializeField] private float timeStartScreen = 4;
    [SerializeField] private float speedVisibleScreen = 2;

    [SerializeField] private GameObject desktop;
    override public void Start()
    {
        base.Start();
        videoPlayer = GetComponentInChildren<VideoPlayer>();
    }
    public void TurningOnOff(bool state)
    {
        isOn = state;
        startScreen.gameObject.SetActive(isOn);
        if (isOn)
        {
            isStartScreen = isOn;
            videoPlayer.Play();
            StartCoroutine(TurnOnOff());
        }
    }

    override public void FixedUpdate()
    {
        base.FixedUpdate();
        if (isOn && isStartScreen) startScreen.color = Color.Lerp(startScreen.color, new Color(startScreen.color.r, startScreen.color.g, startScreen.color.b, 1), Time.deltaTime * speedVisibleScreen);
    }

    private IEnumerator TurnOnOff()
    {
        yield return new WaitForSeconds(timeStartScreen);
        isStartScreen = false;
        startScreen.gameObject.SetActive(false);
        desktop.SetActive(true);
    }

}
