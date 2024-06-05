using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Enemy
{
    bool forceMovement = true; // 是否强制移动
    private Rigidbody2D rigi; // Rigidbody2D 组件的引用

    public Collider2D facingDetector; // 用于检测前方障碍物的碰撞器
    public ContactFilter2D contact; // 指定检测对象的过滤器

    public GameObject groundCheck; // 用于检查是否接触地面的 GameObject

    public float hurtForce; // 受伤时施加的力
    public float deadForce; // 死亡时施加的力

    public int circleRadius; // 地面检查的半径
    public LayerMask ground; // 用于指定什么是地面的 LayerMask

    bool isGrounded; // 是否接触地面

    public float movementSpeed; // 爬虫的移动速度

    private Animator ani; // Animator 组件的引用

    // Start 是在第一帧更新前调用的
    void Start()
    {
        ani = GetComponent<Animator>(); // 获取附加到 GameObject 的 Animator 组件
        rigi = GetComponent<Rigidbody2D>(); // 获取附加到 GameObject 的 Rigidbody2D 组件
    }

    // Update 每帧调用一次
    void Update()
    {
        CheckIsDead(); // 检查爬虫是否死亡
        Direction(); // 确定爬虫应该移动的方向
        Movement(); // 处理爬虫的移动
        FacingDetect(); // 检测前方障碍物和地面，决定是否需要翻转
    }

    private void Movement()
    {
        if (!isDead && forceMovement) // 如果爬虫未死亡且需要强制移动
        {
            if (isFacingRight) // 如果爬虫面向右
            {
                rigi.velocity = Vector2.right * movementSpeed; // 向右移动
            }
            else // 如果爬虫面向左
            {
                rigi.velocity = Vector2.left * movementSpeed; // 向左移动
            }
        }
    }

    private void FacingDetect()
    {
        if (isDead) // 如果爬虫已死亡，则返回
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, ground); // 检查爬虫是否接触地面

        if (!isGrounded) // 如果爬虫未接触地面
        {
            Flip(); // 翻转爬虫方向
        }
        else
        {
            int count = Physics2D.OverlapCollider(facingDetector, contact, new List<Collider2D>()); // 检查前方是否有障碍物

            if (count > 0) // 如果前方有障碍物
            {
                Flip(); // 翻转爬虫方向
            }
        }
    }

    private void Flip()
    {
        Vector3 vector = transform.localScale; // 获取爬虫当前的局部缩放
        vector.x *= -1; // 翻转 x 轴的缩放
        transform.localScale = vector; // 应用翻转后的缩放
    }

    public override void Hurt(int damage, Transform attackPosition)
    {
        base.Hurt(damage); // 调用基类的 Hurt 方法

        Vector2 vector = transform.position - attackPosition.position; // 计算攻击的方向

        StartCoroutine(DelayHurt(vector)); // 启动 DelayHurt 协程
    }

    IEnumerator DelayHurt(Vector2 vector)
    {
        rigi.velocity = Vector2.zero; // 停止爬虫的移动
        forceMovement = false; // 禁止爬虫移动

        if (vector.x > 0) // 如果攻击来自左侧
        {
            rigi.AddForce(new Vector2(hurtForce, 0), ForceMode2D.Impulse); // 向右施加力
        }
        else // 如果攻击来自右侧
        {
            rigi.AddForce(new Vector2(-hurtForce, 0), ForceMode2D.Impulse); // 向左施加力
        }

        yield return new WaitForSeconds(0.3f); // 等待0.3秒
        forceMovement = true; // 允许爬虫再次移动
    }

    protected override void Dead()
    {
        base.Dead(); // 调用基类的 Dead 方法
        StartCoroutine(DelayDead()); // 启动 DelayDead 协程
    }

    IEnumerator DelayDead()
    {
        Vector3 diff = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized; // 计算朝向玩家的方向
        rigi.velocity = Vector2.zero; // 停止爬虫的移动

        if (diff.x < 0) // 如果玩家在右侧
        {
            rigi.AddForce(Vector2.right * deadForce, ForceMode2D.Impulse); // 向右施加力
        }
        else // 如果玩家在左侧
        {
            rigi.AddForce(Vector2.left * deadForce, ForceMode2D.Impulse); // 向左施加力
        }

        if (ani != null) // 如果 Animator 组件不为空
        {
            ani.SetBool("Dead", true); // 设置 "Dead" 参数为 true
        }

        yield return new WaitForSeconds(0.2f); // 等待0.2秒
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // 将 Rigidbody2D 设置为静态
        GetComponent<BoxCollider2D>().enabled = false; // 禁用 BoxCollider2D 组件
    }
}
