using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class iconsbackgroudControler : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RectTransform CloseButton;

    public void Entericonsbackgroud()
    {
        canvasGroup.DOKill();
        //Debug.Log("Enter");
        canvasGroup.DOFade(1f, 0.3f);
    }

    public void Exiticonsbackgroud()
    {
        canvasGroup.DOKill();
        //Debug.Log("Exit");
        canvasGroup.DOFade(0f, 3f)
            .SetDelay(2f)
            .SetEase(Ease.InQuad);
    }
}
