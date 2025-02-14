using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 数字精灵，附加到每个方格中，定义每个方格行为
/// </summary>
public class NumberSprite : MonoBehaviour
{
    private Image img;
    private void Awake()
    {
        img = GetComponent<Image>();
    }
   
    public void SetImage(int number)
    { 
        //0   -->   精灵 查找资源
        img.sprite = ResourceManager.GetImage(number);
    }

    public void CreateEffect()
    { 
        //小 --> 大
        iTween.ScaleFrom(gameObject, Vector3.zero, 0.3f);
    }
 
}
