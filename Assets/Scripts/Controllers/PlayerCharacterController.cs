using Settings;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] PlayerSettings _playerSettings;
    [SerializeField] PlayerInputSystem _inputSystem;
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] Transform _gunPivot;

    public Transform Transform { get; private set; }
    public int CurrentDirection { get; private set; }
    public Vector2 LookDirection { get; private set; }
    public float CurrentSpeedSqr { get; private set; }
    public bool Sliding { get; private set; }

    void Start()
    {
        Transform = transform;
        _inputSystem.GameplayInput.Dodge.performed += OnDodge;
    }

    void FixedUpdate()
    {
        Vector2 lookDirection = GetLookDirection();
        Vector2 moveInput = GetMoveInput();

        if (Sliding)
        {
            HandleSliding();
            return;
        }

        UpdateLookDirection(lookDirection, moveInput);
        UpdateMovement(moveInput);
    }

    private Vector2 GetLookDirection()
    {
        var lookDirection = _inputSystem.GameplayInput.Look;
        if (lookDirection.sqrMagnitude > 1)
            lookDirection.Normalize();
        return lookDirection;
    }

    private Vector2 GetMoveInput()
    {
        var moveInput = _inputSystem.GameplayInput.Movement;
        if (moveInput.sqrMagnitude > 1)
            moveInput.Normalize();
        return moveInput;
    }

    private void HandleSliding()
    {
        if (Time.time - lastSlideTime > _playerSettings.Movement.SlideDuration)
        {
            Sliding = false;
        }
        else
        {
            var speed = _playerSettings.Movement.SlideInitialSpeed * _playerSettings.Movement.EvaluateSlideSpeed((Time.time - lastSlideTime) / _playerSettings.Movement.SlideDuration);
            currentVelocity = currentVelocity.normalized * speed;
            _rigidbody.velocity = currentVelocity;
        }
    }

    private void UpdateLookDirection(Vector2 lookDirection, Vector2 moveInput)
    {
        if (lookDirection.sqrMagnitude < Mathf.Epsilon)
            lookDirection = moveInput;
        LookDirection = lookDirection;
        if (lookDirection.sqrMagnitude < Mathf.Epsilon)
            return;

        CurrentDirection = Mathf.Abs(lookDirection.x) > Mathf.Abs(lookDirection.y) ? 1
            : lookDirection.y > 0 ? 2 : 0;
        if (Mathf.Abs(lookDirection.x) > Mathf.Epsilon)
            Transform.localScale = lookDirection.x > 0 ? _scaleRight : _scaleLeft;
    }

    private void UpdateMovement(Vector2 moveInput)
    {
        if (moveInput.sqrMagnitude > Mathf.Epsilon)
        {
            currentVelocity = Vector2.SmoothDamp(currentVelocity, moveInput * _playerSettings.Movement.MoveSpeed, ref currentVelocity, _playerSettings.Movement.Acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity = Vector2.SmoothDamp(currentVelocity, Vector2.zero, ref currentVelocity, _playerSettings.Movement.Deceleration * Time.fixedDeltaTime);
        }

        CurrentSpeedSqr = currentVelocity.sqrMagnitude;
        _rigidbody.velocity = currentVelocity;
    }

    void OnDodge(InputAction.CallbackContext context)
    {
        var dodgeDirection = _inputSystem.GameplayInput.Movement;
        if (dodgeDirection.sqrMagnitude < Mathf.Epsilon)
        {
            dodgeDirection = _rigidbody.velocity;
        }
        if (dodgeDirection.sqrMagnitude < Mathf.Epsilon || Sliding)
            return;

        Sliding = true;
        lastSlideTime = Time.time;

        _rigidbody.velocity = dodgeDirection.normalized * _playerSettings.Movement.SlideInitialSpeed;
    }

    void OnDestroy()
    {
        _inputSystem.GameplayInput.Dodge.performed -= OnDodge;
    }

    private readonly Vector3 _scaleLeft = new(1, 1, 1);
    private readonly Vector3 _scaleRight = new(-1, 1, 1);

    private Vector2 currentVelocity;
    private float lastSlideTime;
}
