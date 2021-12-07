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
        for (int i = 0; i < MonobitEngine.MonobitNetwork.otherPlayersList.Length; i++)
        {
            if (id == MonobitEngine.MonobitNetwork.otherPlayersList[i].ID)
            {
                MonobitEngine.MonobitNetwork.Kick(MonobitEngine.MonobitNetwork.otherPlayersList[i]);
                Destroy(this.gameObject);
            }
        }

    }

    private void OnOtherPlayerDisconnected(MonobitPlayer otherPlayer)
    {
        StartCoroutine(delaydestroy(otherPlayer.ID));
    }

    IEnumerator delaydestroy(int ID)
    {
        // 1フレーム待たないと完全に実行されない
        yield return new WaitForEndOfFrame();
        if (id == ID)
        {
            Destroy(this.gameObject);
        }
    }
}