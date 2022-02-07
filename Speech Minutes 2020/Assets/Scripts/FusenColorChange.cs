using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;

public class FusenColorChange : MonobitEngine.MonoBehaviour
{
    [SerializeField]
    GameObject FusenPanel;
    Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        
    }

    //付箋の色を変更するメソッド
    public void ChangeColor(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                //自分の付箋の色を変更する処理
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#ffc0cb");
                //自分以外にも上記の付箋の色変更が反映される処理
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;
                
            case 1:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#fffacd");
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;

            case 2:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#98fb98");
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;

            case 3:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#fa8072");
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;

            case 4:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#87cefa");
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;

            default:
                break;
        }
    }

    //カラーコードから色を呼び出すメソッド
    public  Color ToColor(string Color)
    {
        var color = default(Color);
        if (!ColorUtility.TryParseHtmlString(Color, out color))
         {
        }
        return color;
    }

    //ドロップダウンの値から付箋の色を変える処理を自分以外にMunRPCを使って送るメソッド
    [MunRPC]
    public void RecvTextColor(int colorValue)
    {
        switch (colorValue)
        {
            case 0:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#ffc0cb");
                dropdown.value = colorValue;
                break;
            case 1:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#fffacd");
                dropdown.value = colorValue;
                break;
            case 2:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#98fb98");
                dropdown.value = colorValue;
                break;
            case 3:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#fa8072");
                dropdown.value = colorValue;
                break;
            case 4:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#87cefa");
                dropdown.value = colorValue;
                break;
            default:
                break;
        }
    }
}
