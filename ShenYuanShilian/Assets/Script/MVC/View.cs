using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract string Name { get; }
    public List<string> attractEventList = new(); //????????б?

    /// <summary>
    /// ??????
    /// </summary>
    public virtual void RegisterViewEvent() { }

    /// <summary>
    /// ??????
    /// </summary>
    public virtual void UnregisterViewEvent() { }

    /// <summary>
    /// ???????
    /// </summary>
    /// <param name="eventName">?????</param>
    /// <param name="data">???????</param>
    public virtual void SendViewEvent(string eventName, object data = null)
    {
        Mvc.SendEvent(eventName, data);
    }

    /// <summary>
    /// ???????
    /// </summary>
    /// <param name="data">???????</param>
    public abstract void HandleEvent(object data = null);

    /// <summary>
    /// ??????
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetModel<T>() where T : Model
    {
        return Mvc.GetModel<T>();
    }

    /// <summary>
    /// ?????????????
    /// </summary>
    /// <param name="eventName">???????</param>
    /// <returns></returns>
    public bool ContainEvent(string eventName)
    {
        foreach (var item in attractEventList)
        {
            if (item == eventName)
                return true;
        }
        return false;
    }
}
