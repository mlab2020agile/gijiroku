using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using System.Linq;

namespace Tests
{
    public class PassTest
    {
        public string RoomSakusei(string name, string pass)
        {
            string s ="";
            int a = name.Length;
            int b = pass.Length;
            if (a == 0 || b == 0)
            {
                s = "・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n";
                if (!(Regex.Match(name, "^[a-zA-Z0-9]+$")).Success　&& a != 0)
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

        [Test]
        public void PassTest01()
        {
            string name = "abcde";
            string pass = "abcde";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"ルームを作成しました");
        }

        [Test]
        public void PassTest02()
        {
            string name = "a";
            string pass = "a";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"ルームを作成しました");
        }

        [Test]
        public void PassTest03()
        {
            string name = "";
            string pass = "a";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n");
        }
        [Test]
        public void PassTest04()
        {
            string name = "a";
            string pass = "";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n");
        }
        [Test]
        public void PassTest05()
        {
            string name = "aaaaa";
            string pass = "bbbbb";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"ルームを作成しました");
        }
        [Test]
        public void PassTest06()
        {
            string name = "abcd";
            string pass = "";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n");
        }
        [Test]
        public void PassTest07()
        {
            string name = "abcdef";
            string pass = "abcd";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n");
        }
        [Test]
        public void PassTest08()
        {
            string name = "Ａ";
            string pass = "a";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・半角英数字で入力してください");
        }
        [Test]
        public void PassTest09()
        {
            string name = "a";
            string pass = "Ａ";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・半角英数字で入力してください");
        }
        [Test]
        public void PassTest10()
        {
            string name = "";
            string pass = "";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n");
        }
        [Test]
        public void PassTest11()
        {
            string name = "Ａ";
            string pass = "";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n・半角英数字で入力してください");
        }
        [Test]
        public void PassTest12()
        {
            string name = "";
            string pass = "Ａ";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n・半角英数字で入力してください");
        }
        [Test]
        public void PassTest13()
        {
            string name = "ＡＡＡＡＡＡ";
            string pass = "";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n・半角英数字で入力してください");
        }
        [Test]
        public void PassTest14()
        {
            string name = "";
            string pass = "ＡＡＡＡＡＡ";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n・半角英数字で入力してください");
        }
        [Test]
        public void PassTest15()
        {
            string name = "abcdef";
            string pass = "abcdef";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n");
        }
        [Test]
        public void PassTest16()
        {
            string name = "ＡＡＡＡＡＡ";
            string pass = "ＡＡＡＡＡＡ";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n・半角英数字で入力してください");
        }
        [Test]
        public void PassTest17()
        {
            string name = "アイウエオ";
            string pass = "abcde";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・半角英数字で入力してください");
        }
        [Test]
        public void PassTest18()
        {
            string name = "abcde";
            string pass = "アイウエオ";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・半角英数字で入力してください");
        }
        [Test]
        public void PassTest19()
        {
            string name = "アイウエオ";
            string pass = "";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n・半角英数字で入力してください");
        }
        [Test]
        public void PassTest20()
        {
            string name = "アイウエオ";
            string pass = "abcdef";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・部屋名・パスワードどちらも1~5文字の範囲で入力してください\r\n・半角英数字で入力してください");
        }
        [Test]
        public void PassTest21()
        {
            string name = "12345";
            string pass = "123";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"ルームを作成しました");
        }
        [Test]
        public void PassTest22()
        {
            string name = "abcde";
            string pass = "12345";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"ルームを作成しました");
        }
        [Test]
        public void PassTest23()
        {
            string name = "ABCDE";
            string pass = "ABCDE";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"ルームを作成しました");
        }
        [Test]
        public void PassTest24()
        {
            string name = "ABCDE";
            string pass = "12345";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"ルームを作成しました");
        }
        [Test]
        public void PassTest25()
        {
            string name = "@@@@";
            string pass = "abcde";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・半角英数字で入力してください");
        }
        [Test]
        public void PassTest26()
        {
            string name = "abcde";
            string pass = "@@@@";
            string s = RoomSakusei(name,pass);
            Assert.AreEqual(s,"・半角英数字で入力してください");
        }
    }
}


