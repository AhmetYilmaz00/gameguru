using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public UnityEvent onTouch;
        public UnityEvent onStepFinish;
        public UnityEvent onStartNextStep;

        private void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                onTouch.Invoke();
            }
        }
    }
}