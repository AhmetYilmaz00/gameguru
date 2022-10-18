using System;
using UnityEngine;

namespace Game.Scripts.Stack
{
    public class Stack : MonoBehaviour
    {
        public float speed;
        public Transform stackPiece;
        private bool _isStop;

        private void Start()
        {
            GameManager.Instance.onTouch.AddListener(StopMove);
        }

        private void Update()
        {
            if (!_isStop)
            {
                var pos = transform.position;
                transform.position = new Vector3(Mathf.Sin(Time.time * speed), pos.y, pos.z);
            }
        }

        private void StopMove()
        {
            _isStop = true;
        }
    }
}