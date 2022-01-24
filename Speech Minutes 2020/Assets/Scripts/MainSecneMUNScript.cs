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
    private Text IconHideText;
    [SerializeField]
    private GameObject MuteLine;
    [SerializeField]
    private GameObject UserIcon;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    public Dropdown usernamedropdown;
    public int usercnt = 0;
    [SerializeField]
    int playerCount = 0;
    int num;
    int x = Screen.width;
    int y = Screen.height;
    public GameObject[] usericon = new GameObject[9];
    public bool Mute = true;
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
    public bool PlayerScrollState = false;
    public GameObject PlayerScroll;
    public GameObject textPrefab;
    public GameObject content1;
    public GameObject IconButton;
    public GameObject IconHideButton;
    public bool IconHideState = false;
    public GameObject IconPanel;
    public GameObject CloseButton;
    public GameObject CameraPanel;
    public RawImage rawImage1;
    public RawImage rawImage2;
    public RawImage rawImage3;
    public RawImage rawImage4;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    private Sprite sprite;
    public GameObject[] LogText;
    public GameObject[] WadaiThema;
    public List<int> IconList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
    List<int> MuteList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int Iconid;
    public string Iconname;
    public int muteid;
    public int notmuteid;
    IconCreate script;
    LineUpIcon lineupiconscript;
    int playercount = 0;

    void Start()
    {
        PlayerScroll.SetActive(false);
        IconButton.SetActive(false);
        IconPanel.SetActive(false);
        IconHideText.text = "アイコン表示中";
        IconHideButton.GetComponent<Image>().color = new Color(127 / 255f, 255 / 255f, 191 / 255f);
        CameraPanel.GetComponent<RectTransform>().SetAsLastSibling();
        //script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
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
                string s2 = rn.Substring(rn.Length - 2, 1);
                int i1 = int.Parse(s1);
                int i2 = int.Parse(s2);
                string s3 = rn.Substring(0, i2);
                string s4 = rn.Substring(i2, i1);
                RoomNameText.text = "roomName : " + s3;
                PlayerList.text = "PlayerList : ";
                InitialList.text = "";
                //Debug.Log("PlayerList:");
                foreach (MonobitPlayer player in MonobitNetwork.playerList)
                {
                    PlayerList.text = PlayerList.text + player.name + " ";
                    InitialList.text = InitialList.text + player.name.Substring(0, 1) + "     ";
                }

                if (IconHideState == true)
                {
                    monobitView.RPC("HideTrue", MonobitTargets.All, MonobitEngine.MonobitNetwork.player.ID);
                    //IconHideText.text= "アイコン非表示中";
                }
                else
                {
                    monobitView.RPC("HideFalse", MonobitTargets.All, MonobitEngine.MonobitNetwork.player.ID);
                    //IconHideText.text = "アイコン表示中";
                }
                if (playercount != MonobitNetwork.room.playerCount)
                {
                    //monobitView.RPC("IconUpdate", MonobitTargets.AllBuffered);
                    IconUpdatee();
                    Debug.Log("IconUpDate Now");
                    playercount = MonobitNetwork.room.playerCount;
                }
                if (playerCount != MonobitNetwork.room.playerCount)
                {
                    /*
                    if (MonobitNetwork.room.playerCount <= 4)
                    {
                        IconButton.SetActive(false);
                        GameObject[] icons = GameObject.FindGameObjectsWithTag("icon");
                        foreach (GameObject icon in icons)
                        {
                            Destroy(icon);
                        }
                        int iconsum = 0;
                        for (int iconnum = 0; iconnum < 8; iconnum++)
                        {
                            iconsum += IconList[iconnum];
                        }
                        int a = -1;
                        for (num = 0; num < MonobitNetwork.room.playerCount - iconsum; num++)
                        {
                            //GameObject prefab = (GameObject)Instantiate(usericon[num], new Vector3(-x / 2 + x * num / 15, -y / 6, 0), Quaternion.identity);
                            GameObject prefab = (GameObject)Instantiate(usericon[num], new Vector3(-560 + (num % 2) * 150, -150 - (num / 2) * 140, 0), Quaternion.identity);
                            prefab.transform.SetParent(canvas.transform, false);
                            a += 1;
                            while (IconList[a] == 1)
                            {
                                a += 1;
                            }
                            prefab.transform.Find("Text").GetComponent<Text>().text = MonobitNetwork.playerList[a].name;
                            prefab.transform.Find("Initial").GetComponent<Text>().text = MonobitNetwork.playerList[a].name.Substring(0, 1);
                        }
                    }
                    else
                    {
                        GameObject[] icons = GameObject.FindGameObjectsWithTag("icon");
                        foreach (GameObject icon in icons)
                        {
                            Destroy(icon);
                        }
                        IconButton.SetActive(true);
                    }
                    playerCount = MonobitNetwork.room.playerCount;
                    Debug.Log(MonobitNetwork.room.playerCount);
                    CameraPanel.transform.SetAsLastSibling();
                    */
                    monobitView.RPC("Hide", MonobitTargets.All);
                    //playerCount = MonobitNetwork.room.playerCount;
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
                    /*List<MonobitPlayer> playerList = new List<MonobitPlayer>(vcPlayerInfo.Keys);
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
                    }*/
                    // ボイスチャットの送信可否設定を反映させる
                    myVoice.SetMulticastTarget(new Int32[] { });
                    myVoice.SendStreamType = StreamType.MULTICAST;
                    monobitView.RPC("Muteon", MonobitTargets.All, MonobitEngine.MonobitNetwork.player.ID);
                }
                if (!Mute)
                {
                    myVoice.SendStreamType = StreamType.BROADCAST;
                    monobitView.RPC("Muteoff", MonobitTargets.All, MonobitEngine.MonobitNetwork.player.ID);
                }
            }
        }
    }
    public void LeaveRoom()
    {
        Debug.Log("部屋を出る");
        MonobitNetwork.LeaveRoom();
        //Debug.Log("ルームから退出しました");
        //ここでスタートのシーンに遷移する
        SceneManager.LoadScene("StartScene");
    }

    public void OnLeftRoom()
    {
        SceneManager.LoadScene("StartScene");
        Debug.Log("OnLeftRoom");
    }
    public void OnJoinedRoom()
    {
        vcPlayerInfo.Clear();
        vcPlayerInfo.Add(MonobitNetwork.player, (Int32)EnableVC.DISABLE);
        foreach (MonobitPlayer player in MonobitNetwork.otherPlayersList)
        {
            vcPlayerInfo.Add(player, (Int32)EnableVC.DISABLE);
        }
        GameObject go = MonobitNetwork.Instantiate("VoiceActor", Vector3.zero, Quaternion.identity, 0);
        myVoice = go.GetComponent<MonobitVoice>();
        Mc = go.GetComponent<MonobitMicrophone>();
        AC = Mc.GetAudioClip();
        Debug.Log(MonobitNetwork.playerName);
        Debug.Log("My ID : " + MonobitEngine.MonobitNetwork.player.ID);
        if (myVoice != null)
        {
            myVoice.SetMicrophoneErrorHandler(OnMicrophoneError);
            myVoice.SetMicrophoneRestartHandler(OnMicrophoneRestart);
        }
        if (MonobitNetwork.room.playerCount <= 4)
        {
            IconButton.SetActive(false);
        }
        else
        {
            IconButton.SetActive(true);
        }
        monobitView.RPC("Hide", MonobitTargets.All);
        GameObject prefab = MonobitEngine.MonobitNetwork.Instantiate("Canvas_usericon1", Vector3.zero, Quaternion.identity, 0);
        IconListCreate();
        IconSend();
        //monobitView.RPC("IconListCreate", MonobitTargets.AllBuffered, MonobitEngine.MonobitNetwork.player.ID);
        //monobitView.RPC("IconSend", MonobitTargets.AllBuffered, MonobitEngine.MonobitNetwork.player.ID, MonobitEngine.MonobitNetwork.player.name);
        monobitView.RPC("IconListIncrease", MonobitTargets.OthersBuffered);
        //monobitView.RPC("IconUpdate", MonobitTargets.AllBuffered);
        IconUpdatee();
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
        //rawImage1.GetComponent<WebCamController>().StandBy();
        if (!vcPlayerInfo.ContainsKey(newPlayer))
        {
            vcPlayerInfo.Add(newPlayer, (Int32)EnableVC.DISABLE);
        }
        monobitView.RPC("Cnt", MonobitTargets.All, usercnt);
        if (MonobitEngine.MonobitNetwork.isHost)
        {
            //int id =MonobitNetwork.playerList.Length-1;
            monobitView.RPC("SendAllLogData", MonobitTargets.All, LogText[0].GetComponent<Text>().text, LogText[1].GetComponent<Text>().text,
            LogText[2].GetComponent<Text>().text, LogText[3].GetComponent<Text>().text, LogText[4].GetComponent<Text>().text,
            LogText[5].GetComponent<Text>().text, LogText[6].GetComponent<Text>().text, LogText[7].GetComponent<Text>().text);
            monobitView.RPC("SendAllWadaiThema", MonobitTargets.All, WadaiThema[0].GetComponent<Text>().text, WadaiThema[1].GetComponent<Text>().text,
            WadaiThema[2].GetComponent<Text>().text, WadaiThema[3].GetComponent<Text>().text, WadaiThema[4].GetComponent<Text>().text,
            WadaiThema[5].GetComponent<Text>().text, WadaiThema[6].GetComponent<Text>().text, WadaiThema[7].GetComponent<Text>().text);
        }
    }

    [MunRPC]
    public void SendAllLogData(String LogText1, String LogText2, String LogText3, String LogText4, String LogText5, String LogText6, String LogText7, String LogText8)
    {
        LogText[0].GetComponent<Text>().text = LogText1;
        LogText[1].GetComponent<Text>().text = LogText2;
        LogText[2].GetComponent<Text>().text = LogText3;
        LogText[3].GetComponent<Text>().text = LogText4;
        LogText[4].GetComponent<Text>().text = LogText5;
        LogText[5].GetComponent<Text>().text = LogText6;
        LogText[6].GetComponent<Text>().text = LogText7;
        LogText[7].GetComponent<Text>().text = LogText8;
        Debug.Log("ThanksHost");
    }
    [MunRPC]
    public void SendAllWadaiThema(String WadaiThema1, String WadaiThema2, String WadaiThema3, String WadaiThema4, String WadaiThema5, String WadaiThema6, String WadaiThema7, String WadaiThema8)
    {
        WadaiThema[0].GetComponent<Text>().text = WadaiThema1;
        WadaiThema[1].GetComponent<Text>().text = WadaiThema2;
        WadaiThema[2].GetComponent<Text>().text = WadaiThema3;
        WadaiThema[3].GetComponent<Text>().text = WadaiThema4;
        WadaiThema[4].GetComponent<Text>().text = WadaiThema5;
        WadaiThema[5].GetComponent<Text>().text = WadaiThema6;
        WadaiThema[6].GetComponent<Text>().text = WadaiThema7;
        WadaiThema[7].GetComponent<Text>().text = WadaiThema8;
    }

    // 誰かがルームからログアウトしたときの処理
    public virtual void OnOtherPlayerDisconnected(MonobitPlayer otherPlayer)
    {
        rawImage1.transform.localPosition = new Vector3(1000, 1000, 0);
        rawImage2.transform.localPosition = new Vector3(1000, 1000, 0);
        rawImage3.transform.localPosition = new Vector3(1000, 1000, 0);
        rawImage4.transform.localPosition = new Vector3(1000, 1000, 0);

        image1.transform.localPosition = new Vector3(1000, 1000, 0);
        image2.transform.localPosition = new Vector3(1000, 1000, 0);
        image3.transform.localPosition = new Vector3(1000, 1000, 0);
        image4.transform.localPosition = new Vector3(1000, 1000, 0);

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
    public void IconButtonOnclick()
    {
        GameObject[] icons = GameObject.FindGameObjectsWithTag("icon");
        foreach (GameObject icon in icons)
        {
            Destroy(icon);
        }
        for (num = 0; num < MonobitNetwork.room.playerCount; num++)
        {
            GameObject prefab = (GameObject)Instantiate(usericon[num], new Vector3(-160 + num * 80, 200 - 150 * (num / 5), 0), Quaternion.identity);
            prefab.transform.SetParent(IconPanel.transform, false);
            prefab.transform.Find("Text").GetComponent<Text>().text = MonobitNetwork.playerList[num].name;
            prefab.transform.Find("Initial").GetComponent<Text>().text = MonobitNetwork.playerList[num].name.Substring(0, 1);
        }
        playerCount = MonobitNetwork.room.playerCount;
        Debug.Log(MonobitNetwork.room.playerCount);
        IconPanel.SetActive(true);
    }
    public void CloseButtonOnclick()
    {
        GameObject[] icons = GameObject.FindGameObjectsWithTag("icon");
        foreach (GameObject icon in icons)
        {
            Destroy(icon);
        }
        IconPanel.SetActive(false);
    }
    public void IconHideButtonOnclick()
    {
        IconHideState = !IconHideState;
        if (IconHideState)
        {
            IconHideText.text = "アイコン非表示中";
            IconHideButton.GetComponent<Image>().color = new Color(255 / 255f, 127 / 255f, 127 / 255f);
            HideOn();
            //monobitView.RPC("HideOn", MonobitTargets.AllBuffered, MonobitEngine.MonobitNetwork.player.ID);
        }
        else
        {
            IconHideText.text = "アイコン表示中";
            IconHideButton.GetComponent<Image>().color = new Color(127 / 255f, 255 / 255f, 191 / 255f);
            HideOff();
            //monobitView.RPC("HideOff", MonobitTargets.AllBuffered, MonobitEngine.MonobitNetwork.player.ID);
        }
        //monobitView.RPC("IconUpdate", MonobitTargets.AllBuffered);
        IconUpdatee();
        rawImage1.transform.localPosition = new Vector3(1000, 1000, 0);
        rawImage2.transform.localPosition = new Vector3(1000, 1000, 0);
        rawImage3.transform.localPosition = new Vector3(1000, 1000, 0);
        rawImage4.transform.localPosition = new Vector3(1000, 1000, 0);
        monobitView.RPC("Hide", MonobitTargets.All);
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
                    //ミュート設定
                    //myVoice.SendStreamType = StreamType.MULTICAST;
                    MuteLine.SetActive(true);
                    mute();
                    //monobitView.RPC("mute", MonobitTargets.AllBuffered, MonobitEngine.MonobitNetwork.player.ID);

                }
                else
                {
                    //ミュート解除
                    //myVoice.SendStreamType = StreamType.BROADCAST;
                    MuteLine.SetActive(false);
                    notmute();
                    //monobitView.RPC("notmute", MonobitTargets.AllBuffered, MonobitEngine.MonobitNetwork.player.ID);
                }
                monobitView.RPC("Hide", MonobitTargets.All);
            }
        }
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void Muteoff(int id)
    {
        MuteList[id - 1] = 1;
        /*
        if (IconList[id - 1] == 0)
        {
            int iconid = 0;
            for (int iconnum = 0; iconnum < id; iconnum++)
            {
                iconid += IconList[iconnum];
            }
            if (iconid == 0)
            {
                image1.transform.localPosition = new Vector3(-210, -110, 0);//-250,-160
                sprite = Resources.Load<Sprite>("textures/muteoff");
                image1.GetComponent<Image>().sprite = sprite;
            }
            else if (iconid == 1)
            {
                image2.transform.localPosition = new Vector3(-60, -110, 0);
                sprite = Resources.Load<Sprite>("textures/muteoff");
                image2.GetComponent<Image>().sprite = sprite;
            }
            else if (iconid == 2)
            {
                image3.transform.localPosition = new Vector3(-210, -250, 0);
                sprite = Resources.Load<Sprite>("textures/muteoff");
                image3.GetComponent<Image>().sprite = sprite;
            }
            else if (iconid == 3)
            {
                image4.transform.localPosition = new Vector3(-60, -250, 0);
                sprite = Resources.Load<Sprite>("textures/muteoff");
                image4.GetComponent<Image>().sprite = sprite;
            }
        }
        */
        /*
        if (id == MonobitNetwork.playerList[0].ID)
        {
            //image1.transform.localPosition = new Vector3(1000, 1000, 0);
            image1.transform.localPosition = new Vector3(-210, -110, 0);//-250,-160
            sprite = Resources.Load<Sprite>("textures/muteoff");
            image1.GetComponent<Image>().sprite = sprite;

        }
        else if (id == MonobitNetwork.playerList[1].ID)
        {
            //image2.transform.localPosition = new Vector3(1000, 1000, 0);
            image2.transform.localPosition = new Vector3(-60, -110, 0);
            sprite = Resources.Load<Sprite>("textures/muteoff");
            image2.GetComponent<Image>().sprite = sprite;
        }
        else if (id == MonobitNetwork.playerList[2].ID)
        {
            //image3.transform.localPosition = new Vector3(1000, 1000, 0);
            image3.transform.localPosition = new Vector3(-210, -250, 0);
            sprite = Resources.Load<Sprite>("textures/muteoff");
            image3.GetComponent<Image>().sprite = sprite;
        }
        else if (id == MonobitNetwork.playerList[3].ID)
        {
            //image4.transform.localPosition = new Vector3(1000, 1000, 0);
            image4.transform.localPosition = new Vector3(-60, -250, 0);
            sprite = Resources.Load<Sprite>("textures/muteoff");
            image4.GetComponent<Image>().sprite = sprite;
        }
        */
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void Muteon(int id)
    {
        MuteList[id - 1] = 0;
        /*
        if(IconList[id - 1] == 0)
        {
            int iconid = 0;
            for (int iconnum = 0; iconnum < id; iconnum++)
            {
                iconid += IconList[iconnum];
            }
            if (iconid==0)
            {
                image1.transform.localPosition = new Vector3(-210, -110, 0);//-250,-160
                sprite = Resources.Load<Sprite>("textures/muteon");
                image1.GetComponent<Image>().sprite = sprite;
            }
            else if (iconid == 1)
            {
                image2.transform.localPosition = new Vector3(-60, -110, 0);
                sprite = Resources.Load<Sprite>("textures/muteon");
                image2.GetComponent<Image>().sprite = sprite;
            }
            else if (iconid == 2)
            {
                image3.transform.localPosition = new Vector3(-210, -250, 0);
                sprite = Resources.Load<Sprite>("textures/muteon");
                image3.GetComponent<Image>().sprite = sprite;
            }
            else if (iconid == 3)
            {
                image4.transform.localPosition = new Vector3(-60, -250, 0);
                sprite = Resources.Load<Sprite>("textures/muteon");
                image4.GetComponent<Image>().sprite = sprite;
            }
        }
        */
        /*
        if (id == MonobitNetwork.playerList[0].ID)
        {
            image1.transform.localPosition = new Vector3(-210, -110, 0);//-250,-160
            sprite = Resources.Load<Sprite>("textures/muteon");
            image1.GetComponent<Image>().sprite = sprite;
        }
        else if (id == MonobitNetwork.playerList[1].ID)
        {
            image2.transform.localPosition = new Vector3(-60, -110, 0);
            sprite = Resources.Load<Sprite>("textures/muteon");
            image2.GetComponent<Image>().sprite = sprite;
        }
        else if (id == MonobitNetwork.playerList[2].ID)
        {
            image3.transform.localPosition = new Vector3(-210, -250, 0);
            sprite = Resources.Load<Sprite>("textures/muteon");
            image3.GetComponent<Image>().sprite = sprite;
        }
        else if (id == MonobitNetwork.playerList[3].ID)
        {
            image4.transform.localPosition = new Vector3(-60, -250, 0);
            sprite = Resources.Load<Sprite>("textures/muteon");
            image4.GetComponent<Image>().sprite = sprite;
        }
        */
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void HideTrue(int id)
    {
        IconList[id - 1] = 1;
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void HideFalse(int id)
    {
        IconList[id - 1] = 0;
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void IconSend(int id, string name)
    {
        if (MonobitEngine.MonobitNetwork.player.ID == id)
        {
            Iconid = id;
            Iconname = name;
            script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
            script.Icondicision();
            Debug.Log("Iconid:" + Iconid);
            Debug.Log("Iconname:" + Iconname);
            Iconid = 0;
            Iconname = "";
        }
    }
    public void IconSend()
    {
        Iconid = MonobitEngine.MonobitNetwork.player.ID;
        Iconname = MonobitEngine.MonobitNetwork.player.name;
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.Icondicision();
        Debug.Log("Iconid:" + Iconid);
        Debug.Log("Iconname:" + Iconname);
        Iconid = 0;
        Iconname = "";
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void IconListIncrease()
    {
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.AddList();
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void IconListCreate(int id)
    {
        if (MonobitEngine.MonobitNetwork.player.ID == id)
        {
            script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
            script.CreateList(id);
        }
    }
    public void IconListCreate()
    {
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.CreateList(MonobitEngine.MonobitNetwork.player.ID);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void IconUpdate()
    {
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.IconPositionUpdate();
    }
    public void IconUpdatee()
    {
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.IconPositionUpdate();
        monobitView.RPC("IconUpdate", MonobitTargets.OthersBuffered);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void HideOn(int id)
    {
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.ChangeList(id,1);
    }
    public void HideOn()
    {
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.ChangeList(MonobitEngine.MonobitNetwork.player.ID, 1);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void HideOff(int id)
    {
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.ChangeList(id, 0);
    }
    public void HideOff()
    {
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.ChangeList(MonobitEngine.MonobitNetwork.player.ID, 0);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void mute(int id)
    {
        if (MonobitEngine.MonobitNetwork.player.ID == id)
        {
            muteid = id;
            script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
            script.MuteSituation();
            muteid = 0;
        }  
    }
    public void mute()
    {
        muteid = MonobitEngine.MonobitNetwork.player.ID;
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.MuteSituation();
        muteid = 0;
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void notmute(int id)
    {
        if (MonobitEngine.MonobitNetwork.player.ID == id)
        {
            notmuteid = id;
            script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
            script.NotMuteSituation();
            notmuteid = 0;
        }
    }
    public void notmute()
    {
        notmuteid = MonobitEngine.MonobitNetwork.player.ID;
        script = GameObject.Find("UserIcon").GetComponent<IconCreate>();
        script.NotMuteSituation();
        notmuteid = 0;
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void Hide()
    {
        if (MonobitNetwork.room.playerCount <= 4)
        {
            IconButton.SetActive(false);
            GameObject[] icons = GameObject.FindGameObjectsWithTag("icon");
            foreach (GameObject icon in icons)
            {
                Destroy(icon);
            }
            int iconsum = 0;
            for (int iconnum = 0; iconnum < 8; iconnum++)
            {
                iconsum += IconList[iconnum];
            }
            int a = -1;
            for (num = 0; num < MonobitNetwork.room.playerCount - iconsum; num++)
            {
                //GameObject prefab = (GameObject)Instantiate(usericon[num], new Vector3(-x / 2 + x * num / 15, -y / 6, 0), Quaternion.identity);
                GameObject prefab = (GameObject)Instantiate(usericon[num], new Vector3(-560 + (num % 2) * 150, -150 - (num / 2) * 140, 0), Quaternion.identity);
                prefab.transform.SetParent(canvas.transform, false);
                a += 1;
                while (IconList[a] == 1)
                {
                    a += 1;
                }
                prefab.transform.Find("Text").GetComponent<Text>().text = MonobitNetwork.playerList[a].name;
                prefab.transform.Find("Initial").GetComponent<Text>().text = MonobitNetwork.playerList[a].name.Substring(0, 1);
            }
        }
        else
        {
            GameObject[] icons = GameObject.FindGameObjectsWithTag("icon");
            foreach (GameObject icon in icons)
            {
                Destroy(icon);
            }
            IconButton.SetActive(true);
        }
        CameraPanel.GetComponent<RectTransform>().SetAsLastSibling();
        int iconhide = 0;//非表示にしている人数
        for (int iconnum = 0; iconnum < 8; iconnum++)
        {
            iconhide += IconList[iconnum];
        }
        int icondisplay = MonobitNetwork.room.playerCount - iconhide;
        int icondisplay_1 = 0;
        image1.transform.localPosition = new Vector3(1000, 1000, 0);
        image2.transform.localPosition = new Vector3(1000, 1000, 0);
        image3.transform.localPosition = new Vector3(1000, 1000, 0);
        image4.transform.localPosition = new Vector3(1000, 1000, 0);
        if (icondisplay > 0 && icondisplay < 5)
        {
            while (IconList[icondisplay_1] == 1)
            {
                icondisplay_1 += 1;
            }
            image1.transform.localPosition = new Vector3(-210, -110, 0);//-250,-160
            if (MuteList[icondisplay_1] == 1)
            {
                sprite = Resources.Load<Sprite>("textures/muteoff");
            }
            else
            {
                sprite = Resources.Load<Sprite>("textures/muteon");
            }
            image1.GetComponent<Image>().sprite = sprite;
            icondisplay_1 += 1;

            if (icondisplay > 1)
            {
                while (IconList[icondisplay_1] == 1)
                {
                    icondisplay_1 += 1;
                }
                image2.transform.localPosition = new Vector3(-60, -110, 0);
                if (MuteList[icondisplay_1] == 1)
                {
                    sprite = Resources.Load<Sprite>("textures/muteoff");
                }
                else
                {
                    sprite = Resources.Load<Sprite>("textures/muteon");
                }
                image2.GetComponent<Image>().sprite = sprite;
                icondisplay_1 += 1;

                if (icondisplay > 2)
                {
                    while (IconList[icondisplay_1] == 1)
                    {
                        icondisplay_1 += 1;
                    }
                    image3.transform.localPosition = new Vector3(-210, -250, 0);
                    if (MuteList[icondisplay_1] == 1)
                    {
                        sprite = Resources.Load<Sprite>("textures/muteoff");
                    }
                    else
                    {
                        sprite = Resources.Load<Sprite>("textures/muteon");
                    }
                    image3.GetComponent<Image>().sprite = sprite;
                    icondisplay_1 += 1;

                    if (icondisplay > 3)
                    {
                        while (IconList[icondisplay_1] == 1)
                        {
                            icondisplay_1 += 1;
                        }
                        image4.transform.localPosition = new Vector3(-60, -250, 0);
                        if (MuteList[icondisplay_1] == 1)
                        {
                            sprite = Resources.Load<Sprite>("textures/muteoff");
                        }
                        else
                        {
                            sprite = Resources.Load<Sprite>("textures/muteon");
                        }
                        image4.GetComponent<Image>().sprite = sprite;
                    }
                }
            }
        }
        int hide = 0;
        for (int number = 0; number < MonobitNetwork.room.playerCount; number++)
        {
            hide += IconList[MonobitNetwork.playerList[number].ID - 1];
        }
        if (MonobitNetwork.room.playerCount - hide == 0)
        {
            rawImage1.transform.localPosition = new Vector3(1000, 1000, 0);
        }
        if (MonobitNetwork.room.playerCount - hide == 1)
        {
            rawImage2.transform.localPosition = new Vector3(1000, 1000, 0);
        }
        if (MonobitNetwork.room.playerCount - hide == 2)
        {
            rawImage3.transform.localPosition = new Vector3(1000, 1000, 0);
        }
        if (MonobitNetwork.room.playerCount - hide == 3)
        {
            rawImage4.transform.localPosition = new Vector3(1000, 1000, 0);
        }
    }
}