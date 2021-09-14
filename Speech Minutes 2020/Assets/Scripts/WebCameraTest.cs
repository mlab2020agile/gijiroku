using UnityEngine;
using UnityEngine.UI;

public class WebCameraTest : MonoBehaviour {
    public GameObject CameraPanel;
    bool cameraswitch = false;

    public RawImage rawImage;

    WebCamTexture webCamTexture;

    public void OnClick()
    {
        if(!CameraPanel.activeSelf)
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