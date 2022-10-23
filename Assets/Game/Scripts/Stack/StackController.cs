using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Stack
{
    public class StackController : MonoSingleton<StackController>
    {
        [SerializeField] private Stack currentStack;
        [SerializeField] private Stack beforeStack;
        [SerializeField] private Transform currentBroken;
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
            if (Mathf.Abs(currentStack.transform.position.x - beforeStack.transform.position.x) <= tolerance)
            {
                StartCoroutine(SuccesStep());
            }
            else if (currentStack.leftPivot.position.x > beforeStack.rightPivot.position.x ||
                     currentStack.rightPivot.position.x < beforeStack.leftPivot.position.x)
            {
                GameManager.Instance.onFallStart.Invoke();
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
            if (currentStack.leftPivot.position.x < beforeStack.leftPivot.position.x)
            {
                var brokenPiece = currentStack.rightPivot.position.x - beforeStack.rightPivot.position.x;


                currentBroken.transform.position += Vector3.right *
                    (beforeStack.leftPivot.position.x - currentStack.leftPivot.position.x) / 2;


                currentBroken.gameObject.SetActive(true);

                //  currentStack.stackPiece.GetComponent<Rigidbody>().isKinematic = false;


                currentStack.transform.localScale =
                    new Vector3(Mathf.Abs(beforeStack.leftPivot.position.x - currentStack.rightPivot.position.x),
                        currentStack.transform.localScale.y, currentStack.transform.localScale.z);

                currentBroken.localScale = new Vector3(brokenPiece,
                    currentBroken.transform.localScale.y, currentBroken.transform.localScale.z);
                currentBroken.position = currentStack.leftPivot.position;
                currentBroken.GetComponent<Rigidbody>().isKinematic = false;
                currentStack.transform.position += Vector3.left *
                                                   (currentStack.leftPivot.position.x -
                                                    beforeStack.leftPivot.position.x);


                // Debug.Log(brokenPiece);
            }
            else
            {
                var brokenPiece = currentStack.leftPivot.position.x - beforeStack.leftPivot.position.x;


                currentBroken.transform.position += Vector3.left *
                    (beforeStack.rightPivot.position.x - currentStack.rightPivot.position.x) / 2;


                currentBroken.gameObject.SetActive(true);

                //  currentStack.stackPiece.GetComponent<Rigidbody>().isKinematic = false;


                currentStack.transform.localScale =
                    new Vector3(Mathf.Abs(beforeStack.rightPivot.position.x - currentStack.leftPivot.position.x),
                        currentStack.transform.localScale.y, currentStack.transform.localScale.z);

                currentBroken.localScale = new Vector3(brokenPiece,
                    currentBroken.transform.localScale.y, currentBroken.transform.localScale.z);

                currentBroken.position = currentStack.rightPivot.position;
                currentBroken.GetComponent<Rigidbody>().isKinematic = false;
                currentStack.transform.position += Vector3.left *
                                                   (currentStack.rightPivot.position.x -
                                                    beforeStack.rightPivot.position.x);
            }

            yield return _waitForSeconds;
            GameManager.Instance.onStepFinish.Invoke();
        }

        // private void DetermineStackPiecePosition(int coefficient)
        // {
        //     currentStack.stackPiece.position += Vector3.right * (((currentStack.transform.localScale.x / 2) +
        //                                                           currentStack.stackPiece.lossyScale.x / 2) *
        //                                                          coefficient);
        // }

        public void StartNextStep()
        {
            Debug.Log("StartNextStep");
            var stackObj = ObjectPool.SharedInstance.GetPooledObject();

            var stack = stackObj.GetComponent<Stack>();
            currentStack = stack;
            steps.Add(stack);
            beforeStack = steps[steps.Count - 2];
            stack.transform.localScale = beforeStack.transform.localScale;
            stack.Start();
            if (stack.direction == Stack.Direction.Left)
            {
                stackObj.transform.position =
                    Vector3.left * 5 + Vector3.forward * (steps.Count - 1) * stackObj.transform.lossyScale.z;
                stack.endPos = Vector3.right * 5 +
                               Vector3.forward * (steps.Count - 1) * stackObj.transform.lossyScale.z;
            }
            else
            {
                stackObj.transform.position =
                    Vector3.right * 5 + Vector3.forward * (steps.Count - 1) * stackObj.transform.lossyScale.z;
                stack.endPos = Vector3.left * 5 + Vector3.forward * (steps.Count - 1) * stackObj.transform.lossyScale.z;
            }
        }
    }
}