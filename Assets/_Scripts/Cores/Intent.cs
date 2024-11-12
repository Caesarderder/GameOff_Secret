
using System.Collections.Generic;

public class Intent
{
    private Dictionary<string, int> intParams = new Dictionary<string, int>(2);
    private Dictionary<string, float> floatParams = new Dictionary<string, float>(2);
    private Dictionary<string, bool> boolParams = new(2);
    private Dictionary<string, object> objParams = new Dictionary<string, object>(2);

    public static Intent Get()
    {
        return new Intent();
    }
    public int GetInt(string key)
    {
        return intParams.TryGetValue(key, out var p) ? p : 0;
    }

    public float GetFloat(string key)
    {
        return floatParams.TryGetValue(key, out var p) ? p : 0;
    }

    public bool GetBool(string key)
    {
        return boolParams.TryGetValue(key, out var p) && p;
    }

    public object GetObject(string key)
    {
        return objParams.TryGetValue(key, out var p) ? p : null;
    }

    public bool TryGetValue(string key, out bool value)
    {
        return boolParams.TryGetValue(key, out value);
    }

    public bool TryGetValue(string key, out int value)
    {
        return intParams.TryGetValue(key, out value);
    }
    public bool TryGetValue<T>(string key, out T value)
    {
        bool contains = objParams.TryGetValue(key, out var obj);
        if ( contains && obj is T result )
        {
            value = result;
            return true;
        }
        else
        {
            value = default(T);
            return false;
        }
    }
    public void AddInt(string key, int value)
    {
        if ( intParams.TryGetValue(key, out var i) )
        {
            intParams[key] = value;
        }
        else
        {
            intParams.Add(key, value);
        }
    }
    public void AddFloat(string key, float value)
    {
        if ( floatParams.TryGetValue(key, out var i) )
        {
            floatParams[key] = value;
        }
        else
        {
            floatParams.Add(key, value);
        }
    }
    public void AddBool(string key, bool value)
    {
        if ( boolParams.ContainsKey(key) )
        {
            boolParams[key] = value;
        }
        else
        {
            boolParams.Add(key, value);
        }
    }
    public void AddObject(string key, object value)
    {
        if ( objParams.TryGetValue(key, out var i) )
        {
            objParams[key] = value;
        }
        else
        {
            objParams.Add(key, value);
        }
    }
    public void RemoveInt(string key)
    {
        intParams.Remove(key);
    }
    public void RemoveFloat(string key)
    {
        floatParams.Remove(key);
    }

    public void RemoveBool(string key)
    {
        boolParams.Remove(key);
    }
    public void RemoveObject(string key)
    {
        objParams.Remove(key);
    }

    public void Clear()
    {
        intParams.Clear();
        floatParams.Clear();
        boolParams.Clear();
        objParams.Clear();
    }
}