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
    public int UserID;
    // Start is called before the first frame update
    void Start()
    {
        if (monobitView.isMine)
        {
            UserID = MonobitEngine.MonobitNetwork.player.ID;
            IconName.GetComponent<Text>().text = MonobitNetwork.playerList[UserID - 1].name;
            IconInitial.GetComponent<Text>().text = MonobitNetwork.playerList[UserID - 1].name.Substring(0, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
