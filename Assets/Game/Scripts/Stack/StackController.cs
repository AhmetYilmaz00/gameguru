using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Stack
{
    public class StackController : MonoSingleton<StackController>
    {
        [SerializeField] private Stack currentStack;
        [SerializeField] private Stack beforeStack;
        [SerializeField] private float tolerance;
        [SerializeField] private float nextStepTransationTime;
        [SerializeField] private float currentLength;

        public List<Stack> steps = new();

        private Vector3 _initCurrentStackPosition;

        private WaitForSeconds _waitForSeconds;

        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(nextStepTransationTime);
        }

        public void ControlStep()
        {
            if (Mathf.Abs(currentStack.transform.position.x) <= tolerance)
            {
                StartCoroutine(SuccesStep());
            }
            else
            {
                StartCoroutine(StackStopAndCut());
            }
        }

        private IEnumerator SuccesStep()
        {
            yield return _waitForSeconds;
            GameManager.Instance.onStepFinish.Invoke();
        }

        private IEnumerator StackStopAndCut()
        {
            yield return _waitForSeconds;
        }

        private void DetermineStackPiecePosition(int coefficient)
        {
            currentStack.stackPiece.position += Vector3.right * (((currentStack.transform.localScale.x / 2) +
                                                                  currentStack.stackPiece.lossyScale.x / 2) *
                                                                 coefficient);
        }

        public void StartNextStep()
        {
            var obj = ObjectPool.SharedInstance.GetPooledObject();
            var stack = obj.GetComponent<Stack>();
            steps.Add(stack);
            stack.transform.localScale = steps[steps.Count - 2].transform.localScale;
            stack.Start();
            if (stack.direction == Stack.Direction.Left)
            {
                obj.transform.position =
                    Vector3.left * 5 + Vector3.forward * (steps.Count - 1) * obj.transform.lossyScale.z;
                stack.endPos = Vector3.right * 5 + Vector3.forward * (steps.Count - 1) * obj.transform.lossyScale.z;
            }
            else
            {
                obj.transform.position =
                    Vector3.right * 5 + Vector3.forward * (steps.Count - 1) * obj.transform.lossyScale.z;
                stack.endPos = Vector3.left * 5 + Vector3.forward * (steps.Count - 1) * obj.transform.lossyScale.z;
            }
        }
    }
}