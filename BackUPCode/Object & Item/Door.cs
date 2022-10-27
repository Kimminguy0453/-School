using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Open;

    [SerializeField] private bool Lock;
    public bool getLock { get => Lock; }

    //������Ƽ�� get�� ������ִ� ������ �ش� ������ ���� �ٸ������� ���������� �־ ������ ���ϰ� �ϱ� ����.
    [SerializeField] private Animator anime;
    public Animator aime { get => anime; }

    [SerializeField] private string NeedItem;
    public string getNeedItem { get => NeedItem; }

    public string GetText()
    {
        if (Lock)
            return NeedItem + "�� �ʿ��մϴ�";
        return "������/�� �ݱ�";
    }

    public void OnAction()
    {
        Lock = false;
        aime.SetBool("open", Open);
    }


}
