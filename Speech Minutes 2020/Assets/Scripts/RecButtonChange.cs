using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RecButtonChange : MonoBehaviour
{
    public GameObject RecStartButton;
    public GameObject RecStopButton;
    public Image RecStopImage;
    public Image RecStartImage;
    
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
         Transform t = RecStartButton.GetComponent<Transform>();
        t.DOScale(new Vector3(2f, 2f, 2f), 0.3f);
        RecStartImage.DOFade(0f, 0.4f);
        t.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0f)
        .SetDelay(0.3f)
        .OnComplete(() => RecStartButton.SetActive(false));
        RecStopButton.SetActive(true);

        RecStopImage.DOFade(0.55f, 0.5f)
        .SetLoops(10, LoopType.Yoyo)
        .SetDelay(0.6f)
        .OnComplete(() => RecStopImage.DOFade(1.0f, 2f));
    }

    public void StopRecButtonOnClick()
    {
        RecStopButton.SetActive(false);
        RecStartButton.SetActive(true);
        RecStartImage.DOFade(1f, 0.5f)
        .SetDelay(0.2f);
    }
  
}
