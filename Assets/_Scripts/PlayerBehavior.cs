using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Joystick joystick;
    public float joystickHorizontalSens;
    public float joystickVertSens;
    public float horizForce;
    public float vertForce;
    public bool isGrounded;

    private Rigidbody2D m_rigidbody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    void _Move()
    {
        if (isGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSens)
            {
                //move right
                m_rigidbody2D.AddForce(Vector2.right * horizForce * Time.deltaTime);
                m_spriteRenderer.flipX = false;
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSens)
            {
                //move left
                m_rigidbody2D.AddForce(Vector2.left * horizForce * Time.deltaTime);
                m_spriteRenderer.flipX = true;
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Vertical > joystickVertSens)
            {
                //jump
                m_rigidbody2D.AddForce(Vector2.up * vertForce * Time.deltaTime);
                m_animator.SetInteger("AnimState", 2);
            }
            else
            {
                m_animator.SetInteger("AnimState", 0);
                //still
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
}
