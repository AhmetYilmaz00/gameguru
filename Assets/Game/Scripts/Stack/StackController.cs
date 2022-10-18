using UnityEngine;

namespace Game.Scripts.Stack
{
    public class StackController : MonoBehaviour
    {
        private Stack _currentStack;
        private void Start()
        {
        }

        private void Update()
        {
        }

        public void StackStopAndCut()
        {
            _currentStack.speed = 0;
            _currentStack.stackPiece.gameObject.SetActive(true);
        }
    }
}