using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rocker : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 initialPos;     //��ʱ���꣨����ҡ�������λ�ã�
    //ҡ�˰�ť��СͼƬ�ܹ��ƶ�����󳤶ȣ���radiusScale �� ��Ԫ��ͼƬ�Ŀ��� ��Ӱ�죩
    private float maxLength;
    private readonly float radiusScale = 1.25f;

    //�õ�ƫ����
    public Vector2 offset;

    void Start()
    {
        initialPos = GetComponent<RectTransform>().position;
        //ҡ�˰�ť��СͼƬ�ܹ��ƶ��ķ�ΧΪһ��Բ�Σ�
        //���丸Ԫ�أ���ҡ�˵ı���ͼ�������ĵ�Ϊ(0,0)
        //�뾶Ϊ �丸Ԫ�صĿ��� ��һ�� * radiusScale
        maxLength = transform.parent.GetComponent<RectTransform>().rect.width / 2.0f * radiusScale;
    }
    private void Update()
    {
        Debug.Log(offset);
    }

    public void OnDrag(PointerEventData evertData)
    {
        //�������λ���� ҡ�˵ı���ͼ(��СͼƬ�ĸ�����) �����ŷ�Χ��
        if (Vector3.Distance(Input.mousePosition, initialPos) < maxLength)
            transform.position = Input.mousePosition;       //������λ�ø�ҡ�˵�С��ťͼƬ
        else
        {
            //�õ�һ��ָ��
            Vector3 dir = Input.mousePosition - initialPos;
            transform.position = initialPos + dir.normalized * maxLength;
        }
        //�õ�ƫ����
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