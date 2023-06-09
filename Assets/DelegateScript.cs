using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateScript : MonoBehaviour
{
    public delegate void OnFuncType();
    OnFuncType onValueChange;
    void Print1()
    {
        print("1");
    }
    void Print2()
    {
        print("2");
    }

    private void Start()
    {
        onValueChange += Print1;
        onValueChange += Print2;
        onValueChange();
        onValueChange -= Print1;


    }
}
