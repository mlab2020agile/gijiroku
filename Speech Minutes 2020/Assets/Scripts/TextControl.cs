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
    public GameObject teO;
    public GameObject PenButton;
    public GameObject FinishButton;
    public GameObject EnlargeButton;
    public GameObject ShrinkButton;
    public GameObject EditInputFieldObject;
    public InputField EditInputField;
    public GameObject HideButton;
    public GameObject dropdown;
    public GameObject VisibleButton;
    public GameObject DeleteButton;
    private int touchCount = 0;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        // Textコンポーネントを取得
        Text text = this.GetComponentInChildren<Text>();
        Debug.Log(text.text);
        // 色を指定
        text.color = Color.black;
        EditInputFieldObject.SetActive(false);
        if (monobitView.isMine)
        {
        string text_ = this.GetComponentInChildren<Text>().text;
        monobitView.RPC("RecvChattext", MonobitTargets.OthersBuffered, text_);
        }
    }

    //付箋のテキスト内容を反映させるメソッド
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
        }
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
                    touchCount++;
                    //0.3秒後にHogeメソッドを呼び出す
                     Invoke("DoubleclickJudg", 0.3f);
            }
            //付箋にカーソルが重なっているか
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (PenButton.activeSelf)
                {
                    HideButton.SetActive(true);
                }
                else
                {
                    VisibleButton.SetActive(true);
                }
            }
            else
            {
                    HideButton.SetActive(false);
                    VisibleButton.SetActive(false);
            }
        }
        if (Selectflag == true && Input.GetKey(KeyCode.Backspace))
        {
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

    //付箋の編集ボタンを押した時に呼び出されるメソッド
    public void PenButtonOnclick()
    { 
        Text text = this.GetComponentInChildren<Text>();
        EditInputField.text = text.text;
        Selectflag = false;
        EditInputFieldObject.SetActive(true);
    }

    //付箋を編集中に付箋上部の編集テキストフィールド横のチェックマークボタンを押した時に呼び出されるメソッド
    public void FinishButtonOnclick()
    {
        if (EditInputFieldObject.activeSelf)
        {
            // Textコンポーネントを取得
            Text text = GetComponentInChildren<Text>();
            text.text = EditInputField.text;
            string text_ = text.text.ToString() ;
            monobitView.RPC("RecvChattext", MonobitTargets.OthersBuffered, text_);
            EditInputField.text = "";
            Selectflag = false;
            EditInputFieldObject.SetActive(false);
            Debug.Log("editing text");
        }
    }

    //付箋の拡大ボタンを押した時に呼び出されるメソッド
    public void EnlargeButtonOnclick()
    {
        Text textfont = this.GetComponentInChildren<Text>();
        textfont.fontSize += 2;　// (int)scroll*100;
        monobitView.RPC("RecvfontSize", MonobitTargets.OthersBuffered, textfont.fontSize);
    }

    //付箋の縮小ボタンを押した時に呼び出されるメソッド
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

    //付箋の削除ボタンを押した時に呼び出されるメソッド
    public void DeleteButtonOnclick()
    {
        OnDestroy();
        Selectflag = false;
        Debug.Log("false&destroy");
    }

    //付箋のテキストサイズを変更するメソッド
    [MunRPC]
    public void RecvfontSize(int fontSize)
    {
        Text textfont = this.GetComponentInChildren<Text>();
        textfont.fontSize = fontSize;
        Debug.Log("フォントサイズ変更");
    }

    //付箋の削除する時のメソッド
    void OnDestroy()
    {
        MonobitNetwork.Destroy(monobitView);
    }

    //付箋をダブルクリックで編集する時の判定メソッド
    void DoubleclickJudg()
    {
        //ダブルタッチされているか
        if (touchCount != 2) { touchCount = 0; return; }
        else { touchCount = 0; }

        //以下実行したい処理
        PenButtonOnclick();
    }

    //付箋周りのUIを隠すボタン(湾曲した矢印)を押した時に呼び出されるメソッド
    public void HideOnclick()
    {
            PenButton.SetActive(false);
            EnlargeButton.SetActive(false);
            ShrinkButton.SetActive(false);
            dropdown.SetActive(false);
            DeleteButton.SetActive(false);
        HideButton.SetActive(false);
        VisibleButton.SetActive(true);

    }
    //付箋周りのUIを再表示ボタン(3点リーダ)を押した時に呼び出されるメソッド
    public void visibleOnclick()
    {
            PenButton.SetActive(true);
            EnlargeButton.SetActive(true);
            ShrinkButton.SetActive(true);
            dropdown.SetActive(true);
            DeleteButton.SetActive(true);
        HideButton.SetActive(true);
        VisibleButton.SetActive(false);
    }
}
