using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ROFL : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _vector;
    private Vector3 _startPos;

    private void Start()
    {
        _vector = new Vector3(0, 0, 0);
        _rigidbody = GetComponent<Rigidbody>();
        _startPos = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(_startPos, transform.position) > 2)
        {
            _vector = _startPos - transform.position;
            _rigidbody.AddForce(_vector * .1f, ForceMode.Impulse);
        }
    }
}
