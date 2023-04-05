using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigging : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _digDistance = 1f;
    [SerializeField] private float _digCooldown = 1.2f;
    [SerializeField] private AnimatorHandler _animatorHandler;

    private WaitForSeconds _cooldown;
    private bool _canDig = true;

    private void OnValidate()
    {
        UpdateCooldownValue();
    }

    private void OnEnable()
    {
        Shop.OnPickaxeUpgradeBought += UpgradePickaxe;
    }

    private void OnDisable()
    {
        Shop.OnPickaxeUpgradeBought -= UpgradePickaxe;
    }

    private void Awake()
    {
        UpdateCooldownValue();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _digDistance, _layerMask) ||
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, _digDistance, _layerMask))
        {
            if(hit.transform.TryGetComponent(out DiggingObject diggingObject) && diggingObject.CanBeDigged && _canDig)
            {
                if (_animatorHandler != null && !_animatorHandler.animator.GetBool("IsAttacking"))
                    _animatorHandler.PlayTargetAnimation("Attack", 0.10f, true);
                
                StartCoroutine(Cooldown());
                diggingObject.Dig();
            }
        }
    }

    private void UpdateCooldownValue()
    {
        _cooldown = new WaitForSeconds(_digCooldown);
    }

    private void UpgradePickaxe(float value)
    {
        Debug.Log(1.3 / value);
        _animatorHandler.animator.SetFloat("DigSpeed", 1.3f / (value/2));
        _cooldown = new WaitForSeconds(value);
        _digCooldown = value;
    }

    private IEnumerator Cooldown()
    {
        _canDig = false;
        yield return _cooldown;
        _canDig = true;
    }
}
