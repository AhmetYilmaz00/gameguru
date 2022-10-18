using System;
using TMPro;
using UnityEngine;

namespace Game.Scripts.GridGame
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GameObject gridPiece;
        [SerializeField] private TMP_InputField numberEdgeText;

        public GridPiece[,] allPiece;
        public int numberEdge;

        private float _sideLength;
        private GridSize _gridSize;
        private Transform _gridTransform;
        private float _smallEdgeLength;

        private void Start()
        {
            _gridSize = GetComponent<GridSize>();
        }

        private void CorrectionGridSize()
        {
            _gridSize.ResizeSpriteToScreen();
            _gridTransform = _gridSize.transform;
            _sideLength = _gridTransform.localScale.x;
            _smallEdgeLength = _sideLength / numberEdge;
        }

        private void ControlGrid()
        {
            if (allPiece != null)
            {
                foreach (var gridPiece in allPiece)
                {
                    gridPiece.gameObject.SetActive(false);
                }
            }
        }

        private void NumberEdgeText()
        {
            if (numberEdgeText.text == string.Empty)
            {
                return;
            }

            numberEdge = int.Parse(numberEdgeText.text);
        }

        public void CreateGrid()
        {
            NumberEdgeText();
            CorrectionGridSize();
            ControlGrid();

            allPiece = new GridPiece[numberEdge, numberEdge];
            for (var y = 0; y < numberEdge; y++)
            {
                for (var x = 0; x < numberEdge; x++)
                {
                    var obj = ObjectPool.SharedInstance.GetPooledObject();
                    obj.transform.localScale = Vector3.one * _smallEdgeLength;
                    if (obj != null)
                    {
                        obj.transform.position = new Vector3(
                            (-1 * (_sideLength / 2) + (_smallEdgeLength / 2)) + _smallEdgeLength * x,
                            (_sideLength / 2) - (_smallEdgeLength / 2) - _smallEdgeLength * y, 0);

                        allPiece[y, x] = obj.GetComponent<GridPiece>();
                    }
                }
            }
        }
    }
}