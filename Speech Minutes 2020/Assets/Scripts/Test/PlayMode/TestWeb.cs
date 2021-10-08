using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using MonobitEngine;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Linq;
public class TestWeb : UnityEngine.MonoBehaviour
{
    public GameObject CameraPanel;
    bool cameraswitch = false;
    public RawImage rawImage;
        //public RawImage rawImage2;
    WebCamTexture webCamTexture;
    // Start is called before the first frame update
     public void OnClick()
        {
            if (!CameraPanel.activeSelf)
            {
                CameraPanel.SetActive(true);
                webCamTexture = new WebCamTexture();
                rawImage.texture = webCamTexture;
                webCamTexture.Play();
                Debug.Log("押された!"); // ログを出力
            }
        }
        public void Onpush()
        {
            CameraPanel.SetActive(false);
            webCamTexture.Stop();
        }
}
