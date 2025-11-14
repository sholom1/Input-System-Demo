using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public new Collider2D collider;
    public new Rigidbody2D rigidbody;

    private void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // When the player dies communicate the new state to the animator
    public void Die()
    {
        anim.SetBool("Dying", true);
    }

    // Resets the player
    public void Revive(Vector2 position)
    {
        // Move player to target position
        transform.position = position;
        // Clear it's velocity
        rigidbody.linearVelocity = Vector2.zero;
        // Stop dying
        anim.SetBool("Dying", false);
        // Reactivate collider (is disabled by the death animation)
        collider.enabled = true;
    }

    // Checks if the player can Jump, then jumps winning the game.
    public void OnJump()
    {
        // Only one player can jump per round
        if (GameManager.instance.CanPlayerJump())
        {
            // If nobody has jumped yet then this player jumps and is declared the winner.
            GameManager.instance.OnPlayerJumped(this);
            anim.SetTrigger("Jump");
            rigidbody.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
        }
    }
}
