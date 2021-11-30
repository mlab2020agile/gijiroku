using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;
using MonobitEngine.VoiceChat;
using UnityEngine.UI;
using System.Linq;

public class ChangeHost : MonobitEngine.MonoBehaviour
{
    public Text text;

    public GameObject HostChangeOKButton;

    private string line;

    private int id;

    private int firstid;

    private void Start()
    {
        firstid = MonobitEngine.MonobitNetwork.otherPlayersList.First().ID;
        Debug.Log("first id is" + firstid);
        line = text.text;
        string[] str = line.Split(' ');
        id = int.Parse(str[2]) - firstid;
        Debug.Log(id);
    }
    public void OnClickHostChangeButton()
    {
        HostChangeOKButton.SetActive(true);
    }

    public void OnclickHostChangeOKButton()
    {
        if (MonobitEngine.MonobitNetwork.isHost && MonobitEngine.MonobitNetwork.otherPlayersList.Length > 0)
        {
            MonobitEngine.MonobitNetwork.ChangeHost(MonobitEngine.MonobitNetwork.otherPlayersList[id]);
            Debug.Log("HereIsHostAuthority");
        }
    }

    public virtual void OnOtherPlayerDisconnected(MonobitPlayer otherPlayer)
    {
        if (id == otherPlayer.ID)
        {
            Destroy(gameObject);
        }
    }
}
