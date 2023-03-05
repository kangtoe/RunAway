using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    GameObject jumpEfx;

    [SerializeField]
    float jumpForce;
    [SerializeField]
    int maxJumpCount = 1;

    int lifeCount;
    int currentJumpCount;
    bool isGrounded;
    bool isSliding;
    bool isDead;
    bool isInvincible;

    Vector2 startPos;
    Rigidbody2D rb;
    Animator anim;
    AudioSource AudioSource;
    SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        anim.SetFloat("velocityY", rb.velocity.y);

        // pc의 경우 (모바일에서는 버튼 처리)
        if (Input.GetButtonDown("Jump"))
        {
            JumpCheck();
        }

        if (InputManager.Instance.SlideInput)
        {
            if(!isSliding) OnStartSlide();

        }
        else
        {
            OnEndSlide();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 콜라이더가 부딪친 위치가 일정 값 이상일때만 작동
        // 머리 부분이 부딪히면 동작하지 않도록
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            currentJumpCount = 0;
            anim.SetBool("jump", !isGrounded);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //isGrounded = false;
    }

    public void JumpCheck()
    {
        if (currentJumpCount >= maxJumpCount) return;
        if (isSliding) OnEndSlide();

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
    }

    public void OnEndSlide()
    {
        isSliding = false;
        anim.SetBool("slide", false);
    }

    void GroundCheck()
    { 
    
    }

}
