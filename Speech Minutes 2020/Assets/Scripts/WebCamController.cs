using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MonobitEngine;
public class WebCamController : MonobitEngine.MonoBehaviour
{
    int width = 640;
    int height = 480;
    int fps = 30;
    int s = 1;
    int t = 1;
    int u = 0;
    Texture2D texture2;
    Texture2D texture3;
    WebCamTexture webcamTexture;
    Color32[] colors = null;
    Color32[] colorss = null;
    public RawImage rawImage;
    public RawImage rawImage2;
    public RawImage rawImage3;
    public bool cameraswitch = false;
    public Text PlayerText;
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    int cnt = 0;
    IEnumerator Init()
    {
        while (true)
        {
            if (webcamTexture.width > 16 && webcamTexture.height > 16)
            {
                colors = new Color32[webcamTexture.width * webcamTexture.height];
                colorss = new Color32[webcamTexture.width * webcamTexture.height];
                texture2 = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
                rawImage2.GetComponent<RawImage>().material.mainTexture = texture2;
                //texture3 = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
                //rawImage3.GetComponent<RawImage>().material.mainTexture = texture3;
                break;
            }
            yield return null;
        }
    }
    // Use this for initialization
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
        webcamTexture.Play();
        StartCoroutine(Init());
    }
    // Update is called once per frame
    void Update()
    {
        if (cameraswitch)
        {
            if (colors != null)
            {
                if (cnt == 1 || cnt == 2)
                {
                    if (s % 200 == 0)
                    {
                        Debug.Log("sが200の倍数到達");
                        var cc = webcamTexture.GetPixels32(colors);
                        int width = webcamTexture.width;
                        int height = webcamTexture.height;
                        Color32 rc = new Color32(0, 0, 0, byte.MaxValue);
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                Color32 c = colors[x + y * width];
                                monobitView.RPC("Video", MonobitTargets.Others, x, y, c.r, c.g, c.b, c.a);
                                //byte gray = (byte)(0.1f * c.r + 0.7f * c.g + 0.2f * c.b);
                                //rc.r = rc.g = rc.b = gray;
                                //colors[x + y * width] = rc;
                            }
                        }
                    }
                    monobitView.RPC("Name", MonobitTargets.Others, MonobitEngine.MonobitNetwork.player.name);
                    s += 1;
                }
                /*else if (cnt == 3)
                {
                    if (t % 200 == 0)
                    {
                        var cc = webcamTexture.GetPixels32(colors);
                        int width = webcamTexture.width;
                        int height = webcamTexture.height;
                        Color32 rc = new Color32(0, 0, 0, byte.MaxValue);
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                Color32 c = colors[x + y * width];
                                if (u % 2 == 0)
                                {
                                    monobitView.RPC("VideoSwitch", MonobitTargets.All);
                                    monobitView.RPC("Video", MonobitTargets.Others, x, y, c.r, c.g, c.b, c.a);
                                }
                                else
                                {
                                    monobitView.RPC("VideoSwitch", MonobitTargets.All);
                                    monobitView.RPC("Video2", MonobitTargets.Others, x, y, c.r, c.g, c.b, c.a);
                                }
                                //byte gray = (byte)(0.1f * c.r + 0.7f * c.g + 0.2f * c.b);
                                //rc.r = rc.g = rc.b = gray;
                                //colors[x + y * width] = rc;
                            }
                        }
                    }
                    //monobitView.RPC("Name", MonobitTargets.Others, MonobitEngine.MonobitNetwork.player.name);
                    t += 1;
                }*/
                //Debug.Log(s);
                //texture.SetPixels32(cc);
                //texture.Apply();
            }
        }
    }
    /// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    // 他の誰かがカメラボタンを押した時の処理
    public void Come()
    {
        cnt += 1;
        Debug.Log("誰か来た");
        Debug.Log(cnt);
        if (cnt == 1 || cnt == 2)
        {
            Panel2.SetActive(true);
            rawImage.transform.localPosition = new Vector3(-150, -10, 0);
            rawImage.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt1 = rawImage.GetComponent<RectTransform>();
            rt1.sizeDelta = new Vector2(300, 300);
            rawImage2.transform.localPosition = new Vector3(160, -10, 0);
            rawImage2.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt2 = rawImage2.GetComponent<RectTransform>();
            rt2.sizeDelta = new Vector2(300, 300);
        }
        else if (cnt == 3)
        {
            Panel3.SetActive(true);
            rawImage.transform.localPosition = new Vector3(-200, -10, 0);
            rawImage.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt3 = rawImage.GetComponent<RectTransform>();
            rt3.sizeDelta = new Vector2(200, 200);
            rawImage2.transform.localPosition = new Vector3(10, -10, 0);
            rawImage.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt4 = rawImage2.GetComponent<RectTransform>();
            rt4.sizeDelta = new Vector2(200, 200);
            rawImage3.transform.localPosition = new Vector3(220, -10, 0);
            rawImage.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt5 = rawImage3.GetComponent<RectTransform>();
            rt5.sizeDelta = new Vector2(200, 200);
        }
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void Goout()
    {
        cnt -= 1;
        Debug.Log("誰か帰った");
        if (cnt == 2)
        {
            rawImage.transform.localPosition = new Vector3(-150, -10, 0);
            rawImage.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt6 = rawImage.GetComponent<RectTransform>();
            rt6.sizeDelta = new Vector2(300, 300);
            rawImage2.transform.localPosition = new Vector3(160, -10, 0);
            rawImage2.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt7 = rawImage2.GetComponent<RectTransform>();
            rt7.sizeDelta = new Vector2(300, 300);
            Panel3.SetActive(false);
        }
        else if (cnt == 1)
        {
            rawImage.transform.localPosition = new Vector3(-150, -10, 0);
            rawImage.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt8 = rawImage.GetComponent<RectTransform>();
            rt8.sizeDelta = new Vector2(300, 300);
            Panel2.SetActive(false);
        }
    }
    /// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    public void Video(int x, int y, Byte r, Byte g, Byte b, Byte a)
    {
        Color32 ccc = new Color32(r, g, b, 255);
        colorss[x + y * width] = ccc;
        if (x == width - 1 && y == height - 1)
        {
            if (cnt == 1) // 同期のズレを解消
            {
                cnt += 1;
            }
            Debug.Log("画像送る");
            texture2.SetPixels32(colorss);
            texture2.Apply();
        }
    }
    /// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    public void Video2(int x, int y, Byte r, Byte g, Byte b, Byte a)
    {
        Color32 ccc = new Color32(r, g, b, 255);
        colorss[x + y * width] = ccc;
        if (x == width - 1 && y == height - 1)
        {
            texture3.SetPixels32(colorss);
            texture3.Apply();
        }
    }
    /// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    public void VideoSwitch()
    {
        u += 1;
    }
    /// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    public void Name(string n)
    {
        PlayerText.text = n;
    }
    public void OnClick()
    {
        monobitView.RPC("Come", MonobitTargets.All);
        cameraswitch = true;
    }
    public void Onpush()
    {
        monobitView.RPC("Goout", MonobitTargets.All);
        cameraswitch = false;
    }
}