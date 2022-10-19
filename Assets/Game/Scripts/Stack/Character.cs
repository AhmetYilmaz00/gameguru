using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Stack
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float transitionSpeed;

        private bool _isRun;

        public void GoToNextStep()
        {
            _isRun = true;
        }

        private void Update()
        {
            if (_isRun)
            {
                var nextStep = StackController.Instance.steps[StackController.Instance.steps.Count - 1].transform;
                transform.position =
                    Vector3.Lerp(transform.position,
                        new Vector3(nextStep.position.x, transform.position.y, nextStep.position.z),
                        transitionSpeed * Time.deltaTime);
                if (Mathf.Abs(transform.position.z - nextStep.position.z) < 0.2f)
                {
                    _isRun = false;
                    GameManager.Instance.onStartNextStep.Invoke();
                }
            }
        }
    }
}