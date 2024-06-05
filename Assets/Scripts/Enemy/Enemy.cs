using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Breakable
{
    public GameObject coin; // 掉落的金币预制体
    int randomCount; // 随机生成的金币数量

    [SerializeField] protected int minSpawnCoins = 2; // 最小生成金币数量
    [SerializeField] protected int maxSpawnCoins = 5; // 最大生成金币数量
    [SerializeField] protected float maxBumpXForce = 100; // X轴方向上施加的最大力
    [SerializeField] protected float minBumpYForce = 300; // Y轴方向上施加的最小力
    [SerializeField] protected float maxBumpYForce = 500; // Y轴方向上施加的最大力

    protected bool isFacingRight; // 是否面向右侧

    // 确定敌人的方向
    protected void Direction()
    {
        if (transform.localScale.x == 1)
        {
            isFacingRight = true; // 面向右侧
        }
        else if (transform.localScale.x == -1)
        {
            isFacingRight = false; // 面向左侧
        }
    }

    // 碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectCollisionEnter2D(collision); // 调用碰撞检测方法
    }

    // 碰撞检测的具体实现
    protected virtual void DetectCollisionEnter2D(Collision2D collision)
    {
        // 如果与“HeroDetector”层碰撞
        if (collision.gameObject.layer == LayerMask.NameToLayer("HeroDetector"))
        {
            FindObjectOfType<PlayerController>().TakeDamage(); // 让玩家受到伤害
            FindObjectOfType<HitPause>().Stop(0.5f, 0.0f); // 暂停游戏0.5秒
        }
        // 如果敌人已经死亡且与“Terrain”层碰撞
        if (isDead && collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // 将刚体设置为静态
            GetComponent<BoxCollider2D>().enabled = false; // 禁用碰撞器
        }
    }

    // 敌人死亡的处理
    protected override void Dead()
    {
        base.Dead(); // 调用基类的死亡方法
        SpawnCoins(); // 生成金币
    }

    // 生成金币的方法
    public virtual void SpawnCoins()
    {
        randomCount = Random.Range(minSpawnCoins, maxSpawnCoins); // 随机生成金币数量
        for (int i = 0; i < randomCount; i++)
        {
            GameObject geo = Instantiate(coin, transform.position, Quaternion.identity, transform.parent); // 实例化金币
            Vector2 force = new Vector2(Random.Range(-maxBumpXForce, maxBumpXForce), Random.Range(minBumpYForce, maxBumpYForce)); // 随机施加力
            geo.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse); // 将力施加到金币上
        }
    }
}
