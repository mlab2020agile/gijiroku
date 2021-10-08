using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using MonobitEngine;
using System.Text.RegularExpressions;
using UnityEditor;

namespace Tests
{
    public class otamesi: MonobitEngine.MonoBehaviour
    {
        private string roomName = "";
        private string roomPasword = "";
        private string roomUnrock = "";
        public GameObject MUNN;
        StartSecneMUNScript var;

        public bool roomswitch = false;

        public bool roomswitch2 = false;

        public void Setup()
        {
            SceneManager.LoadScene("StartScene");
        }

        private void OnGUI()
    {
        //MUNサーバに接続している場合
        if (roomswitch)
        {
            //Debug.Log("サーバに接続しました");
            // ルームに入室している場合
            if (roomswitch2)
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
                        //MonobitNetwork.CreateRoom(roomName+roomPasword+a+b);
                        Debug.Log(s);
                        /********
                        ここでメインのシーンに遷移する
                        *********/
                        SceneManager.LoadScene("newUI");
                    }
                    else
                    {
                        Debug.Log(s);
                        EditorUtility.DisplayDialog("警告", s, "Close");
                    }
                }
                GUILayout.EndHorizontal();
                // ルーム一覧を検索
                
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
                string playerName = GUILayout.TextField("" ,GUILayout.Height(x/40), GUILayout.Width(x/3));
            }
            GUILayout.EndHorizontal();

            // デフォルトロビーへの自動入室を許可する
            //MonobitNetwork.autoJoinLobby = true;

            // MUNサーバに接続する
            GUILayout.BeginHorizontal();
            GUILayout.Space(x*3/8);
            if (GUILayout.Button("サーバに接続",GUILayout.Height(x/35), GUILayout.Width(x/3)))
            {
                //MonobitNetwork.ConnectServer("SimpleChat_v1.0");
                Debug.Log("サーバに接続しました");
            }
            GUILayout.EndHorizontal();

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
        


        // A Test behaves as an ordinary method
        [Test]
        public void otamesiSimplePasses()
        {
            // Use the Assert class to test conditions
            //SceneManager.LoadScene("StartScene");
            //string d = StartSecneMUNScript.RoomSakusei("se", "ai");

            int i = 1;
            Assert.AreEqual(1, i);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator otamesiWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            SceneManager.LoadScene("StartScene");
            var = MUNN.GetComponent<StartSecneMUNScript>();
            var.Player = "aa";
            //OnGUI();
            yield return new WaitForSeconds(10);
        }
    }
}
