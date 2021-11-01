using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecButtonChange : MonoBehaviour
{
    public GameObject RecStartButton;
    public GameObject RecStopButton;
    // Start is called before the first frame update
    void Start()
    {
        RecStartButton.SetActive(true);
        RecStopButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRecButtonOnClick()
    {
            RecStartButton.SetActive(false);
            RecStopButton.SetActive(true);
    }

    public void StopRecButtonOnClick()
    {
            RecStopButton.SetActive(false);
            RecStartButton.SetActive(true);
    }

}
