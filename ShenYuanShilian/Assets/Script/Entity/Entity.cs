using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int id;
    public int level;
    public int characterId;
    public int characterPos = 0;
    public int goodId;

    protected virtual void Awake()
    {
        // EventManager.Instance.AddListener(EventName.CharacterMove, CheckPlayerPos);
        EventManager.Instance.AddListener(EventName.CharacterResurge, Resurge);
    }

    protected virtual void OnDestroy()
    {
        // EventManager.Instance.RemoveListener(EventName.CharacterMove, CheckPlayerPos);
        EventManager.Instance.RemoveListener(EventName.CharacterResurge, Resurge);
    }

    public virtual void Init(int _id, int _level, int _characterId)
    {
        id = _id;
        level = _level;
        characterId = _characterId;
        characterPos = 0;
    }

    protected virtual void CheckPlayerPos(object sender, EventArgs e)
    {
        CharacterMoveEventArgs args = e as CharacterMoveEventArgs;
        characterPos = args.pos;
    }

    void OnBecameInvisible()
    {
        if (characterPos > id + 5)
        {
            if (gameObject != null && gameObject.activeInHierarchy)
            {
                ObjectPool.Instance.CollectObject(gameObject);
            }
        }
    }

    protected virtual void Resurge(object sender, EventArgs e)
    {
    }
}
