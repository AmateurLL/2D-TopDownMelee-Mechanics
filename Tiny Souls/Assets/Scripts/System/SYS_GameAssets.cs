using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYS_GameAssets : MonoBehaviour
{

    public static SYS_GameAssets instance;

    [Header("Prefabs Load")]
    public GameObject m_DmgPopup;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
