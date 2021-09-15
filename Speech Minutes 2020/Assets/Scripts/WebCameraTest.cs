using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MonobitEngine;

public class WebCameraTest : MonobitEngine.MonoBehaviour {
    public GameObject CameraPanel;
    bool cameraswitch = false;

    public RawImage rawImage;
    public RawImage rawImage2;

    WebCamTexture webCamTexture;
    int cnt = 0;

    public void OnClick()
    {
        if(!CameraPanel.activeSelf)
        {
            CameraPanel.SetActive(true);
            webCamTexture = new WebCamTexture();
            rawImage.texture = webCamTexture;
            webCamTexture.Play();
            monobitView.RPC("Come", MonobitTargets.All);
            Debug.Log("押された!"); // ログを出力
        }
    }
    public void Onpush()
    {
        CameraPanel.SetActive(false);
        webCamTexture.Stop();
        monobitView.RPC("Goout", MonobitTargets.All);
    }

	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    // 他の誰かがカメラボタンを押した時の処理
	public void Come()
    {
        Debug.Log("誰か来た");
    }
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
    public void Goout()
    {
        Debug.Log("誰か帰った");
    }


}