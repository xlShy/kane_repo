using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuFader : MonoBehaviour
{
    public UnityEvent OnGoMenu;
    void Start()
    {
        OnGoMenu?.Invoke();
    }
}
