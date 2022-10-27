using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase<T> : ScriptableObject where T : class
{
    public string Item_name;
    public int Item_code;

    public abstract void OnAction(T target);
}
