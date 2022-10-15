using UnityEngine;

namespace Game.Scripts.GridGame
{
    public class GridSize : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void ResizeSpriteToScreen()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                return;
            }

            transform.localScale = Vector3.one;

            var sprite = _spriteRenderer.sprite;
            var width = sprite.bounds.size.x;
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