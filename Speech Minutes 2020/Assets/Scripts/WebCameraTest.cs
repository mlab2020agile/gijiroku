using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MonobitEngine;
public class WebCameraTest : MonobitEngine.MonoBehaviour
{
    public GameObject CameraPanel;
    bool cameraswitch = false;
    public RawImage rawImage;
    //public RawImage rawImage2;
    WebCamTexture webCamTexture;
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