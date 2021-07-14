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
    private GameObject MuteLine;

    [SerializeField]
    private GameObject UserIcon;

    [SerializeField]
    GameObject canvas;

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

    private void Start()
    {
        
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
                RoomNameText.text = "roomName : " + roomName;
                PlayerList.text = "PlayerList : ";


                //Debug.Log("PlayerList:");
                foreach (MonobitPlayer player in MonobitNetwork.playerList)
                {
                    PlayerList.text = PlayerList.text + player.name + " ";
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

        //7/2試行部分
        // ResourcesフォルダにあるNew SpriteプレハブをGameObject型で取得
        //GameObject obj = (GameObject)Resources.Load("icon");
        // iconプレハブを元に、インスタンスを生成、
        //Instantiate(obj, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);

        //7/6試行部分
        //GameObject user_icon= new GameObject("usericon");
        //user_icon.transform.parent = GameObject.Find("Canvas").transform;
        //user_icon.AddComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        //user_icon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        //user_icon.AddComponent<Image>().sprite = Resources.Load<Sprite>("textures/ifn0438");
        //7/7 user_icon.AddComponent<Image>().sprite = Resources.Load("textures/ifn0438", typeof(Sprite)) as Sprite;
        //user_icon.GetComponent<Image>().preserveAspect = true;
        //user_icon.GetComponent<Image>().SetNativeSize();

        // 「Sprite sprite = Resources.Load("ban", typeof(Sprite)) as Sprite;」と同じ
        //Sprite sprite = Resources.Load<Sprite>("ban");

        // 空のゲームオブジェクトを生成
        //GameObject gameObj = new GameObject();
        // Imageコンポーネントをアタッチ
        //Image image = gameObj.AddComponent<Image>();
        // Resources.Load()で生成したSpriteを指定
        //image.sprite = sprite;
        // Canvasの子オブジェクトとする
        //gameObj.transform.parent = FindObjectOfType<Canvas>().transform;

        // Texture2Dでサイズを取得し、変更する
        //Texture2D texture = Resources.Load<Texture2D>("ban");
        //gameObj.GetComponent<RectTransform>().sizeDelta = new Vector2(texture.width, texture.height);

        //7/7
        GameObject prefab = (GameObject)Instantiate(UserIcon);
        prefab.transform.SetParent(canvas.transform, false);

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
