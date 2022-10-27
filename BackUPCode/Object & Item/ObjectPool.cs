using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool<T> where T : MonoBehaviour, IPoolingObject
{
    [SerializeField] T targetObject;
    [SerializeField] [Range(1, 100)] int poolingAmount = 1;
    Transform containerObject;
    Queue<T> objectPool;
    object initData = null;

    public bool Initialize(object value)
    {
        if (!targetObject || containerObject) 
            return false;

        if (1 > poolingAmount) 
            poolingAmount = 1;
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //참고 https://coding-groot.tistory.com/53
        //string은 변경 할 수 없는 형식
        //string을 변경하면 변경된 문자열을 위한 새로운 string이 만들어져서 성능 저하가 일어남.
        //이를 해결하기 위해 변경가능한 문자열 클래스인 stringBuilder를 사용.
        sb.Append(targetObject.name);
        containerObject = new GameObject(sb.ToString()).transform;
        objectPool = new Queue<T>();
        initData = value;
        MakeAndPooling();
        
        return true;
    }

    bool MakeAndPooling()
    {
        if (!containerObject) 
            return false;
        T poolObject;
        for (int i = 0; poolingAmount > i; i++)
        {
            poolObject = MonoBehaviour.Instantiate(targetObject, containerObject);
            poolObject.name = targetObject.name;
            poolObject.Initialize(initData);
            poolObject.gameObject.SetActive(false);
            objectPool.Enqueue(poolObject);
        }
        return true;
    }

    public bool GetObject(out T item)
    {
        item = null;
        if (!containerObject) 
            return false;
        if (0 >= objectPool.Count)
        {
            if (!MakeAndPooling()) 
                return false;
        }
        item = objectPool.Dequeue();
        item.gameObject.SetActive(true);
        return true;
    }

    /// <summary> 해당 풀이 가지는 아이템이 맞는지 확인. </summary>
    public bool CheckItem(T item)
    {
        if (!targetObject) 
            return false;
        return targetObject.name.Equals(item.name);
    }

    /// <summary> item을 비활성화 시키고 Pool에 넣는다. </summary>
    public bool PutInPool(T item)
    {
        if (!(item && containerObject)) 
            return false;

        item.gameObject.SetActive(false);
        objectPool.Enqueue(item);
        return true;
    }

    public bool Destroy()
    {
        if (!containerObject) 
            return false;
        MonoBehaviour.Destroy(containerObject.gameObject);
        containerObject = null;
        objectPool.Clear();
        objectPool = null;
        return true;
    }

    public void ReturnBackPool()
    {
        if (containerObject)
        {
            // 모든 자식을 순회 한다.
            foreach (Transform child in containerObject)
            {
                if (child.gameObject.activeSelf)
                {
                    if (child.TryGetComponent(out T item)) 
                        PutInPool(item);
                }
            }
        }
    }
} // class ObjectPoo