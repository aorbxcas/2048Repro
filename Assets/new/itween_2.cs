using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///
///</summary>

public class itween_2 : MonoBehaviour 
{
    public iTween.EaseType type;
    public float speed;
    public Button a;
    public Image b;
    public void DoMove()
    {
        iTween.MoveTo(a.gameObject, iTween.Hash(
            "position", b.transform.position,
            "speed", speed,
            "easetype", type
            ));

    }




}
