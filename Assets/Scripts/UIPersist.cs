using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPersist : MonoBehaviour
{
    public static UIPersist OnlyPersistObject;

    private void Awake()
    {
        if (OnlyPersistObject != null)
        {
            Destroy(this.gameObject);
            return;
        }
        OnlyPersistObject = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
