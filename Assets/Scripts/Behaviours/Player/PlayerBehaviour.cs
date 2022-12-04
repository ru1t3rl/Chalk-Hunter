using UnityEngine;
using UnityEngine.InputSystem;

namespace Ru1t3rl.ChalkHunter.Behaviours.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private new Rigidbody rigidbody;
        [SerializeField] private Animator animator;

        [Header("Movement")]
        private bool move = false;
        private Vector3 moveDirection, horizontalVelocity;

        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 5f;

        [SerializeField] private bool doubleFallGravity = true;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>() ?? throw new MissingComponentException("Rigidbody");
            animator ??= GetComponent<Animator>() ?? throw new MissingComponentException("Animator");
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            if (rigidbody.velocity.y <= 0 && doubleFallGravity)
            {
                rigidbody.velocity += Vector3.up * Physics.gravity.y * (2f - 1f) * Time.fixedDeltaTime;
            }

            rigidbody.MovePosition(transform.position + new Vector3(moveDirection.x, 0, moveDirection.z) * speed * Time.fixedDeltaTime);
        }

        private void OnJump()
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }

        private void OnMove(InputValue value)
        {
            move = !move;
            Vector2 movement = value.Get<Vector2>();
            moveDirection = new Vector3(movement.x, 0, movement.y);
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("speedX", moveDirection.x * speed);
            animator.SetFloat("speedY", rigidbody.velocity.y);
            animator.SetFloat("speedZ", moveDirection.z * speed);
        }

        private void TruncateHorizontal()
        {
            horizontalVelocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);

            if (horizontalVelocity.magnitude <= speed)
                return;
            horizontalVelocity = horizontalVelocity.normalized * speed;
            rigidbody.velocity = new Vector3(horizontalVelocity.x, rigidbody.velocity.y, horizontalVelocity.y);
        }
    }
}