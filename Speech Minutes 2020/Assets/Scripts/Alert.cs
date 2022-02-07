using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Alert : MonoBehaviour
{
    private GameObject MUN;
    StartSecneMUNScript tart;
    public GameObject Text2;
    // Start is called before the first frame update
    void Start()
    {
        //MUN = GameObject.Find("MUN");
        //tart = GameObject.Find("MUN").GetComponent<StartSecneMUNScript>();
        string a = StartSecneMUNScript.abcd123;
        Text2.GetComponent<Text>().text = a;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //「OK」ボタン押すと元のシーンに戻る
    public void OnClickReturn()
    {
        SceneManager.LoadScene("StartScene");
    }
}
