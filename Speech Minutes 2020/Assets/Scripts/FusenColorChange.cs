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
    //private int colorValue;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        
    }

    // Update is called once per frame
    /*void Update()
    {
    
        //ドロップダウンの値によって色を変化させる
        if (dropdown.value == 0)
        {
            FusenPanel.GetComponentInChildren<Image>().color = Color.magenta;

        }

        if (dropdown.value == 1)
        {
            FusenPanel.GetComponentInChildren<Image>().color = Color.yellow;
        }

        if (dropdown.value == 2)
        {
            FusenPanel.GetComponentInChildren<Image>().color = Color.green;
        }

    }*/
    public void ChangeColor(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#ffc0cb");
                //colorValue = dropdown.value;
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;
                
            case 1:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#fffacd");
                //colorValue = dropdown.value;
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;

            case 2:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#98fb98");
                //colorValue = dropdown.value;
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;

            case 3:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#fa8072");
                //colorValue = dropdown.value;
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;

            case 4:
                FusenPanel.GetComponentInChildren<Image>().color = ToColor("#87cefa");
                //colorValue = dropdown.value;
                monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
                break;

            default:
                break;
        }
    }
    public  Color ToColor(string Color)
    {
        var color = default(Color);
        if (!ColorUtility.TryParseHtmlString(Color, out color))
         {
        }
        return color;
    }

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
