using UnityEngine;

public class AtkController : MonoBehaviour
{
    private bool atk;
    public void Atacking() => atk = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") && atk)
        {
            collision.gameObject.GetComponent<IEnemy>().Hit(GetComponentInParent<Transform>());
            atk = false;
        }
    }
}
