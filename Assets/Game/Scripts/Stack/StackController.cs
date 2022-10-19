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
            currentStack = steps[steps.Count - 1].GetComponent<Stack>();

            currentStack.speed = 0;
            currentStack.stackPiece.gameObject.SetActive(true);
            if (steps.Count > 1)
            {
                beforeStack = steps[steps.Count - 2].GetComponent<Stack>();
                var distance = beforeStack.transform.position.x - currentStack.transform.position.x;
                var halfLocalScale = currentStack.transform.localScale.x / 2;
                var currentRightPivot =
                    currentStack.transform.position.x + halfLocalScale;
                var currentLeftPivot =
                    currentStack.transform.position.x - halfLocalScale;
                var beforeRightPivot = beforeStack.transform.position.x +
                                       halfLocalScale;
                var beforeLeftPivot = beforeStack.transform.position.x -
                                      halfLocalScale;
                var currentLocalScale = currentStack.transform.localScale;
                if (distance >= 0)
                {
                    if (distance > halfLocalScale)
                    {
                        currentStack.transform.localScale = new Vector3(Mathf.Abs(currentRightPivot - beforeLeftPivot),
                            currentLocalScale.y, currentLocalScale.z);
                    }
                    else
                    {
                        currentStack.transform.localScale = new Vector3(Mathf.Abs(
                                beforeStack.transform.localScale.x - Mathf.Abs(currentRightPivot -
                                                                               beforeRightPivot)),
                            currentLocalScale.y, currentLocalScale.z);
                    }
                }
                else
                {
                    if (distance > halfLocalScale)
                    {
                        currentStack.transform.localScale =
                            new Vector3(Mathf.Abs(beforeRightPivot - currentLeftPivot), currentLocalScale.y,
                                currentLocalScale.z);
                    }
                    else
                    {
                        currentStack.transform.localScale = new Vector3(Mathf.Abs(beforeRightPivot - currentRightPivot),
                            currentLocalScale.y, currentLocalScale.z);
                    }
                }
            }

            // currentStack.transform.localScale -= Vector3.right * Mathf.Abs(_initCurrentStackPosition.x);
            // currentStack.transform.position -= Vector3.right * (_initCurrentStackPosition.x / 2);
            // currentStack.stackPiece.localScale = new Vector3(
            //     (currentStack.stackPiece.localScale.x / currentStack.transform.localScale.x) *
            //     Mathf.Abs(currentStack.transform.position.x) * 2, currentStack.stackPiece.localScale.y,
            //     currentStack.stackPiece.localScale.z);
            // if (currentStack.stackPiece.position.x > 0)
            // {
            //     DetermineStackPiecePosition(1);
            // }
            // else
            // {
            //     DetermineStackPiecePosition(-1);
            // }

            var stackPieceRigidbody = currentStack.stackPiece.GetComponent<Rigidbody>();
            stackPieceRigidbody.isKinematic = false;
            yield return _waitForSeconds;
            stackPieceRigidbody.isKinematic = true;
            GameManager.Instance.onStepFinish.Invoke();
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
            if (stack.isLeft)
            {
                obj.transform.position = Vector3.left * 5 + Vector3.forward * steps.Count * obj.transform.lossyScale.z;
                stack.endPos = Vector3.right * 5 + Vector3.forward * steps.Count * obj.transform.lossyScale.z;
            }
            else
            {
                obj.transform.position = Vector3.right * 5 + Vector3.forward * steps.Count * obj.transform.lossyScale.z;
                stack.endPos = Vector3.left * 5 + Vector3.forward * steps.Count * obj.transform.lossyScale.z;
            }
        }
    }
}