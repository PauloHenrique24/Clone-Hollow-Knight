using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    Animator anim;
    Rigidbody2D rb;
    Vector2 input;

    // Flip
    bool _isFacing;

    [Header("Ataque")]
    [SerializeField] private float timer_delay;
    private GameObject atk_player;

    private bool atk;
    private bool isGround;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        atk_player = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Movimentacao();
    }

    void Movimentacao()
    {
        Vector3 mov = new(input.x, 0f);

        transform.position += mov * speed * Time.deltaTime;

        if (mov.x < 0 && !_isFacing) Flip();
        else if (mov.x > 0 && _isFacing) Flip();

        if (mov.x != 0) anim.SetBool("walk", true);
        else anim.SetBool("walk", false);
    }

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    void Flip()
    {
        _isFacing = !_isFacing;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void OnAtk(InputValue value)
    {
        if (!atk)
        {
            anim.SetTrigger("atk");
            atk_player.GetComponent<AtkController>().Atacking();
            StartCoroutine(Atk());
        }
    }

    IEnumerator Atk()
    {
        atk = true;
        yield return new WaitForSeconds(timer_delay);
        atk = false;
    }

    public void OnJump(InputValue value)
    {
        if (isGround)
        {
            anim.SetTrigger("jump");
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            isGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isGround = true;
        }
    }

}
