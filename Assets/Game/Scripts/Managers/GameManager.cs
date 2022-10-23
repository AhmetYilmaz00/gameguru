using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public UnityEvent onTouch;
        public UnityEvent onStepFinish;
        public UnityEvent onStepGoodFinish;
        public UnityEvent onStartNextStep;
        public UnityEvent onFallStart;
        public UnityEvent onFailLevel;
        public UnityEvent onSuccesLevel;
        public bool firstTouch = true;
        public int stepCount = 7;
        public bool isReadyTouch;

        private void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (isReadyTouch)
                {
                    onTouch.Invoke();
                    isReadyTouch = false;
                }
            }
        }
    }
}