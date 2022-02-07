using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLineWeight : MonoBehaviour
{
    GameObject whiteboard;
    PixAccess  script;
    Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        whiteboard = GameObject.Find("Plane"); //Unityちゃんをオブジェクトの名前から取得して変数に格納する
        script = whiteboard.GetComponent<PixAccess>();
        dropdown = GetComponent<Dropdown>();
    }

    // Update is called once per frame
    //ホワイトボードのUIの中にある太さ変更のプルダウン
    void Update()
    {
        if(dropdown.value == 0){
            script.lineWidth = 1;
        }

        if(dropdown.value == 1){
            script.lineWidth = 2;
        }

        if(dropdown.value == 2){
            script.lineWidth =4;
        }
    }
}
