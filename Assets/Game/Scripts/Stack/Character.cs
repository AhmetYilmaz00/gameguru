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
        [SerializeField] private Animator animator;

        private bool _isRun;
        private bool _isFall;


        private void Update()
        {
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
                    if (StackController.Instance.steps.Count == GameManager.Instance.stepCount)
                    {
                        Debug.Log("SUCESSLEVEL");
                        GameManager.Instance.onSuccesLevel.Invoke();

                        return;
                    }

                    GameManager.Instance.isReadyTouch = true;
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
            GameManager.Instance.onFailLevel.Invoke();
        }

        public void PlayCharacterSuccesAnimation()
        {
            animator.SetBool("IsRunning", false);
        }
    }
}