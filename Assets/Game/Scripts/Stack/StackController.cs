using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Stack
{
    public class StackController : MonoSingleton<StackController>
    {
        [SerializeField] private Stack currentStack;
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
            currentStack= steps[steps.Count - 1].GetComponent<Stack>();
            currentStack.speed = 0;
            currentStack.stackPiece.gameObject.SetActive(true);
            _initCurrentStackPosition = currentStack.transform.position;
            currentStack.transform.localScale -= Vector3.right * Mathf.Abs(_initCurrentStackPosition.x);
            currentStack.transform.position -= Vector3.right * (_initCurrentStackPosition.x / 2);
            currentStack.stackPiece.localScale = new Vector3(
                (currentStack.stackPiece.localScale.x / currentStack.transform.localScale.x) *
                Mathf.Abs(currentStack.transform.position.x) * 2, currentStack.stackPiece.localScale.y,
                currentStack.stackPiece.localScale.z);
            if (currentStack.stackPiece.position.x > 0)
            {
                DetermineStackPiecePosition(1);
            }
            else
            {
                DetermineStackPiecePosition(-1);
            }

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
            steps.Add(obj.GetComponent<Stack>());
            obj.transform.position = Vector3.forward * steps.Count * obj.transform.lossyScale.z;
        }
    }
}