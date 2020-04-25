using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEye : MonoBehaviour
{
    private Vector3 _initialPosition;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _distanceToMoveModifier;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, GetFinalLerpPoint(), _moveSpeed);
    }

    Vector3 GetFinalLerpPoint()
    {
        Vector3 pos1 = _initialPosition;
        Vector3 pos2 = Input.mousePosition;
        Vector3 dir = (pos2 - pos1).normalized;
        Vector3 perpDir = Vector3.Cross(dir, Vector3.right);
        Vector3 lerpPoint = (pos1 + pos2) / 2f;
        for (int i = 0; i < _distanceToMoveModifier; i++)
        {
            lerpPoint = (pos1 + lerpPoint) / 2f;
        }
        Vector3 finalLerpPoint = lerpPoint + perpDir;
        return finalLerpPoint;
    }
}
