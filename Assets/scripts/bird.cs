using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    //画线操作
    public LineRenderer right;
    public Transform rightpos;//这个要在Unity中属性的位置将对象名称拖到变量位置赋值  画线共用
    public LineRenderer left;
    public Transform leftpos;
    public GameObject boom;
    private testmytrail mytrail;

    public float maxdis = 1;//拖拽最大距离
    private bool isclick = false;//控制鼠标按下后鸟跟着鼠标走
    public float smooth = 3;

    public AudioClip select;
    public AudioClip Fly;


    private Rigidbody2D rg;
    [HideInInspector] //public sp 在面板看不见
    public SpringJoint2D sp;
    private void Awake()//获取函数
    {
        sp = GetComponent<SpringJoint2D>();//获取2d命名为SP
        rg = GetComponent<Rigidbody2D>();
        mytrail = GetComponent<testmytrail>();
    }
    private void Update()//这个函数是随时变化的时候可调用的
    {
        if (isclick)//如果鼠标一直按下则进行位置的跟随
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);//去掉z轴的影响
                                                        //转成世界坐标

            //限定范围
            if (Vector3.Distance(transform.position, rightpos.position) > maxdis)//位置限定算距离
            {
                //获得鸟和弹弓的距离差值最大不超过一定距离

                Vector3 pos = (transform.position - rightpos.position).normalized;//单位化向量,明确方向
                pos *= maxdis;//最大长度向量
                              //一个得到方向，一个得到向量长度
                transform.position = pos + rightpos.position;//以右边弹弓为原点,加上向量长度为鸟的最大位置
            }
            //画线
            line();
        }
        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 20),
            Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime);

        //平滑的运动x  当前目标点，目的地，速度平滑
        //差值0~15

    }
    //一个鼠标控制条件的引用

    private void OnMouseDown() //鼠标按下
    {
        isclick = true;
        rg.isKinematic = true;//开启动力学
        audioplay(select);

    }
    private void OnMouseUp() //鼠标抬起
    {
        isclick = false;
        rg.isKinematic = false;
        Invoke("fly", 0.1f);//延时函数，要延时0.1s后才弹簧失活.在0.1s之间受到弹簧物理计算的影响
        lined();
    }
    void lined()
    {
        left.enabled = false;
        right.enabled = false;
    }
    void fly()
    {
        audioplay(Fly);
        mytrail.StartTrails();
        sp.enabled = false;//让弹簧失活
        Invoke("next", 3);//3s后调用
    }
    void next()
    {
        Gamemanager._instance.birds.Remove(this);//可以利用gamemanager中的birds变量
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);//生成的函数

        Gamemanager._instance.nextbird();//判断结局
    }


    void line()//画线操作函数
    {
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightpos.position);
        right.SetPosition(1, transform.position);
        left.SetPosition(0, leftpos.position);
        left.SetPosition(1, transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        mytrail.ClearTrails();
    }
    public void audioplay(AudioClip clip)//播放音乐
    {
        AudioSource.PlayClipAtPoint(clip,transform.position);//在当前位置播放
    }

}
