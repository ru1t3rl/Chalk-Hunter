using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Extensions;

namespace Ru1t3rl.ChalkHunter.Behaviours
{
    public class RegainHealth : MonoBehaviour
    {
        [SerializeField] private bool onCollision = true;
        [SerializeField] private float amount = 25f;
        [SerializeField] private string[] colliderTags;

        public void Heal(Health health)
        {
            health.Heal(amount);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(colliderTags))
            {
                return;
            }

            Health health = other.GetComponent<Health>();
            if (health is null)
            {
                return;
            }

            Heal(health);
            gameObject.SetActive(false);
        }
    }
}
