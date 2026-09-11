using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    public void show() 
    {
        Gamemanager._instance.stars();//_instance 是可以调用gamemanager的函数的
    }
}
