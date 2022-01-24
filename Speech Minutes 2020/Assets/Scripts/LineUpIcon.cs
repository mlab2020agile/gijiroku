using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;
using MonobitEngine.VoiceChat;
using UnityEngine.UI;
public class LineUpIcon : MonobitEngine.MonoBehaviour
{
    public List<int> IconStateList = new List<int> {0};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //リスト追加
    public void AddList()
    {
        IconStateList.Add(0);
    }
    //リスト作成
    public void CreateList(int id)
    {
        for(int i = 0; i < id; i++)
        {
            IconStateList.Add(0);
        }
    }
    //リスト変更
    public void ChangeList(int id,int state)
    {
        IconStateList[id]=state;
        monobitView.RPC("ChangeListSync", MonobitTargets.OthersBuffered,id,state);
    }
    //リストの何番目か
    public int List(int n)
    {
        int order=0;
        for (int i =0; i < MonobitNetwork.room.playerCount; i++)
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
        int icondisplaynumber=0;
        list = List(number)+1;
        for (int i = 1; i < list+1; i++)
        {
            if (IconStateList[i]==0)
            {
                icondisplaynumber++;
            }
        }
        if (IconStateList[list] == 1)
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
    }
}
