using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject WadaiPanel;
    [SerializeField] GameObject WhiteBoardPanel;
    [SerializeField] GameObject SendTextPanel;
    // Start is called before the first frame update

    bool EnableWadaiPanel = true;
    bool EnableWhiteBoardPanel = true;
    bool EnableSendTextPanel = true;

    void Start()
    {
        EnableWadaiPanel = true;
        EnableWhiteBoardPanel = true;
        EnableSendTextPanel = true;
    }

    //UI右上の話題選択ボタンが押されたとき
    public void OnclickWadaiButton()
    {
        //話題パネルをアクティブ化させ、他のパネルを非アクティブ化させる
        WhiteBoardPanel.SetActive(false);
        SendTextPanel.SetActive(false);
        EnableWhiteBoardPanel = true;
        EnableSendTextPanel = true;

        WadaiPanel.SetActive(EnableWadaiPanel);
        EnableWadaiPanel = !EnableWadaiPanel;
    }

    //UI右上のホワイトボードボタンが押されたとき
    public void OnclickWhiteBoardButton()
    {
        //ホワイトボードパネルをアクティブ化させ、他のパネルを非アクティブ化させる
        WadaiPanel.SetActive(false);
        SendTextPanel.SetActive(false);
        EnableWadaiPanel = true;
        EnableSendTextPanel = true;

        WhiteBoardPanel.SetActive(EnableWhiteBoardPanel);
        EnableWhiteBoardPanel = !EnableWhiteBoardPanel;
    }

    //UI真ん中下のテキスト送信ボタンが押されたとき
    public void OnclickSendTextButton()
    {
        //テキスト送信パエルをアクティブ化させ、他のパネルを非アクティブ化させる
        WadaiPanel.SetActive(false);
        WhiteBoardPanel.SetActive(false);
        EnableWadaiPanel = true;
        EnableWhiteBoardPanel = true;

        SendTextPanel.SetActive(EnableSendTextPanel);
        EnableSendTextPanel = !EnableSendTextPanel;
    }



}
