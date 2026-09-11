using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public List<bird> birds;
    public List<pig> pigs;
    public static Gamemanager _instance;//单例模式
    private Vector3 originalpos;//初始化位置
    public GameObject lose;
    public GameObject win;
    public GameObject[] star; 

    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            originalpos = birds[0].transform.position;
        }
    }
    private void Start()//只有在这个函数里面才能开始调用
    {
        Initialized();
    }

    private void Initialized() //这个是为了让后面的小鸟弹簧失活等第一只完事之后只在有弹簧
    { 
        for(int i=0; i<birds.Count;i++)
        {
            if (i == 0)//第一只小鸟 
            {
                birds[i].transform.position = originalpos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;//弹簧有效

            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;//弹簧无效

            }
        }
    
    }
    public void nextbird() //判断结局以及小鸟的替换和判断
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //飞飞
                Initialized();
            }
            else
            {
                lose.SetActive(true);
                //显示界面
            }

        }
        else 
        {
            win.SetActive(true);
            //赢了
        
        }

    
    }
    public void stars() 
    {
        StartCoroutine("show");
    
    }
    IEnumerator show()//星星一个一个出现
    {
        for (int i = 0; i < birds.Count + 1; i++)
        {
            yield return new WaitForSeconds(0.2f);
            star[i].SetActive(true);
        }
    }
    public void replay() 
    {
        SceneManager.LoadScene(2);//场景
    }
    public void home()
    {
        SceneManager.LoadScene(1);
    }
}
