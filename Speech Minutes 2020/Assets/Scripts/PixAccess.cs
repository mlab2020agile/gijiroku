using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MonobitEngine;


public class PixAccess : MonobitEngine.MonoBehaviour
{
	Texture2D drawTexture;
	Color[] buffer;
	/// <summary>
	/// ボタンと連動
	/// </summary>
	public GameObject PenMode;
	public bool mode = false;
	public Text Buttontext;
	public Material lineMaterial;
	public Color lineColor;
	[Range(0, 64)] public float lineWidth;
	public GameObject itakire;
	/// <summary>
	/// 白黒反転
	/// </summary>
	//int inversionFlag = 0;
	public MeshRenderer targetMeshRenderer;

	private Vector2 _prevPosition;
	//public Material image1;

	public void Start()
	{
		Texture2D mainTexture = (Texture2D)GetComponent<Renderer>().material.mainTexture;
		mainTexture = ResizeTexture(mainTexture, mainTexture.width *2, mainTexture.height );
		Color[] pixels = mainTexture.GetPixels();

		buffer = new Color[pixels.Length];
		Debug.Log(pixels.Length);
		Debug.Log(mainTexture.format);
		pixels.CopyTo(buffer, 0);


		drawTexture = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
		drawTexture.filterMode = FilterMode.Point;

		PenMode = GameObject.Find("PenMode");
		itakire = GameObject.Find("Plane");
		Clear();
	}

	//PenMode切り替え用
	public void Mode()
	{
		if (mode)
		{
			mode = false;
			Debug.Log("PenMode:" + mode);
			Buttontext.text = "PenMode:false";
		}
		else
		{
			mode = true;
			Debug.Log("PenMode:" + mode);
			Buttontext.text = "PenMode:true";
		}
	}
	static Texture2D ResizeTexture(Texture2D srcTexture, int newWidth, int newHeight) 
	{
		var resizedTexture = new Texture2D(newWidth, newHeight);
        Graphics.ConvertTexture(srcTexture, resizedTexture);
        return resizedTexture;
    }
	/// <summary>
	/// 初期化
	/// </summary>
	//全けしのメソッド
	[MunRPC]
	public void Clear()
	{

		Texture2D mainTexture = (Texture2D)GetComponent<Renderer>().material.mainTexture;
		mainTexture = ResizeTexture(mainTexture, mainTexture.width *2, mainTexture.height );
		Color[] pixels = mainTexture.GetPixels();

		buffer = new Color[pixels.Length];
		pixels.CopyTo(buffer, 0);
		/* script.bgColor = Color.white;
		   //  texscript.texcolor = Color.black;*/
		for (int x = 0; x < mainTexture.width; x++)
		{
			for (int y = 0; y < mainTexture.height; y++)
			{
				if (y < mainTexture.height)
				{
					buffer.SetValue(Color.white, x+ 512 * y);
				}
			}
		}
	}

	//「clear」ボタンを押すと呼びだ朝れるメソッド
	public void Clearfjag()
	{
		monobitView.RPC("Clear", MonobitTargets.All);
    }
	/// <summary>
	/// 太さ変更
	/// </summary>
	/// <param name="p"></param>
	[MunRPC]

