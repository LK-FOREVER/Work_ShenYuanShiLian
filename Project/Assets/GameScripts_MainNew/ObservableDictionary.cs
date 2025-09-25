using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    public event Action OnChanged;

    public new void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        OnChanged?.Invoke();
    }

    public new TValue this[TKey key]
    {
        get => base[key];
        set
        {
            base[key] = value;
            OnChanged?.Invoke();
        }
    }

    public new bool Remove(TKey key)
    {
        var result = base.Remove(key);
        if (result) OnChanged?.Invoke();
        return result;
    }

}
