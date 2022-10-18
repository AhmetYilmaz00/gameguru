using UnityEngine;

namespace Game.Scripts.Stack
{
    public class Stack : MonoBehaviour
    {
        public float speed;
        public Transform stackPiece ;


        private void Update()
        {
            var pos = transform.position;
            transform.position = new Vector3(Mathf.Sin(Time.time * speed), pos.y, pos.z);
        }
    }
}