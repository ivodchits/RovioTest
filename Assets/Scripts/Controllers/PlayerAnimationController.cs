using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] PlayerCharacterController _characterController;
    [SerializeField] Animator _animator;

    void Update()
    {
        _animator.SetInteger(_direction, _characterController.CurrentDirection);
        _animator.SetFloat(_speed, _characterController.CurrentSpeedSqr);
        _animator.SetBool(_sliding, _characterController.Sliding);
    }

    static readonly int _direction = Animator.StringToHash("Direction");
    static readonly int _speed = Animator.StringToHash("Speed");
    static readonly int _sliding = Animator.StringToHash("Sliding");
}
