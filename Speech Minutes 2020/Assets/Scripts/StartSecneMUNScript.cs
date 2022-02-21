using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MonobitEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class StartSecneMUNScript : MonobitEngine.MonoBehaviour
{
    //test
    /** ルーム名. */
    private string roomName = "";
    private string roomPasword = "";
    private string roomUnrock = "";
    public string player;
    public string uname;
    public static string abcd123 = "aaaaaaaa";
    public void OnGUI()
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
                //スクリーンサイズによってUIの位置や大きさを決定
                int x = Screen.width;
                int y = Screen.height;
                // ルーム名の入力
                //UIの位置を中心に
                GUILayout.Space(y/3.2f);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space (x/6);
                    GUI.skin.label.fontSize = x/30;
                    GUILayout.Label("部屋名・パスワードは半角英数字5文字まで", GUILayout.Height(x/20), GUILayout.Width(x*2/3));
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space (x/4);
                    GUI.skin.label.fontSize = x/50;
                    GUILayout.Label("部屋名: ", GUILayout.Width(x/8));
                    GUI.skin.textField.fontSize = x/50;
                    GUI.skin.button.fontSize = x/50;
                    roomName = GUILayout.TextField(roomName,GUILayout.Height(x/40),GUILayout.Width(x/3));
                }
                GUILayout.EndHorizontal();
                //パスワード欄
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space (x/5);
                    GUILayout.Label("パスワード作成: ",GUILayout.Height(x/40),GUILayout.Width(x*7/40));
                    roomPasword = GUILayout.TextField(roomPasword,GUILayout.Height(x/40),GUILayout.Width(x/3));
                }
                GUILayout.EndHorizontal();
                // ルームを作成して入室する
                GUILayout.BeginHorizontal();
                GUILayout.Space(x*3/8);
                if (GUILayout.Button("部屋を作成",GUILayout.Height(x/35),GUILayout.Width(x/3)))
                {
                    abcd123 = RoomSakusei(roomName,roomPasword);
                    if (abcd123 =="ルームを作成しました")
                    {
                        int a = roomName.Length;
                        int b = roomPasword.Length;
                        //MUNの仕様上string型の情報はroomの１つしか持っていけないため部屋名とパスワードとそれぞれの文字数を合わせている
                        MonobitNetwork.CreateRoom(roomName+roomPasword+a+b);
                        Debug.Log(abcd123);
                    
                        /********
                        ここでメインのシーンに遷移する
                        *********/
                        SceneManager.LoadScene("newUI");
                    }
                    //制約範囲外の場合は警告シーンへ
                    else
                    {
                        Debug.Log(abcd123);
                        #if UNITY_EDITOR
                        //EditorUtility.DisplayDialog("警告", ss, "Close");
                        #endif
                        SceneManager.LoadScene("Alert");
                    }
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
                    string rn = room.name;
                    string s1 = rn.Substring(rn.Length - 1);
                    string s2 = rn.Substring(rn.Length - 2,1);
                    int i1 = int.Parse(s1);
                    int i2 = int.Parse(s2);
                    string s3 = rn.Substring(0,i2);
                    string s4 = rn.Substring(i2,i1);
                    //ルームパスワードと結びつけ
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Space (x/8);
                        GUILayout.Label(s3+"のパスワード解除: ",GUILayout.Height(x/40),GUILayout.Width(x/4));
                        roomUnrock = GUILayout.TextField(roomUnrock,GUILayout.Height(x/40),GUILayout.Width(x/3));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(x*3/8);
                    // ルームを選択して入室する
                    if (GUILayout.Button("部屋に入室 : " + s3, GUILayout.Height(x/35),GUILayout.Width(x/3)))
                    {
                        if (roomUnrock == s4)
                        {
                            MonobitNetwork.JoinRoom(room.name);
                            /********
                            ここでメインのシーンに遷移する
                            *********/
                            SceneManager.LoadScene("newUI");
                        }
                        //間違っていた場合は警告のシーンへ
                        else
                        {
                            abcd123 = "パスワードが違います";
                            #if UNITY_EDITOR
                            //EditorUtility.DisplayDialog("警告", ss, "Close");
                            #endif
                            SceneManager.LoadScene("Alert");
                            Debug.Log(abcd123);
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
                uname = GUILayout.TextField(uname,GUILayout.Height(x/40), GUILayout.Width(x/3));
            }
            GUILayout.EndHorizontal();

            // デフォルトロビーへの自動入室を許可する
            MonobitNetwork.autoJoinLobby = true;

            // MUNサーバに接続する
            GUILayout.BeginHorizontal();
            GUILayout.Space(x*3/8);
            if (GUILayout.Button("サーバに接続",GUILayout.Height(x/35), GUILayout.Width(x/3)))
            {
                //名前入力がない場合は警告のシーンへ
                if (uname == "")
                {
                    abcd123 = "名前を入力してください";
                    SceneManager.LoadScene("Alert");
                }
                else
                {
                    MonobitNetwork.playerName = uname;
                    MonobitNetwork.ConnectServer("SimpleChat_v1.0");
                    Debug.Log("サーバに接続しました");
                }
            }
            GUILayout.EndHorizontal();

        }



    }
    //1~5文字の半角英数字になっているかの判定と判定によっての警告文を決定
    public string RoomSakusei(string name, string pass)
    {
        string s ="";
        int a = name.Length;
        int b = pass.Length;
        if (a == 0 || b == 0)
        {
            s = "・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n";
            if (!(Regex.Match(name, "^[a-zA-Z0-9]+$")).Success && a != 0)
            {
                s = s + "・半角英数字で入力してください";
            }
            else if (!(Regex.Match(pass, "^[a-zA-Z0-9]+$")).Success && b != 0)
            {
                s = s + "・半角英数字で入力してください";
            }
                        
        }
        else if (a <= 5 && b <= 5)
        {
            if (!(Regex.Match(name, "^[a-zA-Z0-9]+$")).Success)
            {
                s = s + "・半角英数字で入力してください";
            }
            else if (!(Regex.Match(pass, "^[a-zA-Z0-9]+$")).Success)
            {
                s = s + "・半角英数字で入力してください";
            }
            else
            {
                s = "ルームを作成しました";
            }
        }
        else
        {
            s = "・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n";
            if (!(Regex.Match(name, "^[a-zA-Z0-9]+$")).Success)
            {
                s = s + "・半角英数字で入力してください";
            }
            else if (!(Regex.Match(pass, "^[a-zA-Z0-9]+$")).Success)
            {
                s = s + "・半角英数字で入力してください";
            }
        }
        return s;
    }
}

