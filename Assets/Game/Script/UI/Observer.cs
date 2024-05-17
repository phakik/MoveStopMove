using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Observer : Singleton<Observer>
{
    private Dictionary<string, List<Action<object>>> _listeners = new Dictionary<string, List<Action<object>>>();
    public bool AddListener(string key, Action<object> value)
    {
        List<Action<object>> action = new List<Action<object>>();
        if (_listeners.ContainsKey(key))
        {
            if (_listeners[key].Equals(null))
            {
                action = _listeners[key];
            }
        }
        else
        {
            _listeners.TryAdd(key, action);
        }
        _listeners[key].Add(value);
        return true;
    }
    public void Notify(string key, object value)
    {
        if (_listeners.ContainsKey(key))
        {
            foreach (Action<object> item in _listeners[key])
            {
                try
                {
                    item?.Invoke(value);
                }
                catch (Exception e)
                {

                    Debug.LogError("invoke action fail!:" + e.ToString());
                }
            }
            return;
        }
        Debug.LogErrorFormat("listener {0} not exist", key);
    }
    public void RemoveListener(string key, Action<object> value)
    {
        List<Action<object>> action = new List<Action<object>>();
        if (_listeners.ContainsKey(key))
        {
            action = _listeners[key];
        }
        else
        {
            _listeners.TryAdd(key, action);
        }
        _listeners[key].Remove(value);

    }
}
