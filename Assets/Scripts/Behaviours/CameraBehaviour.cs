using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private float animationDuration = 1.5f;

    [Header("Ortographic")]
    [SerializeField] private Vector3 ortographicPosition = new Vector3(0, 0, -10);
    [SerializeField] private Quaternion ortographicRotation = Quaternion.Euler(0, 0, 0);


    [Header("Perspective")]
    [SerializeField] private Vector3 perspectivePosition = new Vector3(0, 4.25f, -10);
    [SerializeField] private Quaternion perspectiveRotation = Quaternion.Euler(20, 0, 0);

    private void Awake()
    {
        camera ??= GetComponent<Camera>() ?? Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
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
        }
        else
        {
            transform.DOLocalMove(ortographicPosition, animationDuration);
            transform.DORotateQuaternion(ortographicRotation, animationDuration).onComplete += () => camera.orthographic = true;
        }
    }
}
