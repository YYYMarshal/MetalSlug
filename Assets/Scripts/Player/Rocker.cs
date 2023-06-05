using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rocker : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 initialPos;     //临时坐标（保存摇杆最初的位置）
    //摇杆按钮的小图片能够移动的最大长度（受radiusScale 和 父元素图片的宽度 的影响）
    private float maxLength;
    private readonly float radiusScale = 1.25f;

    //得到偏移量
    public Vector2 offset;

    void Start()
    {
        initialPos = GetComponent<RectTransform>().position;
        //摇杆按钮的小图片能够移动的范围为一个圆形：
        //以其父元素（即摇杆的背景图）的中心点为(0,0)
        //半径为 其父元素的宽度 的一半 * radiusScale
        maxLength = transform.parent.GetComponent<RectTransform>().rect.width / 2.0f * radiusScale;
    }
    private void Update()
    {
        Debug.Log(offset);
    }

    public void OnDrag(PointerEventData evertData)
    {
        //如果鼠标的位置在 摇杆的背景图(即小图片的父物体) 的缩放范围内
        if (Vector3.Distance(Input.mousePosition, initialPos) < maxLength)
            transform.position = Input.mousePosition;       //把鼠标的位置给摇杆的小按钮图片
        else
        {
            //得到一个指向
            Vector3 dir = Input.mousePosition - initialPos;
            transform.position = initialPos + dir.normalized * maxLength;
        }
        //得到偏移量
        Vector3 temp = new Vector2(transform.position.x - initialPos.x, transform.position.y - initialPos.y);
        offset.x = temp.x / maxLength;
        offset.y = temp.y / maxLength;
    }
    public void OnEndDrag(PointerEventData evertData)
    {
        transform.position = initialPos;
        offset = Vector2.zero;
    }
}