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
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        monobitView.RPC("RecvTextColor", MonobitTargets.OthersBuffered, dropdown.value);
        
    
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

    }
    
    [MunRPC]
    public void RecvTextColor(int colorValue)
    {
        switch (colorValue)
        {
            case 0:
                FusenPanel.GetComponentInChildren<Image>().color = Color.magenta;
                
                break;
            case 1:
                FusenPanel.GetComponentInChildren<Image>().color = Color.yellow;
              
                    break;

            case 2:
                FusenPanel.GetComponentInChildren<Image>().color = Color.green;
              
                break;
        }
    }
}
