using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public new Collider2D collider;

    private void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    public void Die()
    {
        anim.SetBool("Dying", true);
    }

    public void Revive(Vector2 position)
    {

        transform.position = position;
        anim.SetBool("Dying", false);
        collider.enabled = true;
    }
}
