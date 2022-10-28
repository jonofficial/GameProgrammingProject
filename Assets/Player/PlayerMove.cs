using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [SerializeField] private protected float speed;

    private Rigidbody2D rb;
    private const string INPUT_AXIS_HORIZONTAL = "Horizontal";

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        float xDirection = Input.GetAxis(INPUT_AXIS_HORIZONTAL);
        rb.velocity = xDirection * speed * Vector2.right;

        if(xDirection > 0 && transform.localScale.x < 0) {
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if(xDirection < 0 && transform.localScale.x > 0) {
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}