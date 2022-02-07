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

    public RectTransform HostChangePanelRect;

    public CanvasGroup HostChangePanelCanvasGroup;

    public RectTransform kickPanelRect;

    public CanvasGroup kickPanelCanvasGroup;

    public GameObject RequestWaitPanel;

    public Text hascometext;

    private string hascomeName;

    private int hascomeID;

    public GameObject AllowPanel;

    public GameObject DisallowPanel;

    int HostJudge=0;



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
        HostChangePanel.SetActive(true);
        OnClickChangeHostCancel();
        kickPanel.SetActive(true);
        OnClickKickCancel();
        hascomeID = MonobitEngine.MonobitNetwork.player.ID;
        hascomeName = MonobitEngine.MonobitNetwork.player.name;
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
        if(HostJudge==1&&MonobitEngine.MonobitNetwork.isHost){
            HostJudge=0;
            AllowPanel.SetActive(true);
            hascometext.text = hascomeName + "からホスト権のリクエストが来ています。";
        }
        if(HostJudge==2&&!MonobitEngine.MonobitNetwork.isHost){
            HostJudge=0;
        DisallowPanel.SetActive(true);
        RequestWaitPanel.SetActive(false);
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
    //ホスト設定ボタンが押されたとき
    public void OnClickHostsetting()
    {
        PreHostSettingPanel.SetActive(EnableHostSetting);
        EnableHostSetting = !EnableHostSetting;
        //kickPanel.SetActive(false);

        HostChangePanelCanvasGroup.alpha = 0;
        HostChangePanelRect.SetAsFirstSibling();
        kickPanelCanvasGroup.alpha = 0;
        kickPanelRect.SetAsFirstSibling();
    }
    //ホスト変更ボタンが押されたとき
    public void OnClickChangeHostButton()
    {
        HostChangePanelCanvasGroup.interactable = true;
        HostChangePanelCanvasGroup.alpha = 1;
        HostChangePanelRect.SetAsLastSibling();
        /*Vector3 localPos = HostChangePanelTransform.localPosition;
        localPos.y = -74;
        HostChangePanelTransform.localPosition = localPos;*/
    }
    //ホスト変更キャンセルボタンが押されたとき
    public void OnClickChangeHostCancel()
    {
        HostChangePanelCanvasGroup.interactable = false;
        HostChangePanelCanvasGroup.alpha = 0;
        HostChangePanelRect.SetAsFirstSibling();
        /*Vector3 localPos = HostChangePanelTransform.localPosition;
        localPos.y = 340;
        HostChangePanelTransform.localPosition = localPos;*/
    }
    //キックボタンが押されたとき
    public void OnClickKickButton()
    {
        kickPanelCanvasGroup.interactable = true;
        kickPanelCanvasGroup.alpha = 1;
        kickPanelRect.SetAsLastSibling();
    }
    //キックキャンセルボタンが押されたとき
    public void OnClickKickCancel()
    {
        kickPanelCanvasGroup.interactable = false;
        kickPanelCanvasGroup.alpha = 0;
        kickPanelRect.SetAsFirstSibling();
    }
    //クライアント設定ボタンが押されたとき
    public void OnClickClientSetting()
    {
        PreRequestPanel.SetActive(EnableClientSetting);
        EnableClientSetting = !EnableClientSetting;
    }

    public void OnClickPreRequestPanel()
    {
        RequestPanel.SetActive(true);
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
    //ホスト権要求を承諾するボタンを押したとき
    public void OnRequestOkClick()
    {
        HostJudge=1;
        monobitView.RPC("hascome", MonobitEngine.MonobitTargets.Host,hascomeName,hascomeID,HostJudge);
        RequestPanel.SetActive(false);
        RequestWaitPanel.SetActive(true);
    }

    [MunRPC]
    public void hascome(string name,int id,int judge)
    {
        HostJudge=judge;
        //hascometext.text = name + "からホスト権のリクエストが来ています。";
        hascomeName = name;
        hascomeID = id;
    }
    //ホスト権要求キャンセルボタンが押されたとき
    public void OnRequestCancelClick()
    {
        RequestPanel.SetActive(false);
    }
    //ホスト権要求が認められたとき
    public void RequestAllow()
    {
        AllowPanel.SetActive(false);
        for (int i = 0; i < MonobitEngine.MonobitNetwork.otherPlayersList.Length; i++)
        {
            if (hascomeID == MonobitEngine.MonobitNetwork.otherPlayersList[i].ID)
            {
                if (MonobitEngine.MonobitNetwork.isHost && MonobitEngine.MonobitNetwork.otherPlayersList.Length > 0)
                {
                    MonobitEngine.MonobitNetwork.ChangeHost(MonobitEngine.MonobitNetwork.otherPlayersList[i]);
                }
            }
        }
    }
    //ホスト権要求が認められないとき
    public void RequestDisallow()
    {
        HostJudge=2;
        AllowPanel.SetActive(false);
        monobitView.RPC("Disallow", MonobitTargets.Host,HostJudge);
    }
    [MunRPC]
    public void Disallow(string name,int judge)
    {
        HostJudge=judge;
    }
    public void OnDisallowOKClick()
    {
        DisallowPanel.SetActive(false);
    }
}