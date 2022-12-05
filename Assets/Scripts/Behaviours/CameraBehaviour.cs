using UnityEngine;
using UnityEngine.Events;
using Ru1t3rl.Events;
using Ru1t3rl.Events.Args;
using Ru1t3rl.ChalkHunter.Data;
using DG.Tweening;

namespace Ru1t3rl.ChalkHunter.Behaviours
{
    public class CameraBehaviour : MonoBehaviour
    {
        [SerializeField] private KeyMap keyMap;
        [SerializeField] private new Camera camera;
        [SerializeField] private float animationDuration = 1.5f;
        public UnityEvent<System.EventArgs> onSwitch = new UnityEvent<System.EventArgs>();

        [Header("Ortographic")]
        [SerializeField] private Vector3 ortographicPosition = new Vector3(0, 0, -10);
        [SerializeField] private Quaternion ortographicRotation = Quaternion.Euler(0, 0, 0);


        [Header("Perspective")]
        [SerializeField] private Vector3 perspectivePosition = new Vector3(0, 4.25f, -10);
        [SerializeField] private Quaternion perspectiveRotation = Quaternion.Euler(20, 0, 0);

        private void Awake()
        {
            camera ??= GetComponent<Camera>() ?? Camera.main;
            EventManager.Instance.AddListener("SwitchView", onSwitch.Invoke);
        }

        private void Start()
        {
            EventManager.Instance.Invoke("SwitchView", new ViewArguments(isPerspective: !camera.orthographic));

            if (!camera.orthographic)
            {
                OnToggleView();
            }
        }

        private void Update()
        {
            if (keyMap.IsKeyDown(Enums.Action.SwitchView))
            {
                OnToggleView();
            }
        }

        private void OnToggleView()
        {
            if (camera.orthographic)
            {
                camera.orthographic = false;
                transform.DOLocalMove(perspectivePosition, animationDuration);
                transform.DORotateQuaternion(perspectiveRotation, animationDuration);

                EventManager.Instance.Invoke("SwitchView", new ViewArguments(isPerspective: !camera.orthographic));
            }
            else
            {
                transform.DOLocalMove(ortographicPosition, animationDuration);
                transform.DORotateQuaternion(ortographicRotation, animationDuration).onComplete += () =>
                {
                    camera.orthographic = true;
                    EventManager.Instance.Invoke("SwitchView", new ViewArguments(isPerspective: !camera.orthographic));
                };
            }
        }
    }
}