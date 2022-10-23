using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Stack
{
    public class Stack : MonoBehaviour
    {
        public Vector3 endPos;
        public float speed;
        public Transform leftPivot;
        public Transform rightPivot;

        public bool isRandom = true;
        private bool _isStop;
        public Direction direction;
        public State state;

        public enum State
        {
            FirstHalfRoad,
            SecondHalfRoad
        }

        public enum Direction
        {
            Left,
            Right
        }

        public void Start()
        {
            GameManager.Instance.onTouch.AddListener(StopMove);
            if (isRandom)
            {
                if (Random.Range(0, 2) == 0)
                {
                    direction = Direction.Left;
                }
                else
                {
                    direction = Direction.Right;
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