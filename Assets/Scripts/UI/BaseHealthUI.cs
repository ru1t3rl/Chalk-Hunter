using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Behaviours;
using Ru1t3rl.Events;

namespace Ru1t3rl.ChalkHunter.UI
{
    [RequireComponent(typeof(Health))]
    public abstract class BaseHealthUI : MonoBehaviour
    {
        [SerializeField] protected Health health;

        protected virtual void Awake()
        {
            health ??= GetComponent<Health>();

            EventManager.Instance.AddListener($"{gameObject.GetInstanceID()}_OnHeal", UpdateUI);
            EventManager.Instance.AddListener($"{gameObject.GetInstanceID()}_OnDamage", UpdateUI);
            EventManager.Instance.AddListener($"{gameObject.GetInstanceID()}_OnDie", UpdateUI);
        }

        public abstract void UpdateUI();
    }
}