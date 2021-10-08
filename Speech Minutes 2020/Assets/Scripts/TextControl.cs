using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;


public class TextControl : MonobitEngine.MonoBehaviour, IDragHandler
{
    // マウススクロール変数
    private float scroll;
    public Text chatComent;
    public bool Selectflag = false;
    // public Color texcolor;
    public GameObject teO;
    public GameObject PenButton;
    public GameObject FinishButton;
    public GameObject EnlargeButton;
    public GameObject ShrinkButton;
    GameObject EdittingTextPanel;
    InputField EdittingTextField;
    GameObject TextCloseButton;

    void Start()
    {
        EdittingTextPanel = GameObject.Find("Canvas").transform.Find("EdittingTextPanel").gameObject;
        TextCloseButton = GameObject.Find("Canvas").transform.Find("EdittingTextPanel").Find("TextCloseButton").gameObject;
        EdittingTextField = GameObject.Find("Canvas").transform.Find("EdittingTextPanel").Find("EdittingTextField").GetComponent<InputField>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        // Textコンポーネントを取得
        Text text = this.GetComponentInChildren<Text>();
        Debug.Log(text.text);
        // 色を指定
        text.color = Color.black;
        EdittingTextPanel.SetActive(false);
        string text_ = this.GetComponentInChildren<Text>().text;
        monobitView.RPC("RecvChattext", MonobitTargets.OthersBuffered, text_);
    }
    [MunRPC]
    public void RecvChattext(string text_)
    {
        Text text = this.GetComponentInChildren<Text>();
        text.text = text_;
        Debug.Log("receiveChattext");
    }
    /// <summary>
    /// テキストコメントを選択するための関数
    /// </summary>
    public void Selecter()
    {
        /*  if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked)
               {
                   Debug.Log("lockされてない");
               Selectflag = true;
               // OnClick();  //クリックされた時の処理
           }*/
        Selectflag = true;
        if (Selectflag == true)
        {
            // Textコンポーネントを取得
            chatComent = this.GetComponentInChildren<Text>();
            // 色を指定
            chatComent.color = Color.white;
            Debug.Log("Selectされました");
            scroll = Input.GetAxis("Mouse ScrollWheel");
            /*  if (scroll > 0)
              {
                  text.fontSize += 14;
                  Debug.Log("回ったよ");
              }*/



        }
        /* EventSystem ev = EventSystem.current;
         if (ev.alreadySelecting)
         {
             Debug.Log("何かを選択しています");
         }*/
    }
    /// <summary>
    /// テキストのフォントサイズ変更及び削除
    /// </summary>
    void Update()
    {
        /* if (GameObject.Find("TextBox").GetComponent<InputField>().text != "")
         {
             Selectflag == false;
         }*/
        //Start();
        if (Selectflag == true)
        {
            if (!monobitView.isMine) { return; }
            scroll = Input.GetAxis("Mouse ScrollWheel");
            Text textfont = this.GetComponentInChildren<Text>();

            if (scroll > 0)
            {
                textfont.fontSize += 1;// (int)scroll*100;
                Debug.Log("大よ" + scroll);
            }
            else if (scroll < 0)
            {
                if (textfont.fontSize >= 32)
                {
                    textfont.fontSize -= 1;// (int)scroll*100;
                    Debug.Log("小よ" + scroll);
                }
                else { textfont.fontSize = 32; }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Selectflag = false;
                if (Selectflag == false)
                {
                    // Textコンポーネントを取得
                    Text text = this.GetComponentInChildren<Text>();
                    // 色を指定
                    text.color = Color.black;

                    Debug.Log("falseですよ");
                }

            }

        }
        if (Selectflag == true && Input.GetKey(KeyCode.Backspace))
        {
            //Destroy(this.gameObject);
            OnDestroy();
            Selectflag = false;
            Debug.Log("false&destroy");
        }

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed secondary button.１");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.ｑ２");
    }
   
    public RectTransform m_rectTransform = null;

    /// <summary>
    /// テキストの位置取得
    /// </summary>
    private void Reset()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }
    /// <summary>
    /// テキストの位置操作
    /// </summary>
    /// <param name="e"></param>
    public void OnDrag(PointerEventData e)
    {
        if (!monobitView.isMine) { return; }
        m_rectTransform.position += new Vector3(e.delta.x, e.delta.y, 0f);
    }

    /*//テキストボックスの削除(バックスペースで削除)
    public void Destroy()
    {
        if (Input.GetKey(KeyCode.Backspace)) {
            Destroy(this.gameObject);
            Debug.Log("ok");
        }
    }*/
    public void PenButtonOnclick()
    {

        //inputFieldのTextコンポーネントを取得
        EdittingTextField = EdittingTextField.GetComponent<InputField>();
        // Textコンポーネントを取得
        Text text = this.GetComponentInChildren<Text>();
        EdittingTextField.text = text.text;
        Selectflag = false;
        EdittingTextPanel.SetActive(true);
        //Debug.Log("Pressed PenButton");
    }
    public void FinishButtonOnclick()
    {
        if (EdittingTextPanel.activeSelf == true)
        {
            // Textコンポーネントを取得
            Text text = GetComponentInChildren<Text>();
            text.text = EdittingTextField.text;
            string text_ = text.text;
            monobitView.RPC("RecvChattext", MonobitTargets.OthersBuffered, text_);
            EdittingTextField.text = "";
            Selectflag = false;
            EdittingTextPanel.SetActive(false);
            Debug.Log("editing text");
        }
    }
    public void TextCloseButtonOnclick()
    {
        EdittingTextField.text = "";
        Selectflag = false;
        EdittingTextPanel.SetActive(false);
        Debug.Log("textfield closed");
    }
    public void EnlargeButtonOnclick()
    {
        Text textfont = this.GetComponentInChildren<Text>();
        textfont.fontSize += 2;// (int)scroll*100;
        monobitView.RPC("RecvfontSize", MonobitTargets.OthersBuffered, textfont.fontSize);
    }
    public void ShrinkButtonOnclick()
    {
        Text textfont = this.GetComponentInChildren<Text>();
        if (textfont.fontSize >= 32)
        {
            textfont.fontSize -= 2;
        }
        else { textfont.fontSize = 32; }
        monobitView.RPC("RecvfontSize", MonobitTargets.OthersBuffered, textfont.fontSize);
    }

    [MunRPC]
    public void RecvfontSize(int fontSize)
    {
        Text textfont = this.GetComponentInChildren<Text>();
        textfont.fontSize = fontSize;
        Debug.Log("フォントサイズ変更");
    }

    void OnDestroy()
    {
        MonobitNetwork.Destroy(monobitView);
    }

}
