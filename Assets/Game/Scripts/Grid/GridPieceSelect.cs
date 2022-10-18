using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Grid
{
    public class GridPieceSelect : MonoBehaviour
    {
        private float _requireDistance;
        private RaycastHit2D _currentSquareHit;
        private Camera _camera;
        private Vector3 _touchPoint;
        private GridController _gridController;
        private HashSet<GridPiece> _deleteGridPieces = new();

        private void Start()
        {
            if (Camera.main != null)
            {
                _camera = Camera.main;
            }

            _gridController = GetComponent<GridController>();
        }


        public void SelectSquare()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                var touch = Input.GetTouch(0);
                _touchPoint = _camera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                _currentSquareHit = Physics2D.Raycast(_touchPoint, _camera.transform.forward);
                if (_currentSquareHit.collider == null)
                {
                    return;
                }

                if (_currentSquareHit.collider.CompareTag("Square"))
                {
                    var gridPieceFeatures = _currentSquareHit.collider.GetComponent<GridPiece>();
                    gridPieceFeatures.multiplictaion.color = Color.white;
                    gridPieceFeatures.isActiveMultiplictaion = true;
                    var allPiece = _gridController.allPiece;

                    for (var y = 0; y < allPiece.GetLength(0); y++)
                    {
                        for (var x = 0; x < allPiece.GetLength(1); x++)
                        {
                            if (allPiece[y, x] == gridPieceFeatures)
                            {
                                NearGridsControl(ref allPiece, y, x);
                            }
                        }
                    }

                    GridsClear();
                }
            }
        }

        private void NearGridsControl(ref GridPiece[,] allPiece, int y, int x)
        {
            _deleteGridPieces.Add(allPiece[y, x]);
            GridControl(ref allPiece, y - 1, x);
            GridControl(ref allPiece, y + 1, x);
            GridControl(ref allPiece, y, x - 1);
            GridControl(ref allPiece, y, x + 1);
        }

        private void GridControl(ref GridPiece[,] allPiece, int y, int x)
        {
            if (y >= _gridController.numberEdge || x >= _gridController.numberEdge || y < 0 || x < 0)
            {
                return;
            }

            if (allPiece[y, x].isActiveMultiplictaion && !_deleteGridPieces.Contains(allPiece[y, x]))
            {
                _deleteGridPieces.Add(allPiece[y, x]);
                NearGridsControl(ref allPiece, y, x);
            }
        }

        private void GridsClear()
        {
            if (_deleteGridPieces.Count < 3)
            {
                _deleteGridPieces.Clear();
                return;
            }

            foreach (var deleteGridPiece in _deleteGridPieces)
            {
                deleteGridPiece.multiplictaion.color = Color.clear;
                deleteGridPiece.isActiveMultiplictaion = false;
            }

            _deleteGridPieces.Clear();
        }
    }
}