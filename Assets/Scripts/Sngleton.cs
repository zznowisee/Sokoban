using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        Instance = (T)this;
    }
}

