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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Icondicision()
    {
        script = GameObject.Find("MUN").GetComponent<MainSecneMUNScript>();
        UserID = script.Iconid;
        Debug.Log("dicision:" + UserID);
        UserName = script.Iconname;
        IconName.GetComponent<Text>().text = UserName;
        IconInitial.GetComponent<Text>().text = UserName.Substring(0, 1);
        monobitView.RPC("IconSync", MonobitTargets.OthersBuffered, UserID, UserName);
    }

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
    public void IconSync(int id, string name)
    {
        UserID = id;
        UserName = name;
        IconName.GetComponent<Text>().text = UserName;
        IconInitial.GetComponent<Text>().text = UserName.Substring(0, 1);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void MuteIconSync(int id)
    {
        MuteID = id;
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteon");
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void NotMuteIconSync(int id)
    {
        NotMuteID = id;
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteoff");
    }
}