	// 太さ：1　色：黒
	public void Draw1(Vector2 p)
	{
		for (int x = (int)(p.x*2)-1; x < (int)(p.x*2)+1; x++)
		{
			for (int y = (int)(p.y)-1; y < (int)(p.y)+1; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.black, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：1　色：赤
	public void Draw2(Vector2 p)
	{
		for (int x = (int)(p.x*2)-1; x < (int)(p.x*2)+1; x++)
		{
			for (int y = (int)(p.y)-1; y < (int)(p.y)+1; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.red, x + 512 * y);
				}
			}

		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：1　色：青
	public void Draw3(Vector2 p)
	{
		for (int x = (int)(p.x*2)-1; x < (int)(p.x*2)+1; x++)
		{
			for (int y = (int)(p.y)-1; y < (int)(p.y)+1; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.blue, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：1　色：緑
	public void Draw4(Vector2 p)
	{
		for (int x = (int)(p.x*2)-1; x < (int)(p.x*2)+1; x++)
		{
			for (int y = (int)(p.y)-1; y < (int)(p.y)+1; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.green, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：1　色：黄色
	public void Draw5(Vector2 p)
	{
		for (int x = (int)(p.x*2)-1; x < (int)(p.x*2)+1; x++)
		{
			for (int y = (int)(p.y)-1; y < (int)(p.y)+1; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.yellow, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：1　色：ピンク
	public void Draw6(Vector2 p)
	{
		for (int x = (int)(p.x*2)-1; x < (int)(p.x*2)+1; x++)
		{
			for (int y = (int)(p.y)-1; y < (int)(p.y)+1; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.magenta, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：1　色：白
	public void Draw7(Vector2 p)
	{
		for (int x = (int)(p.x*2)-1; x < (int)(p.x*2)+1; x++)
		{
			for (int y = (int)(p.y)-1; y < (int)(p.y)+1; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.white, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：2　色：黒
	public void Draw8(Vector2 p)
	{
		for (int x = (int)(p.x*2)-2; x < (int)(p.x*2)+2; x++)
		{
			for (int y = (int)(p.y)-2; y < (int)(p.y)+2; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.black, x+ 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：2　色：赤
	public void Draw9(Vector2 p)
	{
		for (int x = (int)(p.x*2)-2; x < (int)(p.x*2)+2; x++)
		{
			for (int y = (int)(p.y)-2; y < (int)(p.y)+2; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.red, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：2　色：青
	public void Draw10(Vector2 p)
	{
		for (int x = (int)(p.x*2)-2; x < (int)(p.x*2)+2; x++)
		{
			for (int y = (int)(p.y)-2; y < (int)(p.y)+2; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.blue, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：2　色：緑
	public void Draw11(Vector2 p)
	{
		for (int x = (int)(p.x*2)-2; x < (int)(p.x*2)+2; x++)
		{
			for (int y = (int)(p.y)-2; y < (int)(p.y)+2; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.green, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：2　色：黄色
	public void Draw12(Vector2 p)
	{
		for (int x = (int)(p.x*2)-2; x < (int)(p.x*2)+2; x++)
		{
			for (int y = (int)(p.y)-2; y < (int)(p.y)+2; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.yellow, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：2　色：ピンク
	public void Draw13(Vector2 p)
	{
		for (int x = (int)(p.x*2)-2; x < (int)(p.x*2)+2; x++)
		{
			for (int y = (int)(p.y)-2; y < (int)(p.y)+2; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.magenta, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：2　色：白
	public void Draw14(Vector2 p)
	{
		for (int x = (int)(p.x*2)-2; x < (int)(p.x*2)+2; x++)
		{
			for (int y = (int)(p.y)-2; y < (int)(p.y)+2; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.white, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：4　色：黒
	public void Draw15(Vector2 p)
	{
		for (int x = (int)(p.x*2)-4; x < (int)(p.x*2)+4; x++)
		{
			for (int y = (int)(p.y)-4; y < (int)(p.y)+4; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.black, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：4　色：赤
	public void Draw16(Vector2 p)
	{
		for (int x = (int)(p.x*2)-4; x < (int)(p.x*2)+4; x++)
		{
			for (int y = (int)(p.y)-4; y < (int)(p.y)+4; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.red, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：4　色：青
	public void Draw17(Vector2 p)
	{
		for (int x = (int)(p.x*2)-4; x < (int)(p.x*2)+4; x++)
		{
			for (int y = (int)(p.y)-4; y < (int)(p.y)+4; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.blue, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：4　色：緑
	public void Draw18(Vector2 p)
	{
		for (int x = (int)(p.x*2)-4; x < (int)(p.x*2)+4; x++)
		{
			for (int y = (int)(p.y)-4; y < (int)(p.y)+4; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.green, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：4　色：黄色
	public void Draw19(Vector2 p)
	{
		for (int x = (int)(p.x*2)-4; x < (int)(p.x*2)+4; x++)
		{
			for (int y = (int)(p.y)-4; y < (int)(p.y)+4; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.yellow, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：4　色：ピンク
	public void Draw20(Vector2 p)
	{
		for (int x = (int)(p.x*2)-4; x < (int)(p.x*2)+4; x++)
		{
			for (int y = (int)(p.y)-4; y < (int)(p.y)+4; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.magenta, x + 512 * y);
				}
			}
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	// 太さ：4　色：白
	public void Draw21(Vector2 p)
	{
		for (int x = (int)(p.x*2)-4; x < (int)(p.x*2)+4; x++)
		{
			for (int y = (int)(p.y)-4; y < (int)(p.y)+4; y++)
			{
				if ((x >= 0) && (y >= 0))
				{
					buffer.SetValue(Color.white, x + 512 * y);
				}
			}
		}
	}

	[MunRPC]
	public void objectCloorUpdate()
    {
		drawTexture.SetPixels(buffer);
		drawTexture.Apply();
		GetComponent<Renderer>().material.mainTexture = drawTexture;
	}

	void Update()
    {

		monobitView.RPC("objectCloorUpdate", MonobitTargets.All);

		if (mode)
		{
			if (Input.GetMouseButton(0))
			{
				//前回値がまだないなら現在の値を前回値として扱う
            	if (_prevPosition == Vector2.zero)
            	{
                	_prevPosition = Input.mousePosition;
            	}

            	//線形補間に使う入力の終点座標
            	Vector2 endPosition = Input.mousePosition;
            	//1フレームの線の距離
            	float lineLength = Vector2.Distance(_prevPosition, endPosition);
            	//線の長さに応じて変わる補間値　CeilToIntは小数点以下を切り上げ
            	int lerpCountAdjustNum = 5;
            	int lerpCount = Mathf.CeilToInt(lineLength / lerpCountAdjustNum);

            	for (int i = 1; i <= lerpCount; i++)
            	{
                	//Lerpの割合値を "現在の回数/合計回数" で出す
                	float lerpWeight = (float) i / lerpCount;

                	//前回の入力座標、現在の入力座標、割合を渡して補間する座標を算出
                	Vector3 lerpPosition = Vector2.Lerp(_prevPosition, Input.mousePosition, lerpWeight);

                	Ray ray = Camera.main.ScreenPointToRay(lerpPosition);
                	RaycastHit hit;
                	if (Physics.Raycast(ray, out hit, 100.0f))
                	{
						//してるlineWidthとlineColorによって関数を呼び出す物を決める
						if (lineWidth == 1)
						{
							if (lineColor == Color.black)
							{
								monobitView.RPC("Draw1", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.red)
							{
								monobitView.RPC("Draw2", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.blue)
							{
								monobitView.RPC("Draw3", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.green)
							{
								monobitView.RPC("Draw4", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.yellow)
							{
								monobitView.RPC("Draw5", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.magenta)
							{
								monobitView.RPC("Draw6", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.white)
							{
								monobitView.RPC("Draw7", MonobitTargets.All, hit.textureCoord * 256);
							}
						}
						if (lineWidth == 2)
						{
							if (lineColor == Color.black)
							{
								monobitView.RPC("Draw8", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.red)
							{
								monobitView.RPC("Draw9", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.blue)
							{
								monobitView.RPC("Draw10", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.green)
							{
								monobitView.RPC("Draw11", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.yellow)
							{
								monobitView.RPC("Draw12", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.magenta)
							{
								monobitView.RPC("Draw13", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.white)
							{
								monobitView.RPC("Draw14", MonobitTargets.All, hit.textureCoord * 256);
							}
						}
						if (lineWidth == 4)
						{
							if (lineColor == Color.black)
							{
								monobitView.RPC("Draw15", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.red)
							{
								monobitView.RPC("Draw16", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.blue)
							{
								monobitView.RPC("Draw17", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.green)
							{
								monobitView.RPC("Draw18", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.yellow)
							{
								monobitView.RPC("Draw19", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.magenta)
							{
								monobitView.RPC("Draw20", MonobitTargets.All, hit.textureCoord * 256);
							}
							else if (lineColor == Color.white)
							{
								monobitView.RPC("Draw21", MonobitTargets.All, hit.textureCoord * 256);
							}
						}
                	}

                	drawTexture.SetPixels(buffer);
                	drawTexture.Apply();
                	GetComponent<Renderer>().material.mainTexture = drawTexture;
            	}

            	//前回の入力座標を記録
            	_prevPosition = Input.mousePosition;
        	}
        	else
        	{
            	//前回の入力座標をリセット
            	_prevPosition = Vector2.zero;
        	}
		}
	}
}