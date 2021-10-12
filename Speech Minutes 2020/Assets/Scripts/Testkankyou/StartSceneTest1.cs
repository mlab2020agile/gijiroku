using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEditor;
using System.IO;

public class StartSceneTest1 : MonobitEngine.MonoBehaviour
{
    /** ルーム名. */
    private string roomName = "";
    private string roomPasword = "";
    private string name = "";
    private string expect = "";

    private string roomUnrock = "";
    private int r = 0;
    private int i = 0;
    private bool rswitch = false;
    private bool r2switch = false;
    
    public IEnumerator Waittime5()
    {
        yield return new WaitForSeconds(3);
        rswitch = true;
    }
    public IEnumerator Waittime52()
    {
        yield return new WaitForSeconds(3);
        r2switch = true;
    }
        public IEnumerator Waittime522()
    {
        yield return new WaitForSeconds(3);
    }

    public void tete()
    {
            i += 1;
            if(i == 1)
            {
                roomName = "abcde";
                roomPasword = "abcde";
                name = "aa";
                expect = "ルームを作成しました";
            }
            else if(i == 2)
            {
                roomName = "a";
                roomPasword = "a";
                name = "aa";
                expect = "ルームを作成しました";
            }
            else if(i == 3)
            {
                roomName = "";
                roomPasword = "a";
                name = "aa";
                expect = "部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n";
            }
            else if(i == 4)
            {
                roomName = "a";
                roomPasword = "";
                name = "aa";
                expect = "部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n";
            }
            else if(i == 5)
            {
                roomName = "aaaaa";
                roomPasword = "bbbbb";
                name = "aa";
                expect = "ルームを作成しました";
            }
            else if(i == 6)
            {
                roomName = "abcd";
                roomPasword = "";
                name = "aa";
                expect = "部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n";
            }
            else if(i == 7)
            {
                roomName = "abcdef";
                roomPasword = "";
                name = "aa";
                expect = "部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n";
            }
    }
    public void OnGUI()
    {
            StartCoroutine(Waittime522());
            Debug.Log(i);
            //SceneManager.LoadScene("StartScene");
            /*else if(i == 8)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }
            else if(i == 2)
            {
                roomName = "";
                roomPasword = "";
                name = "aa";
                expect = "";
            }*/
            
            
            
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
                        string s = RoomSakusei(roomName,roomPasword);
                        if (s =="ルームを作成しました")
                        {
                            int a = roomName.Length;
                            int b = roomPasword.Length;
                            MonobitNetwork.CreateRoom(roomName+roomPasword+a+b);
                            Debug.Log(s);
                        /********
                        ここでメインのシーンに遷移する
                        *********/
                            SceneManager.LoadScene("newUI");
                        }
                        else
                        {
                            Debug.Log(s);
                        //EditorUtility.DisplayDialog("警告", s, "Close");
                        }
                    }
                    GUILayout.EndHorizontal();
                    StopCoroutine(Waittime52());
                    StartCoroutine(Waittime52());
                    Debug.Log(r);
                    if (r2switch)
                    {
                        r2switch = false;
                        string s = RoomSakusei(roomName,roomPasword);
                        if (s =="ルームを作成しました")
                        {
                            int a = roomName.Length;
                            int b = roomPasword.Length;
                            Debug.Log(s);
                            StreamWriter sw = new StreamWriter("../TextData.txt",false);// TextData.txtというファイルを新規で用意
                            sw.WriteLine(s);// ファイルに書き出したあと改行
                            sw.Flush();// StreamWriterのバッファに書き出し残しがないか確認
                            sw.Close();// ファイルを閉じる
                            //continue;
                            /********
                            ここでメインのシーンに遷移する
                            *********/
                            MonobitNetwork.CreateRoom(roomName+roomPasword+a+b);
                            SceneManager.LoadScene("newUI");
                        }
                        else
                        {
                            Debug.Log(s);
                            StreamWriter sw = new StreamWriter("../TextData.txt",true);// TextData.txtというファイルを新規で用意
                            sw.WriteLine(s);// ファイルに書き出したあと改行
                            sw.Flush();// StreamWriterのバッファに書き出し残しがないか確認
                            sw.Close();// ファイルを閉じる
                            //continue;
                            //EditorUtility.DisplayDialog("警告", s, "Close");
                        }
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
                            else
                            {
                                string s = "パスワードが違います";
                                EditorUtility.DisplayDialog("警告", s, "Close");
                                Debug.Log(s);
                            }
                        
                        }
                        GUILayout.EndHorizontal();
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
                            name :
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
                StartCoroutine(Waittime5());
                if (rswitch)
                {
                    MonobitNetwork.ConnectServer("SimpleChat_v1.0");
                    Debug.Log("サーバに接続しました");
                }

            }
        }



    }
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


