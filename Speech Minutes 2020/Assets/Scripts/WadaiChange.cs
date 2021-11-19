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

        /*
        //DropdownのValueが0のとき（赤が選択されているとき）
        if (dropdown.value == 0)
        {

        }
        //DropdownのValueが1のとき（青が選択されているとき）
        else if (dropdown.value == 1)
        {
            InputField form = GameObject.Find("InputField").GetComponent<InputField>();
            form.text = "";
        }
        //DropdownのValueが2のとき（黄が選択されているとき）
        else if (dropdown.value == 2)
        {

        }
        //DropdownのValueが3のとき（白が選択されているとき）
        else if (dropdown.value == 3)
        {

        }
        //DropdownのValueが4のとき（黒が選択されているとき）
        else if (dropdown.value == 4)
        {

        }
        else if (dropdown.value == 5)
        {

        }
        //DropdownのValueが3のとき（白が選択されているとき）
        else if (dropdown.value == 6)
        {

        }
        //DropdownのValueが4のとき（黒が選択されているとき）
        else if (dropdown.value == 7)
        {

        }*/
    }
    
    /*public void Update()
    {
        //テキストにinputFieldの内容を反映
        if(text[dropdown.value].text != "")
        {
            inputField.text = text[dropdown.value].text;
        }
       
       // display.text = inputField.text;
        //オブジェクトを表示する
        //  gametext.gameObject.SetActive(true);
    }*/

    //オプションが変更されたときに実行するメソッド
    /*public void InputText()
    {
        if (dropdown.value != dropdown2)
        {
            InputField form = GameObject.Find("wadaiInputField").GetComponent<InputField>();
            form.text = "";
            dropdown2 = dropdown.value;
        }
        text[dropdown.value].text = inputField.text;
    }*/
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
    }
    



}
