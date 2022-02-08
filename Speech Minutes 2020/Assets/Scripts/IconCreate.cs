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
    public string UserName;
    public int MuteID;
    public int NotMuteID;
    MainSecneMUNScript script;
    private Sprite sprite;
    LineUpIcon lineupiconscript;
    public List<int> IconStateList = new List<int> { 0,0,0,0,0,0,0,0,0,0 };

    int width = 80;
    int height = 60;
    int fps = 30;
    int s = 1;
    Texture2D texture;
    WebCamTexture webcamTexture;
    Color32[] colors = null;
    Color32[] color = null;
    public GameObject Panel;
    public RawImage rawImage;
    public bool videoswitch = false;

    //非同期処理でカラー32型の変数と２Dテクスチャを設定
    IEnumerator Init()
    {
        while (true)
        {
            if (webcamTexture.width / 2 > 16 && webcamTexture.height / 2 > 16)
            {
                colors = new Color32[webcamTexture.width * webcamTexture.height];
                //縦横それぞれwebカメラで映したピクセル数の1/8のピクセル数にするため/8をしている
                color = new Color32[webcamTexture.width / 8 * webcamTexture.height / 8];
                texture = new Texture2D(webcamTexture.width / 8, webcamTexture.height / 8, TextureFormat.RGBA32, false);
                rawImage.GetComponent<RawImage>().texture = texture;
                break;
            }
            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, 80, 60, this.fps);
        webcamTexture.Play();
        StartCoroutine(Init());
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (videoswitch)
        {
            if (colors != null)
            {
                //50フレームに一度更新
                if (s % 50 == 0)
                {
                    var cc = webcamTexture.GetPixels32(colors);
                    int width = webcamTexture.width;
                    int height = webcamTexture.height;
                    Color32 rc = new Color32(0, 0, 0, byte.MaxValue);
                    for (int x = 0; x < width; x += 8)
                    {
                        for (int y = 0; y < height; y += 8)
                        {
                            Color32 c = colors[x + y * width];
                            Video(x, y, c.r, c.g, c.b, c.a, MonobitEngine.MonobitNetwork.player.ID);
                        }
                    }
                }
                s += 1;
            }
        }
    }
    //ユーザーアイコンの中身作成
    public void Icondicision()
    {
        script = GameObject.Find("MUN").GetComponent<MainSecneMUNScript>();
        UserID = script.Iconid;
        //ユーザーアイコンのテクスチャは9通り
        switch (UserID%9)
        {
            case 0:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon0");
                break;
            case 1:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon1");
                break;
            case 2:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon2");
                break;
            case 3:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon3");
                break;
            case 4:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon4");
                break;
            case 5:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon5");
                break;
            case 6:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon6");
                break;
            case 7:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon7");
                break;
            case 8:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon8");
                break;
            default:
                break;
        }
        UserName = script.Iconname;
        IconName.GetComponent<Text>().text = UserName;
        IconInitial.GetComponent<Text>().text = UserName.Substring(0, 1);
        //変更内容を他のユーザーに共有
        monobitView.RPC("IconSync", MonobitTargets.OthersBuffered, UserID, UserName);
    }
    //ユーザーアイコン位置更新
    public void IconPositionUpdate()
    {
        //ユーザーアイコンを4つまで並べる
        switch (IconOrder(MonobitEngine.MonobitNetwork.player.ID))
        {
            case 1:
                UserIcon.transform.localPosition = new Vector3(-550, -100, 0);
                break;
            case 2:
                UserIcon.transform.localPosition = new Vector3(-400, -100, 0);
                break;
            case 3:
                UserIcon.transform.localPosition = new Vector3(-550, -235, 0);
                break;
            case 4:
                UserIcon.transform.localPosition = new Vector3(-400, -235, 0);
                break;
            default:
                UserIcon.transform.localPosition = new Vector3(1000, 1000, 0);
                break;
        }
        //変更内容を他のユーザーに共有
        monobitView.RPC("IconPositionSync", MonobitTargets.OthersBuffered, IconOrder(MonobitEngine.MonobitNetwork.player.ID));
    }
    //ミュートアイコンに変更
    public void MuteSituation()
    {
        script = GameObject.Find("MUN").GetComponent<MainSecneMUNScript>();
        MuteID = script.muteid;
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteon");
        //変更内容を他のユーザーに共有
        monobitView.RPC("MuteIconSync", MonobitTargets.OthersBuffered, MuteID);
    }
    //ミュート解除アイコンに変更
    public void NotMuteSituation()
    {
        script = GameObject.Find("MUN").GetComponent<MainSecneMUNScript>();
        NotMuteID = script.notmuteid;
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteoff");
        //変更内容を他のユーザーに共有
        monobitView.RPC("NotMuteIconSync", MonobitTargets.OthersBuffered, NotMuteID);
    }
    //カメラパネルを表示
    public void CameraOn()
    {
        Panel.SetActive(true);
        videoswitch = true;
        //変更内容を他のユーザーに共有
        monobitView.RPC("CameraOnSync", MonobitTargets.OthersBuffered);
    }
    //カメラパネルを非表示
    public void CameraOff()
    {
        Panel.SetActive(false);
        videoswitch = false;
        //変更内容を他のユーザーに共有
        monobitView.RPC("CameraOffSync", MonobitTargets.OthersBuffered);
    }
    //カメラ画像をテクスチャにセット
    public void Video(int x, int y, Byte r, Byte g, Byte b, Byte a,int id)
    {
        try
        {
            if(UserID == id)
            {
                Color32 ccc = new Color32(r, g, b, 255);
                color[x / 8 + y / 8 * width] = ccc;
                //範囲内に存在するか
                if (x / 8 >= width - 1 && y / 8 >= height - 1)
                {
                    texture.SetPixels32(color);
                    texture.Apply();
                }
            }
        }
        catch (NullReferenceException)
        {
        }
        //変更内容を他のユーザーに共有
        monobitView.RPC("VideoSync", MonobitTargets.OthersBuffered, x, y, r, g, b, a,id);

    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //ユーザーアイコンの中身を同期
    public void IconSync(int id, string name)
    {
        UserID = id;
        //ユーザーアイコンのテクスチャは9通り
        switch (UserID % 9)
        {
            case 0:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon0");
                break;
            case 1:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon1");
                break;
            case 2:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon2");
                break;
            case 3:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon3");
                break;
            case 4:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon4");
                break;
            case 5:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon5");
                break;
            case 6:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon6");
                break;
            case 7:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon7");
                break;
            case 8:
                IconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/Icon8");
                break;
            default:
                break;
        }
        UserName = name;
        IconName.GetComponent<Text>().text = UserName;
        IconInitial.GetComponent<Text>().text = UserName.Substring(0, 1);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //ミュートアイコン同期
    public void MuteIconSync(int id)
    {
        MuteID = id;
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteon");
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //ミュート解除アイコン同期
    public void NotMuteIconSync(int id)
    {
        NotMuteID = id;
        MuteImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/muteoff");
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //カメラパネル表示の同期
    public void CameraOnSync()
    {
        Panel.SetActive(true);
        videoswitch = true;
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //カメラパネル非表示の同期
    public void CameraOffSync()
    {
        Panel.SetActive(false);
        videoswitch = false;
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //ユーザーアイコン位置同期
    public void IconPositionSync(int number)
    {
        //ユーザーアイコンを4つまで並べる
        switch (number)
        {
            case 1:
                UserIcon.transform.localPosition = new Vector3(-550, -100, 0);
                break;
            case 2:
                UserIcon.transform.localPosition = new Vector3(-400, -100, 0);
                break;
            case 3:
                UserIcon.transform.localPosition = new Vector3(-550, -235, 0);
                break;
            case 4:
                UserIcon.transform.localPosition = new Vector3(-400, -235, 0);
                break;
            default:
                UserIcon.transform.localPosition = new Vector3(1000, 1000, 0);
                break;
        }
    }
    //アイコン状態(表示or非表示)のリスト追加
    public void AddList()
    {
        //アイコン状態(表示or非表示)のリストの末尾に０という要素を追加
        IconStateList.Add(0);
    }
    //アイコン状態(表示or非表示)のリスト作成
    public void CreateList(int id)
    {
        for (int i = 0; i < id; i++)
        {
            //アイコン状態(表示or非表示)のリストの末尾に０という要素を追加
            IconStateList.Add(0);
        }
    }
    //アイコン状態(表示or非表示)のリスト変更
    public void ChangeList(int id, int state)
    {
        IconStateList[id] = state;
        //変更内容を他のユーザーに共有
        monobitView.RPC("ChangeListSync", MonobitTargets.OthersBuffered, id, state);
    }
    //ユーザーリストの何番目か
    public int List(int n)
    {
        int order = 0;
        for (int i = 0; i < MonobitNetwork.room.playerCount; i++)
        {
            //プレイヤーリストのi番目のIDと与えられたIDが一致するか
            if (n == MonobitNetwork.playerList[i].ID)
            {
                order = i;
            }
        }
        return order;
    }
    //ユーザーアイコンが画面の何番目に表示されるか
    public int IconOrder(int number)
    {
        int list;
        int icondisplaynumber = 0;
        list = List(number);
        for (int i = 0; i < list + 1; i++)
        {
            //自分を含めアイコンを表示しているユーザーを数える
            if (IconStateList[MonobitNetwork.playerList[i].ID] == 0)
            {
                icondisplaynumber++;
                Debug.Log("icondisplaynumber:" + icondisplaynumber);
            }
        }
        //アイコン非表示の場合
        if (IconStateList[MonobitNetwork.playerList[list].ID] == 1)
        {
            return 0;
        }
        else//アイコン表示の場合
        {
            return icondisplaynumber;
        }
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //アイコン状態(表示or非表示)のリスト変更を同期
    public void ChangeListSync(int id, int state)
    {
        IconStateList[id] = state;
        Debug.Log("changelistsync:" + id + "," + IconStateList[id]);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    //カメラ画像をテクスチャにセット
    public void VideoSync(int x, int y, Byte r, Byte g, Byte b, Byte a,int id)
    {
        if(UserID == id)
        {
            try
            {
                Color32 ccc = new Color32(r, g, b, 255);
                color[x / 8 + y / 8 * width] = ccc;
                //範囲内に存在するか
                if (x / 8 >= width - 1 && y / 8 >= height - 1)
                {
                    texture.SetPixels32(color);
                    texture.Apply();
                }
            }
            catch (NullReferenceException)
            {
            }
        }
    }
}
