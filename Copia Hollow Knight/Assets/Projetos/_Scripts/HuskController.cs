using UnityEngine;

public class HuskController : MonoBehaviour, IEnemy
{
    private int life = 3;
    private Animator anim;

    public float knockbackForce = 10f;
    public ParticleSystem hitParticle;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Hit(Transform player)
    {
        life--;
        anim.SetTrigger("hit");

        Instantiate(hitParticle, transform.position, Quaternion.identity,transform);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockbackDir = (transform.position - player.position).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }

        if (life <= 0) Dead();
    }

    void Dead()
    {
        anim.SetTrigger("dead");
        Destroy(gameObject, 5f);
    }
}
