using UnityEngine;

namespace Game.Scripts.Stack
{
    public class StackController : MonoBehaviour
    {
        [SerializeField] private Stack currentStack;
        [SerializeField] private float tolerance;
        private Vector3 _initCurrentStackPosition;

        public void ControlStep()
        {
            if (Mathf.Abs(currentStack.transform.position.x) <= tolerance)
            {
                SuccesStep();
            }
            else
            {
                StackStopAndCut();
            }
        }

        private void SuccesStep()
        {
            Debug.Log("HELLALL");
        }

        private void StackStopAndCut()
        {
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

            currentStack.stackPiece.GetComponent<Rigidbody>().isKinematic = false;
        }

        private void DetermineStackPiecePosition(int coefficient)
        {
            currentStack.stackPiece.position += (Vector3.right *
                                                 ((currentStack.transform.localScale.x / 2) +
                                                  currentStack.stackPiece.lossyScale.x / 2)) * coefficient;
        }
    }
}