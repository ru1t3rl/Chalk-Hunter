using UnityEngine;
using Ru1t3rl.Events;
using Ru1t3rl.Events.Args;

namespace Ru1t3rl.ChalkHunter.Behaviours
{
    public class PerspectiveBasedVisual : MonoBehaviour
    {
        [SerializeField] private bool isPerspectiveObject = false;

        private void Awake()
        {
            EventManager.Instance.AddListener("SwitchView", SwitchView);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener("SwitchView", SwitchView);
        }

        private void SwitchView(System.EventArgs args)
        {
            gameObject.SetActive(isPerspectiveObject == (args as ViewArguments).isPerspective);
        }
    }
}