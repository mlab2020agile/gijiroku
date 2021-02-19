using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PopupPanelScript : MonobitEngine.MonoBehaviour, IDragHandler
{
    [SerializeField]
    GameObject LogText;
    [SerializeField]
    GameObject popup_Panel;
    [SerializeField]
    GameObject canvas;
    RectTransform PanelRectTransform;
    public RectTransform m_rectTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        //Clear();
        PanelRectTransform = this.GetComponent<RectTransform>();
    }

    void Clear()
    {
        LogText.GetComponent<Text>().text = "";
    }
    
    /// <summary>
    /// テキストの位置取得
    /// </summary>
    private void Reset()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }


    //ドラッグ中に呼び出されるメソッド
    public void OnDrag(PointerEventData e)
    {
        //ポジションを動かす
        m_rectTransform.position += new Vector3(e.delta.x, e.delta.y, 0f);
    }

    void Update()
    {
        //パネルが画面外に出ないようにする処理
        if(PanelRectTransform.localPosition.x < -470f)
        {
            PanelRectTransform.localPosition = new Vector3(-470f, PanelRectTransform.localPosition.y, PanelRectTransform.localPosition.z);
        }
        if(PanelRectTransform.localPosition.y < -110f)
        {
            PanelRectTransform.localPosition = new Vector3(PanelRectTransform.localPosition.x, -110f, PanelRectTransform.localPosition.z);
        }
        if(PanelRectTransform.localPosition.x > 470f)
        {
            PanelRectTransform.localPosition = new Vector3(470f, PanelRectTransform.localPosition.y, PanelRectTransform.localPosition.z);
        }
        if(PanelRectTransform.localPosition.y > 110f)
        {
            PanelRectTransform.localPosition = new Vector3(PanelRectTransform.localPosition.x, 110f, PanelRectTransform.localPosition.z);
        }
    }
}
