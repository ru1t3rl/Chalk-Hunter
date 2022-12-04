using UnityEngine;
using Random = System.Random;

namespace Ru1t3rl.ChalkHunter.Utilities
{
    public class Animator : MonoBehaviour
    {
        [SerializeField] private bool randomStartValue = true;
        [SerializeField] private string seed;
        private Random random;

        [SerializeField] private float amplitude = 1f;
        [SerializeField] private float frequency = 1f;
        private float angle = 1f;

        private Vector3 startPosition;
        [SerializeField] private bool xPosition = false;
        [SerializeField] private bool yPosition = false;
        [SerializeField] private bool zPosition = false;


        private Quaternion startRotation;
        [SerializeField] private bool xRotation = false;
        [SerializeField] private bool yRotation = false;
        [SerializeField] private bool zRotation = false;

        private Vector3 startScale;
        [SerializeField] private bool xScale = false;
        [SerializeField] private bool yScale = false;
        [SerializeField] private bool zScale = false;

        private void Awake()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            startScale = transform.localScale;

            if (randomStartValue)
            {
                random ??= new Random(seed.GetHashCode());
                angle = random.Next(0, 360);

                UpdatePosition();
                UpdateRotation();
                UpdateScale();
            }
        }

        private void Update()
        {
            angle += Time.deltaTime * frequency;

            UpdatePosition();
            UpdateRotation();
            UpdateScale();
        }

        private void UpdatePosition()
        {
            if (xPosition)
            {
                transform.localPosition = new Vector3(startPosition.x + Mathf.Sin(angle) * amplitude, transform.position.y, transform.position.z);
            }
            if (yPosition)
            {
                transform.localPosition = new Vector3(transform.position.x, startPosition.y + Mathf.Sin(angle) * amplitude, transform.position.z);
            }
            if (zPosition)
            {
                transform.localPosition = new Vector3(transform.position.x, transform.position.y, startPosition.z + Mathf.Sin(angle) * amplitude);
            }
        }

        private void UpdateRotation()
        {
            if (xRotation)
            {
                transform.rotation = new Quaternion(startRotation.x + Mathf.Sin(angle) * amplitude, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            }
            if (yRotation)
            {
                transform.rotation = new Quaternion(transform.rotation.x, startRotation.y + Mathf.Sin(angle) * amplitude, transform.rotation.z, transform.rotation.w);
            }
            if (zRotation)
            {
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, startRotation.z + Mathf.Sin(angle) * amplitude, transform.rotation.w);
            }
        }

        private void UpdateScale()
        {
            if (xScale)
            {
                transform.localScale = new Vector3(startScale.x + Mathf.Sin(angle) * amplitude, transform.localScale.y, transform.localScale.z);
            }
            if (yScale)
            {
                transform.localScale = new Vector3(transform.localScale.x, startScale.y + Mathf.Sin(angle) * amplitude, transform.localScale.z);
            }
            if (zScale)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, startScale.z + Mathf.Sin(angle) * amplitude);
            }
        }
    }
}