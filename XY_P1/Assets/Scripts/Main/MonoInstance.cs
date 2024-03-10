using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    // Start is called before the first frame update
    protected static T instance;
    public static T ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }
    }
    void Awake()
    {
        instance = this as T;
    }
}
