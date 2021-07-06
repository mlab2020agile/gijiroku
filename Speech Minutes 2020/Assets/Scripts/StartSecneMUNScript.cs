using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;

public class StartSecneMUNScript : MonobitEngine.MonoBehaviour
{
    /** ルーム名. */
    private string roomName = "";

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
                // ルーム名の入力
                GUILayout.Space(370);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Space (300);
                    GUILayout.Label("部屋名: ", GUILayout.Width(150));
                    GUI.skin.label.fontSize = 30;
                    GUI.skin.textField.fontSize = 30;
                    roomName = GUILayout.TextField(roomName,GUILayout.Height(45),GUILayout.Width(600));
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                // ルームを作成して入室する
                GUILayout.BeginHorizontal();
                GUILayout.Space(455);
                if (GUILayout.Button("部屋を作成",GUILayout.Height(45),GUILayout.Width(600)))
                {
                    MonobitNetwork.CreateRoom(roomName);
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

                    // ルームを選択して入室する
                    if (GUILayout.Button("部屋に入室 : " + roomParam))
                    {
                        MonobitNetwork.JoinRoom(room.name);
                        /********
                        ここでメインのシーンに遷移する
                        *********/
                        SceneManager.LoadScene("newUI");
                    }
                }
            }

        }
        //していない場合
        else
        {
            // プレイヤー名の入力
            GUILayout.Space(370);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(270);
                GUILayout.Label("名前入力 : ", GUILayout.Width(180));
                GUI.skin.label.fontSize = 30;
                GUI.skin.textField.fontSize = 30;
                GUI.skin.button.fontSize = 30;
                MonobitNetwork.playerName = GUILayout.TextField(
                    (MonobitNetwork.playerName == null) ?
                        "" :
                        MonobitNetwork.playerName,GUILayout.Height(45), GUILayout.Width(600));
            }
            GUILayout.EndHorizontal();

            // デフォルトロビーへの自動入室を許可する
            MonobitNetwork.autoJoinLobby = true;

            // MUNサーバに接続する
            GUILayout.BeginHorizontal();
            GUILayout.Space(455);
            if (GUILayout.Button("サーバに接続",GUILayout.Height(45), GUILayout.Width(600)))
            {
                MonobitNetwork.ConnectServer("SimpleChat_v1.0");
                Debug.Log("サーバに接続しました");
            }
            GUILayout.EndHorizontal();

        }



    }
}
