using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MonobitEngine;

public class TextManager : MonobitEngine.MonoBehaviour
{
    //オブジェクトと結びつける
    public InputField inputField;//textbox
    public Text text;//textoutput
    public Text display;
    public GameObject canvas;//キャンバス

    void Start()
    {
        //Componentを扱えるようにする
        inputField = inputField.GetComponent<InputField>();
        //text = text.GetComponent<Text>();
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        //text.text = inputField.text;
        display.text = inputField.text;
    }
    int resetcunter = 0;
    public RectTransform input_rectTransform = null;
    public RectTransform display_rectTransform = null;

    //テキストボックスへの入力が終わった時に呼び出すメソッド
    public void EndEdit()
    { 
        if (GameObject.Find("TextBox").GetComponent<InputField>().text != "") {
            //テキストがあればプレハブからオブジェクト生成
            //FusenPanel.gameObject.SetActive(true);
            //GameObject prefab = (GameObject)Instantiate(FusenPanel);
            GameObject prefab = MonobitEngine.MonobitNetwork.Instantiate("FusenCanvas", Vector3.zero, Quaternion.identity, 0);
            Debug.Log("複製完了");
            //prefab.transform.SetParent(canvas.transform, false);
            Text prefabtext = prefab.GetComponentInChildren<Text>();
            prefabtext.text = display.text;

            if (resetcunter<4)
            {
                resetcunter += 1;
                input_rectTransform.position += new Vector3(10, -20, 0f);
                display_rectTransform.position = input_rectTransform.position;
            }else if(resetcunter>=4)
            {
                resetcunter = 0;
                input_rectTransform.position += new Vector3(-40, 80, 0f); ;
                display_rectTransform.position = input_rectTransform.position;
            }
        }
    }

    //付箋作成のテキストフィールド横の送信ボタンを押した時の処理
    public void Send()
    {
        //インプットフィールドの中身を消す
        GameObject.Find("TextBox").GetComponent<InputField>().text = "";
    }
}
