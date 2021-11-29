using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MonobitEngine;
public class WadaiChange : MonobitEngine.MonoBehaviour
{
    public GameObject[] Button;
    public InputField inputField;
    public Text[] text;
    public Text[] WadaiThemaText;
    int dropdown2;
    public Text text2;
    int NowBottonPushed = -1;


    //Dropdownを格納する変数
    [SerializeField] private Dropdown dropdown;
    /// <summary>
    /// inputfieldとプルダウンの数値を定義
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
        dropdown2 = dropdown.value;
    }

    /// <summary>
    /// プルダウン動作時に話題の文字を参照
    /// </summary>
    private void Update()
    {
        if(dropdown.value != dropdown2)
        {
            if(text[dropdown.value].text != "")
             {
                    inputField.text = text[dropdown.value].text;
             }
           /* InputField form = GameObject.Find("InputField").GetComponent<InputField>();
            form.text = "";*/
            dropdown2 = dropdown.value;
        }
    }
    
   // 誰かがルームにログインしたときの処理
    /*public void OnOtherPlayerConnected(MonobitPlayer newPlayer)
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdikyouyuu", MonobitTargets.All,NowBottonPushed,text2.text);
        }
    }
    [MunRPC]
    public void wdikyouyuu(int va, string y)
    {
        NowBottonPushed = va;
        text2.text = y;
        if(va == 0)
        {
            text2.color = new Color32(189, 193, 74, 255);
        }
        if(va == 1)
        {
            text2.color = new Color32(195, 160, 65, 255);
        }
        if(va == 2)
        {
            text2.color = new Color32(207, 89, 81, 255);
        }
        if(va == 3)
        {
            text2.color = new Color32(207, 75, 200, 255);
        }
        if(va == 4)
        {
            text2.color = new Color32(144, 82, 204, 255);
        }
        if(va == 5)
        {
            text2.color = new Color32(74, 87, 202, 255);
        }
        if(va == 6)
        {
            text2.color = new Color32(63, 197, 212, 255);
        }
        if(va == 7)
        {
            text2.color = new Color32(62, 207, 69, 255);
        }
    }*/
    public void Wadai1()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdi1", MonobitTargets.All);
        }
    }
    public void Wadai2()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdi2", MonobitTargets.All);
        }
    }
    public void Wadai3()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdi3", MonobitTargets.All);
        }
    }
    public void Wadai4()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdi4", MonobitTargets.All);
        }
    }
    public void Wadai5()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdi5", MonobitTargets.All);
        }
    }
    public void Wadai6()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdi6", MonobitTargets.All);
        }
    }
    public void Wadai7()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdi7", MonobitTargets.All);
        }
    }
    public void Wadai8()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            monobitView.RPC("wdi8", MonobitTargets.All);
        }
    }
    [MunRPC]
    public void wdi1()
    {
        NowBottonPushed = 0;
    }
    [MunRPC]
    public void wdi2()
    {
        NowBottonPushed = 1;
    }
    [MunRPC]
    public void wdi3()
    {
        NowBottonPushed = 2;
    }
    [MunRPC]
    public void wdi4()
    {
        NowBottonPushed = 3;
    }
    [MunRPC]
    public void wdi5()
    {
        NowBottonPushed = 4;
    }
    [MunRPC]
    public void wdi6()
    {
        NowBottonPushed = 5;
    }
    [MunRPC]
    public void wdi7()
    {
        NowBottonPushed = 6;
    }
    [MunRPC]
    public void wdi8()
    {
        NowBottonPushed = 7;
    }

    /// <summary>
    /// inputfieldの文字を送信ボタンで反映
    /// </summary>
    public void WadaiSend()
    {
        //テキストにinputFieldの内容を反映
        //InputField form = GameObject.Find("wadaiInputField").GetComponent<InputField>();
        //form.text = inputField.text;
        Debug.Log(dropdown.value);
        Debug.Log(inputField.text);
        monobitView.RPC("wdi", MonobitTargets.All,dropdown.value,inputField.text);
        /*text[dropdown.value].text = inputField.text;
        WadaiThemaText[dropdown.value].text = inputField.text;
        //display.text = inputField.text;
        //オブジェクトを表示する
        //  gametext.gameObject.SetActive(true);
        //インプットフィールドの中身を消す
        GameObject.Find("wadaiInputField").GetComponent<InputField>().text = "";*/
        //gametext.gameObject.SetActive(false);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void wdi(int va, string tx)
    {
        text[va].text = tx;
        WadaiThemaText[va].text = tx;
        GameObject.Find("wadaiInputField").GetComponent<InputField>().text = "";
        if (NowBottonPushed == va)
        {
            text2.text = "現在の話題："+tx;
            if(va == 0)
            {
                text2.color = new Color32(189, 193, 74, 255);
            }
            if(va == 1)
            {
                text2.color = new Color32(195, 160, 65, 255);
            }
            if(va == 2)
            {
                text2.color = new Color32(207, 89, 81, 255);
            }
            if(va == 3)
            {
                text2.color = new Color32(207, 75, 200, 255);
            }
            if(va == 4)
            {
                text2.color = new Color32(144, 82, 204, 255);
            }
            if(va == 5)
            {
                text2.color = new Color32(74, 87, 202, 255);
            }
            if(va == 6)
            {
                text2.color = new Color32(63, 197, 212, 255);
            }
            if(va == 7)
            {
                text2.color = new Color32(62, 207, 69, 255);
            }
        }
    }
    



}
