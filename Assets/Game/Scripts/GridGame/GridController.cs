using System;
using UnityEngine;

namespace Game.Scripts.GridGame
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private BackgroundSize backgroundSize;
        [SerializeField] private int numberEdge;
        [SerializeField] private GameObject gameobje;

        private float _sideLength;
        private Transform _background;
        private float _smallEdgeLength;

        private void Start()
        {
            backgroundSize.ResizeSpriteToScreen();
            _background = backgroundSize.transform;
            _sideLength = _background.localScale.x;
            _smallEdgeLength = _sideLength / numberEdge;

            CreateGrid();
        }

        private void CreateGrid()
        {
            for (var y = 0; y < numberEdge; y++)
            {
                for (var x = 0; x < numberEdge; x++)
                {
                    var a = Instantiate(gameobje, transform);
                    a.transform.position = new Vector3(
                        (-1 * (_sideLength / 2) + (_smallEdgeLength / 2)) + _smallEdgeLength * x,
                        (_sideLength / 2) - (_smallEdgeLength / 2) - _smallEdgeLength * y, 0);
                }
            }
        }
    }
}