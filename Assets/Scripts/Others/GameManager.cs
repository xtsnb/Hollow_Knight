using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool enableInput = false; // 是否允许输入的标志位

    // 返回 enableInput 标志位的状态
    public bool IsEnableInput()
    {
        return enableInput;
    }

    // 设置 enableInput 标志位的状态
    public void SetEnableInput(bool enabled)
    {
        enableInput = enabled;
    }
}
