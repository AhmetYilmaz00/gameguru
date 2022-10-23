using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Stack
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float camRotateSpeed;
        private bool _isStartCameraRotation;

        private void Update()
        {
            if (_isStartCameraRotation)
            {
                virtualCamera.transform.eulerAngles += Vector3.up * (camRotateSpeed * Time.deltaTime);
            }
        }

        public void CameraRotation()
        {
            _isStartCameraRotation = true;
        }
    }
}