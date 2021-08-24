using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;
using MonobitEngine.VoiceChat;
using UnityEngine.UI;
public class MainSecneMUNScript : MonobitEngine.MonoBehaviour
{
    [SerializeField]
    private Text RoomNameText;
    [SerializeField]
    private Text PlayerList;
    [SerializeField]
    private Text InitialList;
    [SerializeField]
    private GameObject MuteLine;
    [SerializeField]
    private GameObject UserIcon;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    public Dropdown usernamedropdown;
    [SerializeField]
    int playerCount = 0;
    int num;
    int x = Screen.width;
    int y = Screen.height;
    public GameObject[] usericon = new GameObject[9];
    public bool Mute = false;
    /** ルーム名. */
    private string roomName = "";
    /** ルーム内のプレイヤーに対するボイスチャット送信可否設定. */
    private Dictionary<MonobitPlayer, Int32> vcPlayerInfo = new Dictionary<MonobitPlayer, int>();
    /** 自身が所有するボイスアクターのMonobitViewコンポーネント. */
    private MonobitVoice myVoice = null;
    private bool first = true;
    private MonobitMicrophone Mc = null;
    public AudioClip AC;
    public GameObject PlayerListButton;
    public bool PlayerScrollState=false;
    public GameObject PlayerScroll;
    public GameObject textPrefab;
    public GameObject content1;
    void Start()
    {
        PlayerScroll.SetActive(false);
    }
    /** ボイスチャット送信可否設定の定数. */
    private enum EnableVC
    {
        ENABLE = 0,         /**< 有効. */
        DISABLE = 1,        /**< 無効. */
    }
    /** チャット発言ログ. */
    List<string> chatLog = new List<string>();
    /**
    * RPC 受信関数.
    */
    [MunRPC]
    void RecvChat(string senderName, string senderWord)
    {
        chatLog.Add(senderName + " : " + senderWord);
        if (chatLog.Count > 10)
        {
            chatLog.RemoveAt(0);
        }
    }
    private void Update()
    {
        //MUNサーバに接続している場合
        if (MonobitNetwork.isConnect)
        {
            // ルームに入室している場合
            if (MonobitNetwork.inRoom)
            {
                roomName = MonobitEngine.MonobitNetwork.room.name;
                string rn = roomName;
                string s1 = rn.Substring(rn.Length - 1);
                string s2 = rn.Substring(rn.Length - 2,1);
                int i1 = int.Parse(s1);
                int i2 = int.Parse(s2);
                string s3 = rn.Substring(0,i2);
                string s4 = rn.Substring(i2,i1);
                RoomNameText.text = "roomName : " + s3;
                PlayerList.text = "PlayerList : ";
                InitialList.text = "";
                //Debug.Log("PlayerList:");
                foreach (MonobitPlayer player in MonobitNetwork.playerList)
                {
                    PlayerList.text = PlayerList.text + player.name + " ";
                    InitialList.text = InitialList.text + player.name.Substring(0,1)+"     ";
                }
                if (playerCount != MonobitNetwork.room.playerCount)
                {
                    GameObject[] icons = GameObject.FindGameObjectsWithTag("icon");
                    foreach (GameObject icon in icons)
                    {
                        Destroy(icon);
                    }
                    for (num = 0; num < MonobitNetwork.room.playerCount; num++)
                    {
                        GameObject prefab = (GameObject)Instantiate(usericon[num], new Vector3(-x/2+x * num / 15, -y / 6, 0), Quaternion.identity);
                        prefab.transform.SetParent(canvas.transform, false);
                        prefab.transform.Find("Text").GetComponent<Text>().text = MonobitNetwork.playerList[num].name;
                    }
                    playerCount = MonobitNetwork.room.playerCount;
                    Debug.Log(MonobitNetwork.room.playerCount);
                }
                if (usernamedropdown)
                {
                    usernamedropdown.options.Clear();//現在の要素をクリアする
                    usernamedropdown.options.Add(new Dropdown.OptionData("PlayerList"));//新しく要素を追加する
                    for (num = 0; num < MonobitNetwork.playerList.Length; num++)
                    {
                        usernamedropdown.options.Add(new Dropdown.OptionData(MonobitNetwork.playerList[num].name));
                    }
                    usernamedropdown.value = 0;//デフォルトを「PlayerList」に設定
                }
                if (Mute)
                {
                    List<MonobitPlayer> playerList = new List<MonobitPlayer>(vcPlayerInfo.Keys);
                    List<MonobitPlayer> vcTargets = new List<MonobitPlayer>();
                    foreach (MonobitPlayer player in playerList)
                    {
                        vcPlayerInfo[player] = (Int32)EnableVC.DISABLE;
                        Debug.Log("vcPlayerInfo[" + player + "] = " + vcPlayerInfo[player]);
                        // ボイスチャットの送信可のプレイヤー情報を登録する
                        if (vcPlayerInfo[player] == (Int32)EnableVC.ENABLE)
                        {
                            vcTargets.Add(player);
                        }
                    }
                    // ボイスチャットの送信可否設定を反映させる
                    myVoice.SetMulticastTarget(vcTargets.ToArray());
                }
            }
        }
    }
    public void LeaveRoom()
    {
        MonobitNetwork.LeaveRoom();
        //Debug.Log("ルームから退出しました");
        //ここでスタートのシーンに遷移する
        SceneManager.LoadScene("koba_StartScene");
    }
    // 自身がルーム入室に成功したときの処理
    public void OnJoinedRoom()
    {
        vcPlayerInfo.Clear();
        vcPlayerInfo.Add(MonobitNetwork.player, (Int32)EnableVC.DISABLE);
        foreach (MonobitPlayer player in MonobitNetwork.otherPlayersList)
        {
            vcPlayerInfo.Add(player, (Int32)EnableVC.ENABLE);
        }
        GameObject go = MonobitNetwork.Instantiate("VoiceActor", Vector3.zero, Quaternion.identity, 0);
        myVoice = go.GetComponent<MonobitVoice>();
        Mc = go.GetComponent<MonobitMicrophone>();
        AC = Mc.GetAudioClip();
        Debug.Log(MonobitNetwork.playerName);
        if (myVoice != null)
        {
            myVoice.SetMicrophoneErrorHandler(OnMicrophoneError);
            myVoice.SetMicrophoneRestartHandler(OnMicrophoneRestart);
        }
    }
    public void DebugButton()
    {
        Debug.Log("myVoice = " + myVoice);
        Debug.Log("Mc = " + Mc);
        Debug.Log("");
    }
    // 誰かがルームにログインしたときの処理
    public void OnOtherPlayerConnected(MonobitPlayer newPlayer)
    {
        if (!vcPlayerInfo.ContainsKey(newPlayer))
        {
            vcPlayerInfo.Add(newPlayer, (Int32)EnableVC.ENABLE);
        }
    }
    // 誰かがルームからログアウトしたときの処理
    public virtual void OnOtherPlayerDisconnected(MonobitPlayer otherPlayer)
    {
        if (vcPlayerInfo.ContainsKey(otherPlayer))
        {
            vcPlayerInfo.Remove(otherPlayer);
        }
    }
     public void ListButtonOnclick()
    {
                    if (PlayerScrollState == false)
                    {
                        foreach (MonobitPlayer player in MonobitNetwork.playerList)
                        {
                            GameObject _text = Instantiate(textPrefab, content1.transform);
                            _text.GetComponent<Text>().text = player.name;
                        }
                        PlayerScrollState = !PlayerScrollState;
                        PlayerScroll.SetActive(true);
                    }
                    else
                    {
                        foreach (Transform child in content1.transform)
                        {
                            // 一つずつ破棄する
                            Destroy(child.gameObject);
                        }
                        PlayerScrollState = !PlayerScrollState;
                        PlayerScroll.SetActive(false);
                    }
     }
    /// <summary>
    /// マイクのエラーハンドリング用デリゲート
    /// </summary>
    /// <returns>
    /// true : 内部にてStopCaptureを実行しループを抜けます。
    /// false: StopCaptureを実行せずにループを抜けます。
    /// </returns>
    public bool OnMicrophoneError()
    {
        UnityEngine.Debug.LogError("Error: Microphone Error !!!");
        return true;
    }
    /// <summary>
    /// マイクのリスタート用デリゲート
    /// </summary>
    /// <remarks>
    /// 呼び出された時点ではすでにStopCaptureされています。
    /// </remarks>
    public void OnMicrophoneRestart()
    {
        UnityEngine.Debug.LogWarning("Info: Microphone Restart !!!");
    }
    public void muteButtonOnclicked()
    {
        //MUNサーバに接続している場合
        if (MonobitNetwork.isConnect)
        {
            // ルームに入室している場合
            if (MonobitNetwork.inRoom)
            {
                Mute = !Mute;
                if (Mute)
                {
                    myVoice.SendStreamType = StreamType.MULTICAST;
                    MuteLine.SetActive(true);
                }
                else
                {
                    myVoice.SendStreamType = StreamType.BROADCAST;
                    MuteLine.SetActive(false);
                }
            }
        }
    }
}