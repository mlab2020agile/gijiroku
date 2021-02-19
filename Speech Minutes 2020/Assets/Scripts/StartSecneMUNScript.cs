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
                GUILayout.BeginHorizontal();
                GUILayout.Label("RoomName : ");
                roomName = GUILayout.TextField(roomName, GUILayout.Width(200));
                GUILayout.EndHorizontal();
                // ルームを作成して入室する
                if (GUILayout.Button("Create Room", GUILayout.Width(150)))
                {
                    MonobitNetwork.CreateRoom(roomName);
                    Debug.Log("ルームを作成しました");
                    
                    /********
                    ここでメインのシーンに遷移する
                    *********/
                    SceneManager.LoadScene("newUI");
                }
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
                    if (GUILayout.Button("Enter Room : " + roomParam))
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
            GUILayout.BeginHorizontal();
            GUILayout.Label("PlayerName : ");
            MonobitNetwork.playerName = GUILayout.TextField(
                (MonobitNetwork.playerName == null) ?
                    "" :
                    MonobitNetwork.playerName, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            // デフォルトロビーへの自動入室を許可する
            MonobitNetwork.autoJoinLobby = true;

            // MUNサーバに接続する
            if (GUILayout.Button("Connect Server", GUILayout.Width(150)))
            {
                MonobitNetwork.ConnectServer("SimpleChat_v1.0");
                Debug.Log("サーバに接続しました");
            }

        }



    }
}
