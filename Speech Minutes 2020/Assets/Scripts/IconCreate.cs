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
    MainSecneMUNScript script;
    private Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("MUN").GetComponent<MainSecneMUNScript>();
        
        UserID = script.Icon;
        IconName.GetComponent<Text>().text = MonobitNetwork.playerList[UserID - 1].name;
        IconInitial.GetComponent<Text>().text = MonobitNetwork.playerList[UserID - 1].name.Substring(0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mutejudge()
    {
        Debug.Log("mutejudge");
        Debug.Log("id:"+UserID);
        if (UserID == script.muteid)
        {
            MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteon");
            Debug.Log("muteon");
        }
        else if (UserID == script.notmuteid)
        {
            MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteoff");
            Debug.Log("muteoff");
        }
    }
}
