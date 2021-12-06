using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;
using MonobitEngine.VoiceChat;
using UnityEngine.UI;
using System.Linq;

public class KickPlayer : MonobitEngine.MonoBehaviour
{
    public Text text;

    public GameObject KickOKButton;

    private string line;

    private int id;

    private int firstid;

    private void Start()
    {
        firstid = MonobitEngine.MonobitNetwork.otherPlayersList.First().ID;
        Debug.Log("first id is" + firstid);
        line = text.text;
        string[] str = line.Split(' ');
        id = int.Parse(str[3]);
        Debug.Log(id);
    }
    public void OnClickKickButton()
    {
        KickOKButton.SetActive(true);
    }

    public void OnclickKickOKButton()
    {
        MonobitEngine.MonobitNetwork.Kick(MonobitEngine.MonobitNetwork.otherPlayersList[id]);
        Destroy(gameObject);
    }

    public virtual void OnOtherPlayerDisconnected(MonobitPlayer otherPlayer)
    {
        if (id == otherPlayer.ID)
        {
            Destroy(this.gameObject);
        }
    }
}
