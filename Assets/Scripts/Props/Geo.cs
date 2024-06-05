using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geo : MonoBehaviour
{
    [SerializeField] private AudioClip[] geoHitGround; // 地缘结晶击中地面的音效数组

    AudioSource audioSource; // 音频源组件

    public bool isGround; // 是否已经接触地面

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // 获取音频源组件
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果尚未接触地面且碰撞对象是地形层的物体
        if (!isGround && collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            isGround = true; // 标记为已接触地面
            int index = Random.Range(0, geoHitGround.Length); // 在地缘结晶击中地面音效数组中随机选择一个音频
            audioSource.PlayOneShot(geoHitGround[index]); // 播放选定的音频
        }
    }
}
