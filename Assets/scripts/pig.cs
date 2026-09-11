using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float maxspeed = 10;//碰撞速度设置值
    public float minspeed = 5;
    
    private SpriteRenderer render;
    public Sprite hurt;//为受伤猪设置一个变量承接图片
    
    public GameObject boom;
    public GameObject pigscore;
   
    public bool ispig=false;
    
    public AudioClip pengzhuang;
    public AudioClip deaded;
    public AudioClip birdpengzhuang;


    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision) //碰撞检测 相对速度
    {
        if (collision.gameObject.tag == "player")
        {
            audioplay(birdpengzhuang);        
        }


        if(collision.relativeVelocity.magnitude > maxspeed)//相对速度超过maxspeed 猪没
        {
            dead();
        }
        if(collision.relativeVelocity.magnitude > minspeed && collision.relativeVelocity.magnitude < maxspeed)
        //猪受伤
        {
            render.sprite = hurt;
            audioplay(pengzhuang);
        }
       
    }
    void dead()
    {
        if (ispig) 
        {
            Gamemanager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom,transform.position,Quaternion.identity);
        audioplay(deaded);//music
        GameObject score = Instantiate(pigscore, transform.position + new Vector3(0,0.65f,0), Quaternion.identity);
        Destroy(score, 1.0f);//猪的分数1.5s内出现后结束
    }
    public void audioplay(AudioClip clip)//播放音乐
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);//在当前位置播放,与destroy不在一个平台上，只是调用音乐罢了
    }

}
