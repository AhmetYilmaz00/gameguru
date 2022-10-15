using UnityEngine;

namespace Game.Scripts.GridGame
{
    public class BackgroundSize : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
        }

        public void ResizeSpriteToScreen()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                return;
            }

            transform.localScale = Vector3.one;

            var sprite = _spriteRenderer.sprite;
            var width = sprite.bounds.size.x;
            var height = sprite.bounds.size.y;

            if (Camera.main != null)
            {
                var worldScreenHeight = Camera.main.orthographicSize * 2;
                var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

                transform.localScale = new Vector3(worldScreenWidth / width, worldScreenWidth / width,
                    transform.localScale.z);
            }
        }
    }
}