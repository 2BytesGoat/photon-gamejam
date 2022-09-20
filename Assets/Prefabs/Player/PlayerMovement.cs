using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 1.25f;
    public Rigidbody2D rb;

    public GameObject PlayerText;

    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        animator.SetFloat("Speed", movement.sqrMagnitude);

        UpdateFacingDirection();
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateFacingDirection() {
        if (movement.x < 0) {
            this.transform.localScale = new Vector3(-1, 1, 1);
            PlayerText.transform.localScale = new Vector3(-1, 1, 1);
        } else if (movement.x > 0) {
            this.transform.localScale = new Vector3(1, 1, 1);
            PlayerText.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
