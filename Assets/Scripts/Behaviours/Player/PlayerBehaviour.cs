using UnityEngine;
using Ru1t3rl.ChalkHunter.Utilities;
using Ru1t3rl.ChalkHunter.Enums;
using Ru1t3rl.Events;
using Ru1t3rl.Events.Args;

namespace Ru1t3rl.ChalkHunter.Behaviours.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UnityEngine.Animator animator;
        [SerializeField] private KeyMap keyMap;
        [SerializeField] private new Rigidbody rigidbody;

        [Header("Movement")]
        private Vector3 velocity;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float surfaceDrag = 5f;
        public float Gravity => gravity / 100f;
        [SerializeField] private bool doubleFallGravity = true;
        [SerializeField] private bool slowAirial = true;
        private bool inPerspective = false;

        private void Awake()
        {
            if (keyMap is null)
                throw new System.NullReferenceException("The keymap is empty, without it the player can't move");

            rigidbody ??= GetComponent<Rigidbody>();

            EventManager.Instance.AddListener("SwitchView", SwitchView);
            inPerspective = Camera.main.orthographic;
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener("SwitchView", SwitchView);
        }

        private void SwitchView(System.EventArgs args)
        {
            inPerspective = (args as ViewArguments).isPerspective;
        }

        private void Update()
        {
            // Physics Related
            if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
            {
                Jump();
            }

            ApplyGravity();
            Move();
            TruncateRigidbodyVelocity();

            // Update Visuals
            SetVisualDirection();
            UpdateAnimtor();
        }

        private void Move()
        {
            velocity.x += (keyMap.IsKey(Action.Left) ? -speed : keyMap.IsKey(Action.Right) ? speed : 0) * (!IsOnGround && slowAirial ? 0.5f : 1);
            if (inPerspective)
                velocity.z += keyMap.IsKey(Action.Down) ? -speed : keyMap.IsKey(Action.Up) ? speed : 0 * (!IsOnGround && slowAirial ? 0.5f : 1);

            rigidbody?.AddForce(new Vector3(velocity.x, 0, velocity.z), ForceMode.VelocityChange);

            velocity.x /= surfaceDrag;
            if (velocity.x < 0.1f && velocity.x > -0.1f)
                velocity.x = 0;


            velocity.z /= surfaceDrag;
            if (velocity.z < 0.1f && velocity.z > -0.1f)
                velocity.z = 0;
        }

        private void ApplyGravity()
        {
            if (rigidbody.velocity.y <= 0 && doubleFallGravity)
            {
                rigidbody.velocity += Vector3.up * Physics.gravity.y * (2f - 1f) * Time.fixedDeltaTime;
            }

            velocity.y = rigidbody.velocity.y;
        }

        private void Jump()
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            velocity.y = rigidbody.velocity.y;
            animator.SetTrigger("Jump");
        }

        private void UpdateAnimtor()
        {
            animator.SetFloat("speedX", velocity.x);
            animator.SetFloat("speedY", velocity.y);
            animator.SetFloat("speedZ", velocity.z);
        }

        private void SetVisualDirection()
        {
            if (velocity.x > 0)
                transform.localScale = Vector3.one;
            else if (velocity.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        private void TruncateRigidbodyVelocity()
        {
            rigidbody.velocity = new Vector3(
                Mathf.Clamp(rigidbody.velocity.x, -speed, speed),
                rigidbody.velocity.y,
                Mathf.Clamp(rigidbody.velocity.z, -speed, speed)
            );
        }

        private bool IsOnGround => Physics.Raycast(transform.position, Vector3.down, 1f);
    }
}