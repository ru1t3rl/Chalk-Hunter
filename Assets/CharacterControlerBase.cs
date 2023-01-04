using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterControlerBase : MonoBehaviour
{
    private CharacterController controller;
    public CharacterController Controller => controller ??= GetComponent<CharacterController>() ?? gameObject.AddComponent<CharacterController>();

    [Header("Ground Check Settings")]
    [SerializeField] protected float groundCheckDistance = 0.1f;
    [SerializeField] protected LayerMask groundLayer;

    protected abstract void Update();
    protected abstract void ApplyGravity();
    public abstract void ApplyMovement(Vector3 direction);
    public abstract void Jump();


    protected bool IsOnGround(bool useLayerMask = false) =>
        useLayerMask ?
        Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, 1 << groundLayer) :
        Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
}
