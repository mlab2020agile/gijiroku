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
    int width = 80;
    int height = 60;
    int fps = 30;
    int s = 1;
    Texture2D texture1;
    Texture2D texture2;
    Texture2D texture3;
    Texture2D texture4;
    WebCamTexture webcamTexture;
    Color32[] colors = null;
    Color32[] color1 = null;
    Color32[] color2 = null;
    Color32[] color3 = null;
    Color32[] color4 = null;
    public RawImage rawImage1;
    public RawImage rawImage2;
    public RawImage rawImage3;
    public RawImage rawImage4;
    public bool cameraswitch = false;
    public Text PlayerText;
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject CameraPanel;
    int cnt = 0;
    IEnumerator Init()
    {
        while (true)
        {
            if (webcamTexture.width/2 > 16 && webcamTexture.height/2 > 16)
            {
                colors = new Color32[webcamTexture.width * webcamTexture.height];
                color1 = new Color32[webcamTexture.width/8 * webcamTexture.height/8];
                color2 = new Color32[webcamTexture.width/8 * webcamTexture.height/8];
                color3 = new Color32[webcamTexture.width/8 * webcamTexture.height/8];
                color4 = new Color32[webcamTexture.width/8 * webcamTexture.height/8];
                texture1 = new Texture2D(webcamTexture.width/8, webcamTexture.height/8, TextureFormat.RGBA32, false);
                rawImage1.GetComponent<RawImage>().texture = texture1;
                texture2 = new Texture2D(webcamTexture.width/8, webcamTexture.height/8, TextureFormat.RGBA32, false);
                rawImage2.GetComponent<RawImage>().texture = texture2;
                texture3 = new Texture2D(webcamTexture.width/8, webcamTexture.height/8, TextureFormat.RGBA32, false);
                rawImage3.GetComponent<RawImage>().texture = texture3;
                texture4 = new Texture2D(webcamTexture.width/8, webcamTexture.height/8, TextureFormat.RGBA32, false);
                rawImage4.GetComponent<RawImage>().texture = texture4;
                break;
            }
            yield return null;
        }
    }
    // Use this for initialization
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, 80, 60, this.fps);
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
                    if (s % 50 == 0)
                    {
                        Debug.Log("sが50の倍数到達");
                        var cc = webcamTexture.GetPixels32(colors);
                        int width = webcamTexture.width;
                        int height = webcamTexture.height;
                        Color32 rc = new Color32(0, 0, 0, byte.MaxValue);
                        for (int x = 0; x < width; x+=8)
                        {
                            
                            for (int y = 0; y < height; y+=8)
                            {
                                Color32 c = colors[x + y * width];
                                monobitView.RPC("Video", MonobitTargets.All, x, y, c.r, c.g, c.b, c.a, MonobitEngine.MonobitNetwork.player.ID);
                            }
                        }
                    }
                    //monobitView.RPC("Name", MonobitTargets.Others, MonobitEngine.MonobitNetwork.player.name);
                    s += 1;
                
                    
            }
        }
    }
    /// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    // 他の誰かがカメラボタンを押した時の処理
    public void Come(int ID)
    {
        Debug.Log("誰か来た");
        if (ID == 1)
        {
            rawImage1.transform.localPosition = new Vector3(-250, -160, 0);
            //RectTransform rt4 = rawImage1.GetComponent<RectTransform>();
            //rt4.sizeDelta = new Vector2(100, 100);
        }
        if (ID == 2)
        {
            rawImage2.transform.localPosition = new Vector3(-150, -160, 0);
        }
        if (ID == 3)
        {
            rawImage3.transform.localPosition = new Vector3(-250, -290, 0);
        }
        if (ID == 4)
        {
            rawImage4.transform.localPosition = new Vector3(-250, -290, 0);
        }
    }
    /// <summary>
    /// 初期化
    /// </summary>
    [MunRPC]
    public void Goout(int ID)
    {
        if (ID == 1)
        {
            rawImage1.transform.localPosition = new Vector3(1000, 1000, 0);
        }
        if (ID == 2)
        {
            rawImage2.transform.localPosition = new Vector3(1000, 1000, 0);
            //Panel2.SetActive(false);
        }
        if (ID == 3)
        {
            rawImage3.transform.localPosition = new Vector3(1000, 1000, 0);
        }
        if (ID == 4)
        {
            rawImage4.transform.localPosition = new Vector3(1000, 1000, 0);
            //Panel2.SetActive(false);
        }
    }
    /// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    public void Video(int x, int y, Byte r, Byte g, Byte b, Byte a, int id)
    {
        Color32 ccc = new Color32(r, g, b, 255);
        if(id == 1)
        {
            color1[x/8 + y/8 * width] = ccc;
            if (x/8 >= width - 1 && y/8 >= height - 1)
            {
                Debug.Log("画像送る");
                Debug.Log(width);
                Debug.Log(height);
                texture1.SetPixels32(color1);
                texture1.Apply();
                rawImage1.transform.localPosition = new Vector3(-250, -160, 0);
            }
        }
        if(id == 2)
        {
            color2[x/8 + y/8 * width] = ccc;
            if (x/8 >= width - 1 && y/8 >= height - 1)
            {
                Debug.Log("画像送る");
                Debug.Log(width);
                Debug.Log(height);
                texture2.SetPixels32(color2);
                texture2.Apply();
                rawImage2.transform.localPosition = new Vector3(-150, -160, 0);
            }
        }
        if(id == 3)
        {
            color3[x/8 + y/8 * width] = ccc;
            if (x/8 >= width - 1 && y/8 >= height - 1)
            {
                Debug.Log("画像送る");
                Debug.Log(width);
                Debug.Log(height);
                texture3.SetPixels32(color1);
                texture3.Apply();
                rawImage3.transform.localPosition = new Vector3(-250, -290, 0);
            }
        }
        if(id == 4)
        {
            color4[x/8 + y/8 * width] = ccc;
            if (x/8 >= width - 1 && y/8 >= height - 1)
            {
                Debug.Log("画像送る");
                Debug.Log(width);
                Debug.Log(height);
                texture4.SetPixels32(color4);
                texture4.Apply();
                rawImage4.transform.localPosition = new Vector3(-150, -290, 0);
            }
        }
    }
    /// <summary>
	/// 初期化
	/// </summary>

    public void OnClick()
    {
        if (!cameraswitch)
        {
            monobitView.RPC("Come", MonobitTargets.All,MonobitEngine.MonobitNetwork.player.ID);
            cameraswitch = true;
        }
        else
        {
            monobitView.RPC("Goout", MonobitTargets.All,MonobitEngine.MonobitNetwork.player.ID);
            cameraswitch = false;
        }
    }
}