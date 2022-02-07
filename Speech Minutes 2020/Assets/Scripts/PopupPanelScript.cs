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
    public float rate;

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
        //画面比率によって判定を分けている
        rate = 1.0f*Screen.width/Screen.height;
        if (1.7f<rate)
        {
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
        else if ((1.5f<rate) && (rate<= 1.7f))
        {
            if(PanelRectTransform.localPosition.x < -470f)
            {
                PanelRectTransform.localPosition = new Vector3(-470f, PanelRectTransform.localPosition.y, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.y < -150f)
            {
                PanelRectTransform.localPosition = new Vector3(PanelRectTransform.localPosition.x, -150f, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.x > 470f)
            {
                PanelRectTransform.localPosition = new Vector3(470f, PanelRectTransform.localPosition.y, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.y > 150f)
            {
                PanelRectTransform.localPosition = new Vector3(PanelRectTransform.localPosition.x, 150f, PanelRectTransform.localPosition.z);
            }
        }
        else if ((1.3f<rate) && (rate<= 1.5f))
        {
            if(PanelRectTransform.localPosition.x < -470f)
            {
                PanelRectTransform.localPosition = new Vector3(-470f, PanelRectTransform.localPosition.y, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.y < -230f)
            {
                PanelRectTransform.localPosition = new Vector3(PanelRectTransform.localPosition.x, -230f, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.x > 470f)
            {
                PanelRectTransform.localPosition = new Vector3(470f, PanelRectTransform.localPosition.y, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.y > 230f)
            {
                PanelRectTransform.localPosition = new Vector3(PanelRectTransform.localPosition.x, 230f, PanelRectTransform.localPosition.z);
            }
            
        }
         else
        {
            if(PanelRectTransform.localPosition.x < -470f)
            {
                PanelRectTransform.localPosition = new Vector3(-470f, PanelRectTransform.localPosition.y, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.y < -260f)
            {
                PanelRectTransform.localPosition = new Vector3(PanelRectTransform.localPosition.x, -260f, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.x > 470f)
            {
                PanelRectTransform.localPosition = new Vector3(470f, PanelRectTransform.localPosition.y, PanelRectTransform.localPosition.z);
            }
            if(PanelRectTransform.localPosition.y > 260f)
            {
                PanelRectTransform.localPosition = new Vector3(PanelRectTransform.localPosition.x, 260f, PanelRectTransform.localPosition.z);
            }
            
        }
    }
}
