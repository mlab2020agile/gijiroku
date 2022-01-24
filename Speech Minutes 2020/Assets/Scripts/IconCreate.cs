using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;
using MonobitEngine.VoiceChat;
using UnityEngine.UI;

public class IconCreate : MonobitEngine.MonoBehaviour
{
    public GameObject UserIcon;
    public Text IconName;
    public Image IconImage;
    public Text IconInitial;
    public Image MuteImage;
    public int UserID;
    public string UserName;
    public int MuteID;
    public int NotMuteID;
    MainSecneMUNScript script;
    private Sprite sprite;
    LineUpIcon lineupiconscript;
    public List<int> IconStateList = new List<int> { 0,0,0,0,0,0,0,0,0,0 };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    //ユーザーアイコンの中身作成
    public void Icondicision()
    {
        script = GameObject.Find("MUN").GetComponent<MainSecneMUNScript>();
        UserID = script.Iconid;
        Debug.Log("Icondicision:" + UserID);
        switch (UserID%9)
        {
            case 0:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon0");
                break;
            case 1:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon1");
                break;
            case 2:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon2");
                break;
            case 3:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon3");
                break;
            case 4:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon4");
                break;
            case 5:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon5");
                break;
            case 6:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon6");
                break;
            case 7:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon7");
                break;
            case 8:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon8");
                break;
            default:
                break;
        }
        UserName = script.Iconname;
        IconName.GetComponent<Text>().text = UserName;
        IconInitial.GetComponent<Text>().text = UserName.Substring(0, 1);
        monobitView.RPC("IconSync", MonobitTargets.OthersBuffered, UserID, UserName);
    }
    //ユーザーアイコン位置更新
    public void IconPositionUpdate()
    {
        switch (IconOrder(MonobitEngine.MonobitNetwork.player.ID))
        {
            case 1:
                UserIcon.transform.localPosition = new Vector3(0, 0, 0);
                break;
            case 2:
                UserIcon.transform.localPosition = new Vector3(160, 0, 0);
                break;
            case 3:
                UserIcon.transform.localPosition = new Vector3(0, -150, 0);
                break;
            case 4:
                UserIcon.transform.localPosition = new Vector3(160, -150, 0);
                break;
            default:
                UserIcon.transform.localPosition = new Vector3(1000, 1000, 0);
                break;
        }
        monobitView.RPC("IconPositionSync", MonobitTargets.OthersBuffered, IconOrder(MonobitEngine.MonobitNetwork.player.ID));
    }
    //ミュートアイコンに変更
    public void MuteSituation()
    {
        script = GameObject.Find("MUN").GetComponent<MainSecneMUNScript>();
        MuteID = script.muteid;
        Debug.Log("mutesituation");
        Debug.Log("id:"+UserID);
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteon");
        Debug.Log("muteon");
        monobitView.RPC("MuteIconSync", MonobitTargets.OthersBuffered, MuteID);
    }
    //ミュート解除アイコンに変更
    public void NotMuteSituation()
    {
        script = GameObject.Find("MUN").GetComponent<MainSecneMUNScript>();
        NotMuteID = script.notmuteid;
        Debug.Log("notmutesituation");
        Debug.Log("id:" + UserID);
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteoff");
        Debug.Log("muteoff");
        monobitView.RPC("NotMuteIconSync", MonobitTargets.OthersBuffered, NotMuteID);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //ユーザーアイコン位置変更
    public void IconSync(int id, string name)
    {
        UserID = id;
        Debug.Log("IconSync:" + UserID);
        switch (UserID % 9)
        {
            case 0:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon0");
                break;
            case 1:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon1");
                break;
            case 2:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon2");
                break;
            case 3:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon3");
                break;
            case 4:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon4");
                break;
            case 5:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon5");
                break;
            case 6:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon6");
                break;
            case 7:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon7");
                break;
            case 8:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon8");
                break;
            default:
                break;
        }
        UserName = name;
        IconName.GetComponent<Text>().text = UserName;
        IconInitial.GetComponent<Text>().text = UserName.Substring(0, 1);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //ミュートアイコン同期
    public void MuteIconSync(int id)
    {
        MuteID = id;
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteon");
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //ミュート解除アイコン同期
    public void NotMuteIconSync(int id)
    {
        NotMuteID = id;
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteoff");
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //ユーザーアイコン位置同期
    public void IconPositionSync(int number)
    {
        switch (number)
        {
            case 1:
                UserIcon.transform.localPosition = new Vector3(0, 0, 0);
                break;
            case 2:
                UserIcon.transform.localPosition = new Vector3(160, 0, 0);
                break;
            case 3:
                UserIcon.transform.localPosition = new Vector3(0, -150, 0);
                break;
            case 4:
                UserIcon.transform.localPosition = new Vector3(160, -150, 0);
                break;
            default:
                UserIcon.transform.localPosition = new Vector3(1000, 1000, 0);
                break;
        }
    }
    //リスト追加
    public void AddList()
    {
        IconStateList.Add(0);
    }
    //リスト作成
    public void CreateList(int id)
    {
        for (int i = 0; i < id; i++)
        {
            IconStateList.Add(0);
        }
    }
    //リスト変更
    public void ChangeList(int id, int state)
    {
        IconStateList[id] = state;
        Debug.Log("changelist:"+ id+","+IconStateList[id]);
        monobitView.RPC("ChangeListSync", MonobitTargets.OthersBuffered, id, state);
    }
    //リストの何番目か
    public int List(int n)
    {
        int order = 0;
        for (int i = 0; i < MonobitNetwork.room.playerCount; i++)
        {
            if (n == MonobitNetwork.playerList[i].ID)
            {
                order = i;
            }
        }
        return order;
    }
    //ユーザーアイコンが何番目に表示されるか
    public int IconOrder(int number)
    {
        int list;
        int icondisplaynumber = 0;
        list = List(number);
        for (int i = 0; i < list + 1; i++)
        {
            if (IconStateList[MonobitNetwork.playerList[i].ID + 1] == 0)
            {
                icondisplaynumber++;
            }
        }
        if (IconStateList[MonobitNetwork.playerList[list].ID + 1] == 1)
        {
            return 0;
        }
        else
        {
            return icondisplaynumber;
        }
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void ChangeListSync(int id, int state)
    {
        IconStateList[id] = state;
        Debug.Log("changelistsync:" + id + "," + IconStateList[id]);
    }
}
