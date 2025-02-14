using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Console2048;
using UnityEngine.EventSystems;
using MoveDirection = Console2048.MoveDirection;

/// <summary>
/// 
/// </summary>
public class GameController : MonoBehaviour,IPointerDownHandler,IDragHandler
{
    private GameCore core;
    private NumberSprite[,] spriteActionArray;
    private Text score;
    private Text finalScore;
    private GameObject overImage;

    //一、创建方格
    private void Start()
    {
        Screen.SetResolution(768, 1024, true);
        overImage = GameObject.Find("OverImage");
        finalScore = GameObject.FindGameObjectWithTag("finalScore").GetComponent<Text>();
        overImage.SetActive(false);
        core = new GameCore();
        spriteActionArray = new NumberSprite[4, 4];
        score = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();


        Init();
        GenerateNumber();
        GenerateNumber();
    } 

    private void Init()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                spriteActionArray[r, c] = CreateSprite(r, c);
            }
        }
    } 

    private NumberSprite CreateSprite(int r,int c)
    {
        GameObject go = new GameObject(r.ToString() + c.ToString());
        go.AddComponent<Image>();
        go.transform.SetParent(transform,false);
        var script = go.AddComponent<NumberSprite>();
        script.SetImage(0);
        return script;
    }

    //二、生成新数字
    private void GenerateNumber()
    { 
        Location loc;
        int number;
        core.GenerateNumber(out loc, out number);
        spriteActionArray[loc.RIndex, loc.CIndex].SetImage(number);
        spriteActionArray[loc.RIndex, loc.CIndex].CreateEffect();
    }
      
    //四、输入检测 
    private Vector2 beginPoint;
    private bool isDown = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        beginPoint = eventData.position;
        isDown = true;
    }
    //拖拽时每帧执行
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDown) return;
        Vector3 touchOffset = eventData.position - beginPoint;
        float x = Mathf.Abs(touchOffset.x);
        float y = Mathf.Abs(touchOffset.y);
        MoveDirection? dir = null;
        if (x > y && x >50)
        {
            dir = touchOffset.x > 0 ? MoveDirection.Right : MoveDirection.Left;
        }
        if (y > x && y >50)
        {
            dir = touchOffset.y > 0 ? MoveDirection.Up : MoveDirection.Down;
        }

        if (dir != null)
        {
            core.Move(dir.Value);
            isDown = false;
        } 
    }

    //三、更新界面
    private void Update()
    {
        if (core.IsChange)
        {
            int myScore = int.Parse(score.text);
            score.text = (myScore + 2).ToString();
            //更新界面
            UpdateMap();
            GenerateNumber();
            if (core.IsOver())
            {
                overImage.SetActive(true);
                finalScore.text = score.text;
            }
            core.IsChange = false;
        }
    }

    private void UpdateMap()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                spriteActionArray[r, c].SetImage(core.Map[r, c]);
            }
        }
    }
}
