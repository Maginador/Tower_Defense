using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private Camera viewCamera;
        [SerializeField] private float clickTimeThreshold,clickTimer;
        private Vector3 _recordedInitialPosition, _recordedFinalPosition;

        private bool _isClicking;

        private int _currentZoomLevel = 0, _minZoomLevel = -3, _maxZoomLevel = 3;

        public void ZoomIn()
        {
            if (_currentZoomLevel < _maxZoomLevel)
            {
                viewCamera.fieldOfView -= 10;
                _currentZoomLevel++;
            }
        }

        public void ZoomOut()
        {
            if (_currentZoomLevel > _minZoomLevel)
            {
                viewCamera.fieldOfView += 10;
                _currentZoomLevel--;

            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _recordedInitialPosition = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                clickTimer = Time.time;
                _isClicking = true;
            }

            if (_isClicking)
            {
                if (Time.time - clickTimer > clickTimeThreshold)
                {
                    _recordedFinalPosition = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                    viewCamera.transform.position += _recordedInitialPosition - _recordedFinalPosition;
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
            
                _isClicking = false;

            }
        }
    }
}
