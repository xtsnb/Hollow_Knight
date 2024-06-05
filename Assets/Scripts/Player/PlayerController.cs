using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("附加调价")]
    [SerializeField] float hurtForce = 1f; // 受伤后玩家的推力
    [SerializeField] float moveSpeed = 10f; // 移动速度
    [SerializeField] float jumpForce = 1f; // 跳跃力
    [SerializeField] public float jumpTimer = 0.5f; // 跳跃持续时间
    [SerializeField] Vector3 flippedScale = new Vector3(-1, 1, 1); // 翻转后的缩放比例
    [SerializeField] int moveChangeAni; // 动画状态
    float moveX; // 水平移动
    float moveY; // 垂直移动
    [Header("调用组件脚本")]
    private CharacterEffect characterEffect; // 角色效果
    private CharacterAudio characterAudio; // 角色音效
    private Rigidbody2D rigi; // 刚体组件
    private Animator animator; // 动画组件
    private CinemaShaking cinemaShaking; // 画面震动
    private Attack attack; // 攻击组件
    private GameManager gameManager; // 游戏管理
    private AudioSource audio; // 音频组件

    [Header("判断条件")]
    bool isFacingRight; // 是否面向右
    bool isOnGround; // 是否在地面上
    bool canMove; // 是否可以移动
    [SerializeField] bool firstLanding; // 是否第一次着陆
    [Header("")]
    [SerializeField] float slashIntervalTime = 0.2f; // 连击间隔时间
    [SerializeField] float maxComboTime = 0.4f; // 最大连击时间
    [SerializeField] float recoilForce; // 后坐力
    [SerializeField] int slashCount; // 连击次数
    [SerializeField] int slashDamage = 1; // 斩击伤害
    [SerializeField] float downRecoilForce; // 向下攻击的后坐力
    float lastSlashTime; // 上次斩击的时间

    void Start()
    {
        characterEffect = FindObjectOfType<CharacterEffect>(); // 查找角色效果组件
        characterAudio = FindObjectOfType<CharacterAudio>(); // 查找角色音效组件
        attack = FindObjectOfType<Attack>(); // 查找攻击组件
        rigi = GetComponent<Rigidbody2D>(); // 获取刚体组件
        animator = GetComponent<Animator>(); // 获取动画组件
        gameManager = FindObjectOfType<GameManager>(); // 查找游戏管理组件
        cinemaShaking = FindObjectOfType<CinemaShaking>(); // 查找画面震动组件
        audio = GetComponent<AudioSource>(); // 获取音频组件
        canMove = true; // 设置可以移动
    }

    // 每帧更新一次
    void Update()
    {
        ResetComboTime(); // 重置连击时间
        Movement(); // 移动处理
        Direction(); // 方向处理
        Jump(); // 跳跃处理
        PlayerAttack(); // 攻击处理
        animator.SetBool("FirstLanding", firstLanding); // 设置是否第一次着陆
    }

    // 移动方法
    private void Movement()
    {
        // 获取水平方向和垂直方向输入
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // 如果可以移动并且游戏管理允许输入
        if (canMove && gameManager.IsEnableInput())
        {
            rigi.velocity = new Vector2(moveX * moveSpeed, rigi.velocity.y); // 设置刚体速度
        }

        // 更新面朝方向和动画状态
        if (moveX > 0)
        {
            isFacingRight = true;
            moveChangeAni = 1;
        }
        else if (moveX < 0)
        {
            isFacingRight = false;
            moveChangeAni = -1;
        }
        else moveChangeAni = 0;

        animator.SetInteger("movement", moveChangeAni); // 设置动画参数
        animator.SetFloat("VelocityY", rigi.velocity.y); // 设置垂直速度参数
    }

    // 处理玩家的面朝方向
    private void Direction()
    {
        if (gameManager.IsEnableInput())
        {
            if (moveX > 0)
            {
                transform.localScale = flippedScale; // 翻转角色
            }
            else if (moveX < 0)
            {
                transform.localScale = Vector3.one; // 恢复原始缩放
            }
        }
    }

    // 跳跃方法
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (!gameManager.IsEnableInput())
                return;

            jumpTimer -= Time.deltaTime; // 跳跃计时

            if (jumpTimer > 0)
            {
                rigi.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force); // 添加跳跃力
                animator.SetTrigger("jump"); // 触发跳跃动画
                characterAudio.Play(CharacterAudio.AudioType.Jump, true); // 播放跳跃音效
            }
        }
    }

    // 碰撞进入事件
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Grouding(collision, false); // 处理落地检测
    }

    // 碰撞持续事件
    private void OnCollisionStay2D(Collision2D collision)
    {
        Grouding(collision, false); // 处理落地检测
    }

    // 碰撞退出事件
    private void OnCollisionExit2D(Collision2D collision)
    {
        Grouding(collision, true); // 处理离地检测
    }

    // 处理落地检测
    private void Grouding(Collision2D col, bool exitState)
    {
        if (exitState)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                isOnGround = false; // 设置未在地面上
        }
        else
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal == Vector2.up)
            {
                characterEffect.DoEffect(CharacterEffect.EffectType.FallTrail, true); // 播放落地效果
                isOnGround = true; // 设置在地面上
                JumpCancle(); // 取消跳跃
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal == Vector2.down)
            {
                JumpCancle(); // 取消跳跃
            }
        }
        animator.SetBool("isOnGround", isOnGround); // 设置动画参数
    }

    // 取消跳跃
    private void JumpCancle()
    {
        animator.ResetTrigger("jump"); // 重置跳跃动画
        jumpTimer = 0.5f; // 重置跳跃计时
    }

    // 处理玩家受伤
    public void TakeDamage()
    {
        cinemaShaking.CinemaShake(); // 画面震动
        StartCoroutine(FindObjectOfType<Invincibility>().SetInvincibility()); // 设置无敌时间
        FindObjectOfType<Health>().Hurt(); // 减少生命值

        if (isFacingRight)
        {
            rigi.velocity = new Vector2(1, 1) * hurtForce; // 受伤后推力
        }
        else
            rigi.velocity = new Vector2(-1, 1) * hurtForce;

        animator.Play("TakeDamage"); // 播放受伤动画
        characterAudio.Play(CharacterAudio.AudioType.TakeDamage, true); // 播放受伤音效
    }

    // 播放受击粒子效果
    public void PlayHitParticals()
    {
        characterEffect.DoEffect(CharacterEffect.EffectType.HitL, true); // 播放左侧受击效果
        characterEffect.DoEffect(CharacterEffect.EffectType.HitR, true); // 播放右侧受击效果
    }

    // 处理玩家攻击
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!gameManager.IsEnableInput())
                return;

            if (Time.time >= lastSlashTime + slashIntervalTime)
            {
                lastSlashTime = Time.time;

                if (moveY > 0)
                {
                    SlashAndDetect(Attack.AttackType.Upslash); // 向上斩击
                    animator.Play("UpSlash"); // 播放向上斩击动画
                }
                else if (!isOnGround && moveY < 0)
                {
                    SlashAndDetect(Attack.AttackType.DownSlash); // 向下斩击
                    animator.Play("DownSlash"); // 播放向下斩击动画
                }
                else
                {
                    slashCount++;
                    switch (slashCount)
                    {
                        case 1:
                            SlashAndDetect(Attack.AttackType.Slash); // 普通斩击
                            animator.Play("Slash"); // 播放普通斩击动画
                            break;
                        case 2:
                            SlashAndDetect(Attack.AttackType.AltSlash); // 替代斩击
                            animator.Play("AltSlash"); // 播放替代斩击动画
                            slashCount = 0;
                            break;
                    }
                }
            }
        }
    }

    // 重置连击时间
    private void ResetComboTime()
    {
        if (Time.time >= lastSlashTime + maxComboTime && slashCount != 0)
        {
            slashCount = 0; // 重置连击计数
        }
    }

    // 处理斩击和检测
    private void SlashAndDetect(Attack.AttackType attackType)
    {
        List<Collider2D> colliders = new List<Collider2D>();
        attack.Play(attackType, ref colliders);

        bool hasEnemy = false;
        bool hasDamagePlayer = false;

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("EnemyDetector"))
            {
                hasEnemy = true;
                break;
            }
        }

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("DamagePlayer"))
            {
                hasDamagePlayer = true;
                break;
            }
        }

        if (hasEnemy)
        {
            // 后坐力
            if (attackType == Attack.AttackType.DownSlash)
            {
                AddDownRecoilForce(); // 添加向下攻击后坐力
            }
            else
            {
                StartCoroutine(AddRecoilForce()); // 添加普通攻击后坐力
            }
        }

        if (hasDamagePlayer)
        {
            if (attackType == Attack.AttackType.DownSlash)
            {
                AddDownRecoilForce(); // 添加向下攻击后坐力
            }
        }

        foreach (Collider2D col in colliders)
        {
            Breakable breakable = col.GetComponent<Breakable>();
            if (breakable != null)
            {
                breakable.Hurt(slashDamage, transform); // 对可破坏物造成伤害
            }
        }
    }

    // 添加向下攻击后坐力
    private void AddDownRecoilForce()
    {
        rigi.velocity.Set(rigi.velocity.x, 0);
        rigi.AddForce(Vector2.up * downRecoilForce, ForceMode2D.Impulse);
    }

    // 添加普通攻击后坐力
    IEnumerator AddRecoilForce()
    {
        canMove = false;
        if (isFacingRight)
        {
            rigi.AddForce(Vector2.left * recoilForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }

    // 处理首次着陆
    public void FirstLand()
    {
        StopInput(); // 停止输入
        characterEffect.DoEffect(CharacterEffect.EffectType.BurstRocks, true); // 播放爆炸效果
    }

    // 停止输入
    public void StopInput()
    {
        gameManager.SetEnableInput(false); // 禁止输入
        StopHorizontalMovement(); // 停止水平移动
    }

    // 恢复输入
    public void ResumeInput()
    {
        gameManager.SetEnableInput(true); // 允许输入
        firstLanding = true; // 设置首次着陆
        FindObjectOfType<SoulOrb>().DelayShowOrb(0.1f); // 延迟显示灵魂球
    }

    // 停止水平移动
    public void StopHorizontalMovement()
    {
        Vector2 velocity = rigi.velocity;
        velocity.x = 0;
        rigi.velocity = velocity; // 设置水平速度为0
        animator.SetInteger("movement", 0); // 设置动画状态为0
    }

    // 播放音频
    public void PlayMusic(AudioClip clip)
    {
        audio.PlayOneShot(clip); // 播放音频片段
    }
}
