using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour {
    public float jumpForce = 400f;
    public float deltaFire = 1.0f;
    public float moveSpeed = 4.0f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public Transform firePosition;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private new AudioSource audio;
    private bool grounded = true;
    private bool canFire = true;

    private float minimumX;
    private float maximumX;

    void Start() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        var colliderXSize = GetComponent<CapsuleCollider2D>().size.x;

        var verticalExtent = Camera.main.orthographicSize;
        var horizontalExtent = verticalExtent * Screen.width / Screen.height;
        minimumX = -horizontalExtent + colliderXSize * 4;
        maximumX = -colliderXSize;
    }

    void Update() {
        if (GameManager.Instance.GameIsOver()) {
            animator.speed = 0;
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;
        }

        if (!GameManager.Instance.GameIsOn()) return;
        animator.speed = 1f;
        rigidbody2D.gravityScale = -Physics2D.gravity.y;

        if (!grounded) return;

        if (Input.GetKeyDown("space")) {
            Jump();
        } else if (Input.GetMouseButtonDown(0) && canFire) {
            Fire();
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            var position = transform.position;
            var newX = Mathf.Clamp(position.x - moveSpeed * Time.deltaTime, minimumX, maximumX);
            position = new Vector2(newX, position.y);
            transform.position = position;
            animator.speed = 0.6f;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            var position = transform.position;
            var newX = Mathf.Clamp(position.x + moveSpeed * Time.deltaTime, minimumX, maximumX);
            position = new Vector2(newX, position.y);
            transform.position = position;
            animator.speed = 1.3f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (1 << col.gameObject.layer == groundLayer) {
            grounded = true;
            animator.Play("Player_walk_full", 0, 0);
        }
    }

    private void Jump() {
        grounded = false;
        rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        animator.Play("Player_jump", 0, 0); // TODO select jump depending on canFire
    }

    private void Fire() {
        canFire = false;
        var obj = GameObject.Instantiate(bullet, firePosition.position, bullet.transform.rotation);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Bullet.bulletForce, 0f));
        animator.Play("Player_walk", 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        audio.Play();
        StartCoroutine(CountTillNextFire());
    }

    private IEnumerator CountTillNextFire() {
        yield return new WaitForSeconds(deltaFire);
        canFire = true;
        if (grounded) {
            animator.Play("Player_walk_full", 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
    }
}
