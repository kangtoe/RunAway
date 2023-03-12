using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    Collider2D standCollider;
    [SerializeField]
    Collider2D slideCollider;

    [SerializeField]
    GameObject jumpEfx;

    [SerializeField]
    GameObject shield;

    [SerializeField]
    float jumpForce;
    [SerializeField]
    float slamForce;
    [SerializeField]
    int maxJumpCount = 1;

    [Header("피격 시 대기시간")]
    [SerializeField]
    float hitHoldTime = 1f;

    [Header("피격 후 무적시간")]
    [SerializeField]
    float hitInvincibleTime = 0.5f;

    //int lifeCount;
    int currentJumpCount;
    bool isGrounded;
    bool isSliding;
    bool isDead;
    bool isInvincible;
    bool isOnHit;
    bool isShielded;
    bool isBoosted;

    Vector2 startPos;
    Rigidbody2D rb;
    Animator anim;
    AudioSource AudioSource;
    SpriteRenderer SpriteRenderer;

    InputManager InputManager => InputManager.Instance;
    GameManager GameManager => GameManager.Instance;
    UiManager UiManager => UiManager.Instance;

    #region 유니티 라이프 사이클

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        slideCollider.enabled = false;
    }


    void Update()
    {
        float animSpeed = GameManager.GameSpeed;
        anim.SetFloat("animSpeed", animSpeed);

        if (isDead) return;

        anim.SetFloat("velocityY", rb.velocity.y);

        if (isOnHit) return;

        if (InputManager.JumpInput)
        {
            TryJump();
        }

        if (InputManager.SlideInput)
        {
            if (!isGrounded) Slam();
            if (!isSliding) OnStartSlide();

        }
        else
        {
            if (isSliding) OnEndSlide();
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("HIT WITH : " + collision.gameObject.name);

        if (isDead) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("ClearTrigger"))
        {
            //Debug.Log("Clear");

            anim.SetBool("idle", true);
            GameManager.PlayClearCutScene();
            //GameManager.GameClear();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            ItemBase item = collision.gameObject.GetComponent<ItemBase>();
            item.UseItem(this);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("DeadZone") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Drill"))
        {

            //Hit();

            //if (!isDead)
            //{
            //    rb.velocity = Vector2.zero;
            //    transform.position = startPos; // Repositioning
            //}

            GameManager.Instance.GameOver();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") && !isInvincible)
        {
            Hit();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 발 부분 충돌 시에만 착지 판정
        if (collision.contacts[0].normal.y > 0.5f)
        {
            // 하강 중
            if (rb.velocity.y < 0)
            {
                rb.velocity = Vector2.zero;
            }
            
            isGrounded = true;
            currentJumpCount = 0;
            anim.SetBool("jump", !isGrounded);

            UiManager.SetRightBtnTxt("slide");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //isGrounded = false;
    }

    #endregion 유니티 라이프 사이클    

    #region 점프 & 슬라이드 & 하강

    public void TryJump()
    {
        if (isDead) return;
        if (isOnHit) return;
        if (currentJumpCount >= maxJumpCount) return;
        if (isSliding) OnEndSlide();

        UiManager.SetRightBtnTxt("down");

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //AudioSource.Play();

        currentJumpCount++;
        isGrounded = false;
        anim.SetBool("jump", !isGrounded);

        Vector3 offset = Vector3.up * 0.1f + Vector3.left * 0.5f;
        Instantiate(jumpEfx, transform.position + offset, Quaternion.identity);

        //anim.SetBool("Grounded", isGrounded);

        if (currentJumpCount > 1)
        {
            anim.SetTrigger("doubleJump");
        }
    }

    public void OnStartSlide()
    {
        anim.SetBool("slide", true);
        isSliding = true;

        standCollider.enabled = false;
        slideCollider.enabled = true;
    }

    public void OnEndSlide()
    {
        isSliding = false;
        anim.SetBool("slide", false);

        standCollider.enabled = true;
        slideCollider.enabled = false;
    }

    // 체공 중 바닥으로 빠르게 하강
    void Slam()
    {
        //Debug.Log("slam");

        if (isGrounded)
        {
            Debug.Log("can slam only on air");
            return;
        }

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, -slamForce), ForceMode2D.Impulse);
    }

    #endregion 점프 & 슬라이드 & 하강

    #region 피격 & 사망

    void Die()
    {
        isDead = true;
        rb.GetComponent<Collider2D>().isTrigger = true;
    }

    void Hit()
    {
        if (isShielded)
        {
            ActiveShield(false);
            StartCoroutine(InvincibleControl(hitInvincibleTime));
        }
        else
        {
            StartCoroutine(HitCr(hitHoldTime));
            StartCoroutine(InvincibleControl(hitHoldTime + hitInvincibleTime));
            GameManager.HoldPlayerProgress(hitHoldTime);
        }

        CameraManager.Instance.Shake();
    }

    IEnumerator HitCr(float duration)
    {

        OnStartHit();

        float t = 0;
        while (true)
        {
            t += Time.deltaTime / duration;
            if (t > 1) t = 1;

            float currentSpeed = Mathf.Lerp(1, 0, t);
            GameManager.SetGameSpeed(currentSpeed);

            if (t == 1) break;

            yield return null;
        }

        OnEndHit();
    }

    void OnStartHit()
    {

        isOnHit = true;
        anim.SetBool("onHit", true);
        //GameManager.SetGameSpeed(0);
    }

    void OnEndHit()
    {
        GameManager.SetGameSpeed(1);
        isOnHit = false;
        anim.SetBool("onHit", false);
    }

    #endregion 피격 & 사망

    IEnumerator InvincibleControl(float duration, float interval = 0.1f)
    {
        Debug.Log("InvincibleControl : duration = " + duration);

        isInvincible = true;        
        
        float InvincibleTime = 0;

        while (InvincibleTime < duration)
        {         
            SpriteRenderer.color = new Color32(255, 255, 255, 90);            

            yield return new WaitForSeconds(interval);

            SpriteRenderer.color = new Color32(255, 255, 255, 180);
       
            yield return new WaitForSeconds(interval);

            InvincibleTime += interval * 2;
        }

        SpriteRenderer.color
                   = new Color32(255, 255, 255, 255);

        isInvincible = false;
     
        yield return null;
    }

    IEnumerator BoostControl(float duration, float boostAmount)
    {        
        float additionalInvincibleTime = 1f;

        if (isBoosted)
        {
            Debug.Log("isBoosted already!");
            yield break;
        }

        if (isOnHit)
        {
            OnEndHit();
        }

        isBoosted = true;

        GameManager.SetGameSpeed(1 * boostAmount);

        StartCoroutine(InvincibleControl(duration + additionalInvincibleTime));
        yield return new WaitForSeconds(duration);

        Debug.Log("boost end");
        GameManager.SetGameSpeed(1);
        isBoosted = false;
    }

    public void ActiveShield(bool active)
    {
        shield.SetActive(active);
        isShielded = active;
    }

    public void StartBoost(float duration, float boostAmount)
    {
        StopAllCoroutines();
        StartCoroutine(BoostControl(duration, boostAmount));
    }
}
