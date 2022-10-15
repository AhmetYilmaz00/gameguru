using System;
using UnityEngine;

namespace Game.Scripts.GridGame
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private int numberEdge;
        private float _sideLength;
        private GridSize _gridSize;
        private Transform _gridTransform;
        private float _smallEdgeLength;

        private void Start()
        {
            _gridSize = GetComponent<GridSize>();
            _gridSize.ResizeSpriteToScreen();
            _gridTransform = _gridSize.transform;
            _sideLength = _gridTransform.localScale.x;
            _smallEdgeLength = _sideLength / numberEdge;
        }


        public void CreateGrid()
        {
            for (var y = 0; y < numberEdge; y++)
            {
                for (var x = 0; x < numberEdge; x++)
                {
                    var obj = ObjectPool.SharedInstance.GetPooledObject();
                    if (obj != null)
                    {
                        obj.transform.position = new Vector3(
                            (-1 * (_sideLength / 2) + (_smallEdgeLength / 2)) + _smallEdgeLength * x,
                            (_sideLength / 2) - (_smallEdgeLength / 2) - _smallEdgeLength * y, 0);
                    }
                }
            }
        }
    }
}