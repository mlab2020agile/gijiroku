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
    Texture2D texture;
    WebCamTexture webcamTexture;
    Color32[] colors = null;
    Color32[] colorss = null;
    public RawImage rawImage2;

    IEnumerator Init()
    {
        while (true)
        {
            if (webcamTexture.width > 16 && webcamTexture.height > 16)
            {
                colors = new Color32[webcamTexture.width * webcamTexture.height];
                colorss = new Color32[webcamTexture.width * webcamTexture.height];
                texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
                rawImage2.GetComponent<RawImage>().material.mainTexture = texture;
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
        if (colors != null)
        {
            var cc = webcamTexture.GetPixels32(colors);
            

            int width = webcamTexture.width;
            int height = webcamTexture.height;
            Color32 rc = new Color32(0, 0, 0, byte.MaxValue);
            if (s == 500 || s == 1)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color32 c = colors[x + y * width];
                        monobitView.RPC("Video", MonobitTargets.All, x, y, c.r, c.g, c.b, c.a);
                        //byte gray = (byte)(0.1f * c.r + 0.7f * c.g + 0.2f * c.b);
                        //rc.r = rc.g = rc.b = gray;
                        //colors[x + y * width] = rc;
                    }
                }
            }
            s +=1;
            Debug.Log(s);
            //texture.SetPixels32(cc);
            //texture.Apply();
        }
    }
    /// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    public void Video(int x, int y, Byte r, Byte g, Byte b, Byte a)
    {
        Color32 ccc = new Color32(r,g,b,255);
        colorss[x + y * width] = ccc;
        if (x == width-1 && y == height-1)
        {
            texture.SetPixels32(colorss);
            texture.Apply();
        }
    }

}
