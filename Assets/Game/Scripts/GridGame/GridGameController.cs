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
        private Vector2 _touchPoint;

        private void Start()
        {
            if (Camera.main != null)
            {
                _camera = Camera.main;
                _cameraTransform = _camera.transform;
            }
        }

        private void FixedUpdate()
        {
            SelectSquare();
        }

        private void SelectSquare()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchPoint = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
                _currentSquareHit = Physics2D.Raycast(_touchPoint, _cameraTransform.forward);
                Debug.Log("kanka ekrana tıkladın");

                if (_currentSquareHit.collider.gameObject.CompareTag("Square"))
                {
                    Debug.Log("kanka kareye tıkladın");
                  //  _currentSquareHit.collider.gameObject.GetComponent<SquareFeatures>().xSprite.enabled = true;
                }
            }
        }
    }
}