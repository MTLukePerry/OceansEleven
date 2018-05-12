using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager
{
    private static List<MonoBehaviour> _singletons = new List<MonoBehaviour>();

    public static T GetInstance<T>() where T : MonoBehaviour
    {
        foreach (var singleton in _singletons)
        {
            if (singleton.GetType() == typeof(T))
            {
                return (T)singleton;
            }
        }

        return null;
    }

    public static void RegisterSingleton<T>(T instance) where T : MonoBehaviour
    {
        bool existingSingleton = false;
        foreach (var singleton in _singletons)
        {
            if (singleton.GetType() == instance.GetType())
            {
                existingSingleton = true;
                break;
            }
        }

        if (!existingSingleton)
        {
            _singletons.Add(instance);
        }
        else
        {
            Debug.LogWarning("Trying to add a Singleton of type " + instance.GetType() + " when one already exists!");
        }
    }
}
