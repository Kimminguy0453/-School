using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Open;

    [SerializeField] private bool Lock;
    public bool getLock { get => Lock; }

    //프로퍼티로 get을 만들어주는 이유는 해당 변수의 값을 다른곳에서 가져갈수는 있어도 수정은 못하게 하기 위해.
    [SerializeField] private Animator anime;
    public Animator aime { get => anime; }

    [SerializeField] private string NeedItem;
    public string getNeedItem { get => NeedItem; }

    public string GetText()
    {
        if (Lock)
            return NeedItem + "이 필요합니다";
        return "문열기/문 닫기";
    }

    public void OnAction()
    {
        Lock = false;
        aime.SetBool("open", Open);
    }


}
