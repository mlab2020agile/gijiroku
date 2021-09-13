using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MonobitEngine;

public class camerapanel : MonobitEngine.MonoBehaviour
{
    void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}
