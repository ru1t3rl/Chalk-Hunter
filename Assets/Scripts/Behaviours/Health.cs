using UnityEngine;
using UnityEngine.Events;
using Ru1t3rl.Events;

namespace Ru1t3rl.ChalkHunter.Behaviours
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        public UnityEvent onHeal, onDamage, onDie;

        public float CurrentHealth => health;
        public float MaxHealth => maxHealth;

        private void Awake()
        {
            EventManager.Instance.AddEvent($"{gameObject.GetInstanceID()}_OnHeal", onHeal);
            EventManager.Instance.AddEvent($"{gameObject.GetInstanceID()}_OnDamage", onDamage);
            EventManager.Instance.AddEvent($"{gameObject.GetInstanceID()}_OnDie", onDie);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveEvent($"{gameObject.GetInstanceID()}_OnHeal");
            EventManager.Instance.RemoveEvent($"{gameObject.GetInstanceID()}_OnDamage");
            EventManager.Instance.RemoveEvent($"{gameObject.GetInstanceID()}_OnDie");
        }

        public void Heal(float amount)
        {
            health += amount;
            health = Mathf.Clamp(health, 0, maxHealth);
            EventManager.Instance.Invoke($"{gameObject.GetInstanceID()}_OnHeal");
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            health = Mathf.Clamp(health, 0, maxHealth);
            EventManager.Instance.Invoke($"{gameObject.GetInstanceID()}_OnDamage");
            if (health <= 0)
                Die();
        }

        public void Die()
        {
            EventManager.Instance.Invoke($"{gameObject.GetInstanceID()}_OnDie");
            gameObject.SetActive(false);
        }
    }
}