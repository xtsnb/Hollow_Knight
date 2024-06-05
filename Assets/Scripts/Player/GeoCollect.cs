using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeoCollect : MonoBehaviour
{
    [SerializeField] Animator collectAni; // 收集动画
    [SerializeField] AudioClip[] geoCollect; // 地缘结晶音效数组
    [SerializeField] int geoCount = 0; // 地缘结晶数量
    [SerializeField] Text geoText; // 显示地缘结晶数量的文本

    private AudioSource audioSource; // 音频源

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 获取音频源组件

        geoText.text = geoCount.ToString(); // 将初始地缘结晶数量显示在文本中
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰撞到的对象属于 Geo 层
        if (collision.gameObject.layer == LayerMask.NameToLayer("Geo"))
        {
            // 播放收集动画
            collectAni.SetTrigger("Collect");

            // 在 geoCollect 数组中随机选择一个音频播放
            int index = Random.Range(0, geoCollect.Length);
            audioSource.PlayOneShot(geoCollect[index]); // 播放选定的音频

            geoCount++; // 地缘结晶数量加一
            geoText.text = geoCount.ToString(); // 更新地缘结晶数量的文本显示
            Destroy(collision.gameObject); // 销毁碰撞到的地缘结晶对象
        }
    }
}
