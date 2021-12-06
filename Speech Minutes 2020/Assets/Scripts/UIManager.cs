using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MonobitEngine;

public class UIManager : MonobitEngine.MonoBehaviour
{
    [SerializeField] GameObject WadaiPanel;
    [SerializeField] GameObject WhiteBoardPanel;
    [SerializeField] GameObject SendTextPanel;
    // Start is called before the first frame update
    [SerializeField] GameObject WadaiElementsPanel;


    [SerializeField] GameObject HostSettingButton;
    [SerializeField] GameObject PreHostSettingPanel;
    [SerializeField] GameObject HostChangePanel;
    [SerializeField] GameObject kickPanel;


    [SerializeField] GameObject ClientSettingButton;
    [SerializeField] GameObject PreRequestPanel;
    [SerializeField] GameObject RequestPanel;

    bool EnableWadaiPanel = true;
    bool EnableWhiteBoardPanel = true;
    bool EnableSendTextPanel = true;

    bool EnableHostSetting = true;
    bool EnableClientSetting = true;

    public GameObject PrefabobjforKick;
    public GameObject PrefabobjforHost;

    public Transform ContentforKick;
    public Transform ContentforHost;

    void Start()
    {
        EnableWadaiPanel = true;
        EnableWhiteBoardPanel = true;
        EnableSendTextPanel = true;
        var parent1 = ContentforKick.transform;
        var parent2 = ContentforHost.transform;
        foreach (MonobitEngine.MonobitPlayer player in MonobitEngine.MonobitNetwork.otherPlayersList)
        {
            GameObject obj1 = Instantiate(PrefabobjforKick, Vector3.zero, Quaternion.identity, parent1);
            GameObject obj2 = Instantiate(PrefabobjforHost, Vector3.zero, Quaternion.identity, parent2);
            obj1.GetComponentInChildren<Text>().text = "name" + " " + player.name + " " + "id" + " " + player.ID;
            obj2.GetComponentInChildren<Text>().text = "name" + " " + player.name + " " + "id" + " " + player.ID;
        }

    }

    void Update()
    {
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            HostSettingButton.SetActive(true);
            ClientSettingButton.SetActive(false);
        }
        if (!MonobitEngine.MonobitNetwork.isHost)
        {
            HostSettingButton.SetActive(false);
            ClientSettingButton.SetActive(true);
        }

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
        if (!MonobitEngine.MonobitNetwork.player.isHost)
        {
            WadaiElementsPanel.SetActive(false);
        }
        WadaiElementsPanel.SetActive(true);
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

    public void OnClickHostsetting()
    {
        PreHostSettingPanel.SetActive(EnableHostSetting);
        EnableHostSetting = !EnableHostSetting;
        HostChangePanel.SetActive(false);
        kickPanel.SetActive(false);
    }

    public void OnClickChangeHostButton()
    {
        HostChangePanel.SetActive(true);
    }

    public void OnClickChangeHostCancel()
    {
        HostChangePanel.SetActive(false);
    }
    public void OnClickKickButton()
    {
        kickPanel.SetActive(true);
    }

    public void OnClickClientSetting()
    {
        PreRequestPanel.SetActive(EnableClientSetting);
        EnableClientSetting = !EnableClientSetting;
    }

    public void OnClickPreRequestPanel()
    {
        RequestPanel.SetActive(true);
    }

    public void OnClickRequestOK()
    {
        Debug.Log("WannabeHost");
    }
    public void OnClickRequestCancel()
    {
        RequestPanel.SetActive(false);
    }

    public void OnOtherPlayerConnected(MonobitPlayer newPlayer)
    {
        var parent1 = ContentforKick.transform;
        var parent2 = ContentforHost.transform;
        GameObject obj1 = Instantiate(PrefabobjforKick, Vector3.zero, Quaternion.identity, parent1);
        GameObject obj2 = Instantiate(PrefabobjforHost, Vector3.zero, Quaternion.identity, parent2);
        obj1.GetComponentInChildren<Text>().text = "name" + " " + newPlayer.name + " " + "id" + " " + newPlayer.ID;
        obj2.GetComponentInChildren<Text>().text = "name" + " " + newPlayer.name + " " + "id" + " " + newPlayer.ID;
    }


}