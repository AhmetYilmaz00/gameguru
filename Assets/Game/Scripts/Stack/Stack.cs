using UnityEngine;

namespace Game.Scripts.Stack
{
    public class Stack : MonoBehaviour
    {
        [SerializeField] private float speed;
        

        private void Update()
        {
            var pos = transform.position;
            transform.position = new Vector3(Mathf.Sin(Time.time * speed), pos.y, pos.z);
        }
    }
}