using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mapselect : MonoBehaviour
{
    public AudioClip map;
    public Button button1;
    private void Update()
    {
        audioplay(map);
    }
    void Start()
    {
        button1.onClick.AddListener(SwitchScene);
    }
    void SwitchScene()
    {
       
        SceneManager.LoadScene(2);
    }
    public void audioplay(AudioClip clip)//播放音乐
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);//在当前位置播放
    }
}
