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

    private int Myid;

    private int Otherid;

    private int Othernumber;
    private void Start()
    {
        line = text.text;
        string[] str = line.Split(' ');
        id = int.Parse(str[3]);
        Debug.Log(id);
    }
    public void OnClickHostChangeButton()
    {
        HostChangeOKButton.SetActive(true);
    }

    public void OnclickHostChangeOKButton()
    {
        for (int i = 0; i < MonobitEngine.MonobitNetwork.otherPlayersList.Length; i++)
        {
            if (id == MonobitEngine.MonobitNetwork.otherPlayersList[i].ID)
            {
                if (MonobitEngine.MonobitNetwork.isHost && MonobitEngine.MonobitNetwork.otherPlayersList.Length > 0)
                {
                    MonobitEngine.MonobitNetwork.ChangeHost(MonobitEngine.MonobitNetwork.otherPlayersList[i]);
                    Debug.Log("HereIsHostAuthority");
                }
            }
        }
        /*
                for (int i = 0; i < MonobitEngine.MonobitNetwork.playerList.Length; i++)
                {
                    if (MonobitNetwork.playerList[i].ID == id)
                    {
                        Otherid = i;
                        Debug.Log(Otherid);
                    }
                    if (MonobitNetwork.playerList[i].ID == id)
                    {
                        Myid = i;
                    }
                }
                if (Myid > Otherid)
                {
                    Othernumber = Otherid;
                }
                else
                {
                    Othernumber = Otherid - 1;
                }

              */
    }

    private void OnOtherPlayerDisconnected(MonobitPlayer otherPlayer)
    {
        StartCoroutine(delaydestroy(otherPlayer.ID));
    }

    IEnumerator delaydestroy(int ID)
    {
        // 1フレーム待たないと完全に実行されない
        yield return new WaitForEndOfFrame();
        // 1フレーム待たないと完全に実行されない
        yield return new WaitForEndOfFrame();
        if (id == ID)
        {
            Destroy(this.gameObject);
        }
    }
}
