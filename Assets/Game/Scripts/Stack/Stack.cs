using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Stack
{
    public class Stack : MonoBehaviour
    {
        public Vector3 endPos;
        public float speed;
        public Transform stackPiece;
        public bool isRandom = true;
        public bool isLeft;
        private bool _isStop;

        public void Start()
        {
            GameManager.Instance.onTouch.AddListener(StopMove);
            if (isRandom)
            {
                if (Random.Range(0, 2) == 0)
                {
                    isLeft = true;
                }
                else
                {
                    isLeft = false;
                }
            }
        }


        private void Update()
        {
            if (!_isStop)
            {
                transform.position = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
            }
        }

        private void StopMove()
        {
            _isStop = true;
        }
    }
}