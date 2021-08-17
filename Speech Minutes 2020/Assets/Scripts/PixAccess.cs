﻿using System.Collections;
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

	private Vector2 _prevPosition;

	public void Start()
	{
		Texture2D mainTexture = (Texture2D)GetComponent<Renderer>().material.mainTexture;
		Color[] pixels = mainTexture.GetPixels();

		buffer = new Color[pixels.Length];
		pixels.CopyTo(buffer, 0);

		drawTexture = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
		drawTexture.filterMode = FilterMode.Point;

		PenMode = GameObject.Find("PenMode");
		itakire = GameObject.Find("Plane");

		mode = true;
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
	/// <summary>
	/// 初期化
	/// </summary>
	[MunRPC]
	public void Clear()
	{

		Texture2D mainTexture = (Texture2D)GetComponent<Renderer>().material.mainTexture;
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
					buffer.SetValue(Color.white, x + 256 * y);
				}
			}
		}
	}


	public void Clearfjag()
	{
		monobitView.RPC("Clear", MonobitTargets.All);
    }
	/// <summary>
	/// 太さ変更
	/// </summary>
	/// <param name="p"></param>
	[MunRPC]
	public void Draw(Vector2 p)
	{


		if (lineWidth%2 == 1)
		{
			for (int x = (int)p.x-(int)(lineWidth/2); x < (int)p.x+(int)(lineWidth/2)+1; x++)
			{
				for (int y = (int)p.y-(int)(lineWidth/2); y < (int)p.y+(int)(lineWidth/2)+1; y++)
				{
					buffer.SetValue(lineColor, x + 256 * y);
				}
			}
		}
		else
		{
			for (int x = (int)p.x-(int)(lineWidth/2); x < (int)p.x+(int)(lineWidth/2); x++)
			{
				for (int y = (int)p.y-(int)(lineWidth/2); y < (int)p.y+(int)(lineWidth/2); y++)
				{
					buffer.SetValue(lineColor, x + 256 * y);
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
                	    monobitView.RPC("Draw", MonobitTargets.All, hit.textureCoord * 256);
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