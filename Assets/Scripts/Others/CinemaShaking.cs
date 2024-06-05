using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaShaking : MonoBehaviour
{
    // 声明一个 CinemachineImpulseSource 类型的变量 myImpulse
    private Cinemachine.CinemachineImpulseSource myImpulse;

    // 在 Start 方法中初始化 myImpulse 变量
    private void Start()
    {
        // 获取当前物体上的 CinemachineImpulseSource 组件
        myImpulse = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

    // 定义一个公共方法 CinemaShake，用于触发震动效果
    public void CinemaShake()
    {
        // 生成震动冲击
        myImpulse.GenerateImpulse();
    }
}
