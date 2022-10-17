using System;
using UnityEngine;

namespace Game.Scripts.GridGame
{
    public class GridGameController : MonoBehaviour
    {
        private float _requireDistance;
        private RaycastHit2D _currentSquareHit;
        private Camera _camera;
        private Transform _cameraTransform;
        private Vector3 _touchPoint;

        private void Start()
        {
            if (Camera.main != null)
            {
                _camera = Camera.main;
                _cameraTransform = _camera.transform;
            }
        }

        private void Update()
        {
            SelectSquare();
        }

        private void SelectSquare()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                var touch = Input.GetTouch(0);
                _touchPoint = _camera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                Debug.Log(_touchPoint);
                _currentSquareHit = Physics2D.Raycast(_touchPoint, _camera.transform.forward);
                Debug.DrawRay(_touchPoint, _camera.transform.forward, Color.green);


                if (_currentSquareHit.collider == null)
                {
                    return;
                }

                if (_currentSquareHit.collider.CompareTag("Square"))
                {
                    _currentSquareHit.collider.GetComponent<GridPieceFeatures>().multiplictaion.color = Color.white;
                }
            }
        }
    }
}