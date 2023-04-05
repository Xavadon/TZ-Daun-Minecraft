using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementSpeed = 250f; //5
    [SerializeField] private float _rotationSpeed = 700f;
    [SerializeField] private float _checkGroundDistance = 0.5f;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _ladderLayerMask;

    private Rigidbody _rigidbody;
    private PlayerActions _playerActions;
    private bool _isMove = false;
    private bool _isJump = false;
    private Vector3 _stopVector;

    private void Awake()
    {
        _stopVector = new Vector3(0, 0, 0);
        _rigidbody = GetComponent<Rigidbody>();
        _playerActions = new PlayerActions();
        _playerActions.PlayerControls.Movement.performed += OnMove;
        _playerActions.PlayerControls.Movement.canceled += OnMoveStopped;
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        Shop.OnBootsUpgradeBought += UpgradeBoots;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        Shop.OnBootsUpgradeBought -= UpgradeBoots;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (!_isMove && !_isJump)
        {
            if (_animator != null) _animator.SetBool("IsMoving", true);
            _isMove = true;
            Movement();
        }
    }

    private void OnMoveStopped(InputAction.CallbackContext context)
    {
        if (_isMove && !_isJump)
        {
            if (_animator != null) _animator.SetBool("IsMoving", false);
            _isMove = false;
            StopMovement();
        }
    }

    private async void Movement()
    {
        while (_isMove)
        {
            var inputPosition = _playerActions.PlayerControls.Movement.ReadValue<Vector2>();

            var movementDirection = new Vector3(inputPosition.x, 0, inputPosition.y) * _movementSpeed * Time.fixedDeltaTime;

            if (CheckGround()) _rigidbody.velocity = movementDirection;
            if (CheckLadder()) _rigidbody.velocity += Vector3.up * _movementSpeed * movementDirection.z * Time.deltaTime;

            if (movementDirection != Vector3.zero)
            {
                var rotationDirection = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDirection, _rotationSpeed * Time.fixedDeltaTime);
            }
            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
    }

    private void StopMovement()
    {
        _stopVector.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _stopVector;
    }

    private bool CheckGround()
    {
        return (Physics.CheckSphere(transform.position + Vector3.down * _checkGroundDistance, .4f, _groundLayerMask));
    }

    private bool CheckLadder()
    {
        return Physics.CheckSphere(transform.position + Vector3.down * _checkGroundDistance, .4f, _ladderLayerMask);
    }

    private void UpgradeBoots()
    {
        _movementSpeed *= 1.15f;
        _animator.SetFloat("MoveSpeed", _movementSpeed / 250);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * _checkGroundDistance, .4f);
    }
}
