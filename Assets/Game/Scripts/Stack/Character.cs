using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Stack
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float transitionSpeed;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private bool _isRun;
        private bool _isFall;


        private void Update()
        {
            Debug.Log("_isRun" + _isRun);
            if (_isRun)
            {
                var nextStep = StackController.Instance.steps[StackController.Instance.steps.Count - 1].transform;
                var position = transform.position;
                var nextStepPos = nextStep.position;
                position =
                    Vector3.Lerp(position,
                        new Vector3(nextStepPos.x, position.y, nextStepPos.z),
                        transitionSpeed * Time.deltaTime);
                transform.position = position;
                if (Mathf.Abs(transform.position.z - nextStep.position.z) < 0.2f)
                {
                    _isRun = false;
                    GameManager.Instance.onStartNextStep.Invoke();
                }
            }

            if (_isFall)
            {
                var position = transform.position;
                position =
                    Vector3.Lerp(position,
                        position + Vector3.forward * 5,
                        transitionSpeed * Time.deltaTime);
                transform.position = position;
                if (transform.position.y < 0.48f)
                {
                    _isFall = false;
                    virtualCamera.Follow = null;
                    virtualCamera.LookAt = null;
                }
            }
        }

        public void GoToNextStep()
        {
            _isRun = true;
        }

        public void StartFall()
        {
            _isFall = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}