using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Character : MonoBehaviour, IDamageable
{

    // Variablese 

    [SerializeField] private float maxSpeed = 120.0f;
    [SerializeField] private float jumpForce = 360.0f;
    [SerializeField] private Transform footsTransform;
    [SerializeField] private LayerMask layerMaskGround;

    // Animation Instance
    private AnimInstance ownerAnimInstance;
    private SpriteRenderer ownerSpriteRender;

    private Rigidbody2D rb2D;
    private BoxCollider2D ownerCollider;
    private PlayerController ownerController;
    private float directionMovementX;

    // Properties

    public bool IsJump { get; private set; }
    public bool IsFalling { get; private set; }
    public bool IsGround { get; private set; }
    public bool IsDeath { get; private set; } = false;


    void Start()
    {
        Cursor.visible = false;
        
        rb2D = GetComponent<Rigidbody2D>();

        ownerAnimInstance = GetComponent<AnimInstance>();
        ownerSpriteRender = GetComponent<SpriteRenderer>();
        ownerCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        CheckGround();

        rb2D.linearVelocityX = directionMovementX * maxSpeed * Time.fixedDeltaTime;

        if (ownerSpriteRender && Mathf.Abs(rb2D.linearVelocityX) > 0.0f)
        {
            ownerSpriteRender.flipX = rb2D.linearVelocityX < 0.0f;
            Vector2 newColliderOffset = new Vector2(ownerSpriteRender.flipX ? -0.1f : 0.1f, ownerCollider.offset.y);
            ownerCollider.offset = newColliderOffset;
        }
    }

    void CheckGround()
    {
        IsGround = Physics2D.BoxCast(footsTransform.position, new Vector2(ownerCollider.size.x, 0.15f), 0.0f, Vector2.down, 0.2f, layerMaskGround);
        IsFalling = IsGround && rb2D.linearVelocityY < -0.15f;

        if (IsGround)
        {
            IsJump = false;
        }
    }

    public void PossesedBy(PlayerController controller)
    {
        ownerController = controller;
    }

    public void Move(float direcation)
    {
        directionMovementX = direcation;
    }

    public void JumpForce(float force)
    {
        rb2D.linearVelocityY = 0.0f;
        rb2D.AddForceY(force, ForceMode2D.Impulse);
        IsJump = true;
    }

    public void Jump()
    {
        if (IsGround)
        {
            rb2D.linearVelocityY = 0.0f;
            rb2D.AddForceY(jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump!");
        }
    }

    public void TakeDamage()
    {
        if (!IsDeath)
        {
            IsDeath = true;
            Bus<PlayerDeathEvent>.Raise(new PlayerDeathEvent(gameObject));
        }
    }

    public float GetCharacterMovementSpeed()
    {
        return Mathf.Abs(rb2D.linearVelocityX);
    }
}
