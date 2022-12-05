using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Ru1t3rl.ChalkHunter.Behaviours.Enemies
{
    [RequireComponent(typeof(SphereCollider))]
    public class ArealAttack : MonoBehaviour
    {
        public UnityEvent onAttack;
        private bool inAttack = false;
        private Coroutine attackRoutine;

        [Header("General")]
        [SerializeField] private string playerTag;
        [SerializeField] private float maxDamage = 20f;
        [SerializeField] private float minDamage = 10f;
        [Tooltip("When disabled, the attack will use max damage!")]
        [SerializeField] private bool useRangedDamage = true;
        [SerializeField] private float cooldown = 2f;

        [Header("Radius")]
        [Tooltip("When false, the radius will start from a min value and grow to original value!")]
        [SerializeField] private bool instant = true;
        [SerializeField] private float minRadius = 0.1f;
        [SerializeField] private float maxRadius = 1f;
        [SerializeField] private float radiusTransitionDuration = 1f;

        [Header("References")]
        [SerializeField] private SphereCollider sphereCollider;

        private void OnValidate()
        {
            sphereCollider ??= GetComponent<SphereCollider>();
            sphereCollider.radius = maxRadius;
        }

        private void Awake()
        {
            sphereCollider ??= GetComponent<SphereCollider>() ?? gameObject.AddComponent<SphereCollider>();
        }

        private IEnumerator Attack()
        {
            inAttack = true;

            if (!instant)
            {
                sphereCollider.radius = minRadius;
                DOTween.To(
                    () => sphereCollider.radius,
                    x => sphereCollider.radius = x,
                    maxRadius,
                    radiusTransitionDuration).SetEase(Ease.Unset).onComplete += () =>
                    {
                        if (cooldown > radiusTransitionDuration)
                        {
                            new WaitForSeconds(cooldown - radiusTransitionDuration);
                        }

                        inAttack = false;
                        attackRoutine = null;
                    };
            }
            else
            {
                yield return new WaitForSeconds(radiusTransitionDuration);
                inAttack = false;
                attackRoutine = null;
            }
        }

        public void OnTriggerEnter(Collider other)
        {

            if (!inAttack)
            {
                if (other.CompareTag(playerTag) && attackRoutine is null)
                {
                    attackRoutine = StartCoroutine(Attack());
                    onAttack?.Invoke();
                }
                return;
            }

            if (other.CompareTag(playerTag))
            {
                OnHitPlayer(other);
            }
        }

        public void OnHitPlayer(Collider other)
        {
            Health health = other.GetComponent<Health>();
            if (health is not null)
            {
                if (useRangedDamage)
                {
                    // Check how far the object is from the center of the sphere
                    // and calculate the damage based on that.
                    // distance / Radius will give more damage the further you get 
                    // away from the center. Radius - distance inverts this.
                    health.TakeDamage(Mathf.Max(
                            maxDamage * ((sphereCollider.radius - Vector3.Distance(other.transform.position, this.transform.position)) / sphereCollider.radius),
                            minDamage
                        ));
                }
                else
                {
                    health.TakeDamage(maxDamage);
                }
            }
        }
    }
}