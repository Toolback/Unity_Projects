
using UnityEngine;

// T = generic or type                                      ensure that any classes are of type monobehavior 
public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T:MonoBehaviour
{
    // make singleton globaly accessible
    private static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    // protected can be accessed by inheriting classes
    // virtuel can be override by other instances
    protected virtual void Awake()
    {
        // if an instance doesn't exist, then set the instance to equal this object as type T
        if (instance == null)
        {
            instance = this as T;
        }
        else // if theres already one created, then destroy the gameobject related to this instance (new extra one created non desired // only one instance required)
        {
            Destroy(gameObject);
        }
    }
}
