using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Singleton instance is null.");
            }
            return instance;
        }
    }
}