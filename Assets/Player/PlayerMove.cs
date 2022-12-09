using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {
    [SerializeField] private protected float speed;

    private Rigidbody2D rb;
    private const string INPUT_AXIS_HORIZONTAL = "Horizontal";

    [SerializeField] private protected float jumpForce;

    private protected float coyoteTimer = 0;
    private protected const float timeToCoyote = 0.025f;

    private enum Animations { Idle, Walk, Jump }
    private Animator animator;

    public bool canMov = true;

    private const string CHECK_TAG = "check";
    private const string CHECKPOINT_TAG = "CheckPoint";
    private const string HITBOX_TAG = "HitBox";

    private protected Vector2 lastPlataform = Vector2.zero;
    private protected Vector2 checkPoint;

    [SerializeField] private Slider lifeBar;
    [SerializeField] private Text lifeQuant;
    
    private int life = 3;
    private byte forceReturn = 0;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        LifeManager(+100);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)) canMov = !canMov;

        MoveCamera();
        ControllAnim();
        Move();
        Jump();  
    }

    public void LifeManager(float damage) {
        lifeBar.value += damage;

        if(lifeBar.value <= 0) {
            transform.position = checkPoint;   
            life--;
            lifeQuant.text = life.ToString();

            lifeBar.value = 100;
        }
        else if(forceReturn == 1) {
            transform.position = lastPlataform;
            forceReturn = 0;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag(CHECK_TAG)) lastPlataform = transform.position;
        if(collision.CompareTag(CHECKPOINT_TAG)) checkPoint = transform.position;

        if(collision.CompareTag(HITBOX_TAG)) {
            forceReturn = 1;

            rb.velocity = Vector2.zero;
            LifeManager(-15);
        }
    }

    private byte CanJump() {
        RaycastHit2D rayCenter = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, 1 << 7);
        RaycastHit2D rayLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.3f, transform.position.y), Vector2.down, 0.2f, 1 << 7);
        RaycastHit2D rayRigh = Physics2D.Raycast(new Vector2(transform.position.x + 0.3f, transform.position.y), Vector2.down, 0.2f, 1 << 7);

        if(rayCenter.collider == true || rayLeft.collider == true || rayRigh.collider == true) {
            coyoteTimer = 0;
            return 1;
        }
        else {
            if(coyoteTimer <= timeToCoyote) {
                coyoteTimer += Time.deltaTime;
                return 1;
            }
            else return 0;
        }
    }

    private void Move() {
        if(canMov == true) {
            float xDirection = Input.GetAxis(INPUT_AXIS_HORIZONTAL);
            rb.velocity = new Vector2(xDirection * speed, rb.velocity.y);

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
        else rb.velocity = Vector2.zero;
    }

    private void Jump() {
        if(Input.GetButtonDown("Jump") && CanJump() == 1 && canMov == true) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void ControllAnim() {
        if(CanJump() == 1) {
            if(rb.velocity.x == 0) animator.Play(Animations.Idle.ToString());
            else animator.Play(Animations.Walk.ToString());
        }
        else animator.Play(Animations.Jump.ToString());
    }

    private void MoveCamera() {
        Vector3 newCameraPosition = Camera.main.gameObject.transform.position;

        if(transform.position.x >= 0) newCameraPosition.x = transform.position.x;
        else if(newCameraPosition.x > 0) newCameraPosition.x -= Time.deltaTime * 1f;
        else newCameraPosition.x = 0;

        if(transform.position.y >= 6.5f) newCameraPosition.y = transform.position.y;
        else if(newCameraPosition.y > 6.5f) newCameraPosition.y -= Time.deltaTime * 1f;
        else newCameraPosition.y = 6.5f;

        Camera.main.gameObject.transform.position = newCameraPosition;
    }
}