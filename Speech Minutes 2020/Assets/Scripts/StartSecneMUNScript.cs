using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;

public class StartSecneMUNScript : MonobitEngine.MonoBehaviour
{
    /** ルーム名. */
    private string roomName = "";
    private string roomPasword = "";
    private string roomUnrock = "";

    // カスタムパラメータリスト
    private Hashtable customRoomParam = new Hashtable();

    private void OnGUI()
    {
        //MUNサーバに接続している場合
        if (MonobitNetwork.isConnect)
        {
            //Debug.Log("サーバに接続しました");
            // ルームに入室している場合
            if (MonobitNetwork.inRoom)
            {
                
            }
            // ルームに入室していない場合
            else
            {
                int x = Screen.width;
                int y = Screen.height;
                // ルーム名の入力
                //UIの位置を中心に
                GUILayout.Space(y/2.3f);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space (x/4);
                    GUILayout.Label("部屋名: ", GUILayout.Width(x/8));
                    GUI.skin.label.fontSize = x/50;
                    GUI.skin.textField.fontSize = x/50;
                    GUI.skin.button.fontSize = x/50;
                    roomName = GUILayout.TextField(roomName,GUILayout.Height(x/40),GUILayout.Width(x/3));
                }
                GUILayout.EndHorizontal();
                //パスワード欄
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space (x/4);
                    GUILayout.Label("パスワード作成: ",GUILayout.Height(x/40),GUILayout.Width(x/8));
                    this.customRoomParam["Password"] = GUILayout.TextField((this.customRoomParam.ContainsKey("Password")) ? 
                        this.customRoomParam["Password"].ToString() : "", GUILayout.Height(x/40),GUILayout.Width(x/3));
                }
                GUILayout.EndHorizontal();
                // ルームを作成して入室する
                GUILayout.BeginHorizontal();
                GUILayout.Space(x*3/8);
                if (GUILayout.Button("部屋を作成",GUILayout.Height(x/35),GUILayout.Width(x/3)))
                {
                    // カスタムルームパラメータの設定
                    MonobitEngine.RoomSettings roomSettings = new MonobitEngine.RoomSettings()
                    {
                        isVisible = true,
                        isOpen = true,
                        roomParameters = customRoomParam,
                        lobbyParameters = new string[] { }
                    };
                    MonobitNetwork.CreateRoom(roomName,roomSettings,null);
                    Debug.Log("ルームを作成しました");
                    
                    /********
                    ここでメインのシーンに遷移する
                    *********/
                    SceneManager.LoadScene("newUI");
                }
                GUILayout.EndHorizontal();
                // ルーム一覧を検索
                foreach (RoomData room in MonobitNetwork.GetRoomData())
                {
                    // ルームパラメータの可視化
                    System.String roomParam =
                        System.String.Format(
                            "{0}({1}/{2})",
                            room.name,
                            room.playerCount,
                            ((room.maxPlayers == 0) ? "-" : room.maxPlayers.ToString())
                        );
                    //ルームパスワードと結びつけ
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Space (x/4);
                        GUILayout.Label("のパスワード解除: ",GUILayout.Height(x/40),GUILayout.Width(x/8));
                        roomUnrock = GUILayout.TextField(roomUnrock,GUILayout.Height(x/40),GUILayout.Width(x/3));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(x*3/8);
                    // ルームを選択して入室する
                    if (GUILayout.Button("部屋に入室 : " + roomParam))
                    {
                        if (customRoomParam["Password"].Equals(roomUnrock))
                        {
                            MonobitNetwork.JoinRoom(room.name);
                            /********
                            ここでメインのシーンに遷移する
                            *********/
                            SceneManager.LoadScene("newUI");
                        }
                        
                    }
                    GUILayout.EndHorizontal();
                }
            }

        }
        //していない場合
        else
        {
            int x = Screen.width;
            int y = Screen.height;
            // プレイヤー名の入力
            GUILayout.Space(y/2.3f);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(x/4);
                GUILayout.Label("名前入力 : ", GUILayout.Width(x/8));
                GUI.skin.label.fontSize = x/50;
                GUI.skin.textField.fontSize = x/50;
                GUI.skin.button.fontSize = x/50;
                MonobitNetwork.playerName = GUILayout.TextField(
                    (MonobitNetwork.playerName == null) ?
                        "" :
                        MonobitNetwork.playerName,GUILayout.Height(x/40), GUILayout.Width(x/3));
            }
            GUILayout.EndHorizontal();

            // デフォルトロビーへの自動入室を許可する
            MonobitNetwork.autoJoinLobby = true;

            // MUNサーバに接続する
            GUILayout.BeginHorizontal();
            GUILayout.Space(x*3/8);
            if (GUILayout.Button("サーバに接続",GUILayout.Height(x/35), GUILayout.Width(x/3)))
            {
                MonobitNetwork.ConnectServer("SimpleChat_v1.0");
                Debug.Log("サーバに接続しました");
            }
            GUILayout.EndHorizontal();

        }



    }
}
