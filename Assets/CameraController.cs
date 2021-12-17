using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera viewCamera;
    [SerializeField] private float clickTimeThreshold,clickTimer;
    private Vector3 recordedInitialPosition, recordedFinalPosition;

    private bool isClicking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            recordedInitialPosition = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            clickTimer = Time.time;
            isClicking = true;
        }

        if (isClicking)
        {
            if (Time.time - clickTimer > clickTimeThreshold)
            {
                recordedFinalPosition = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                viewCamera.transform.position += recordedInitialPosition - recordedFinalPosition;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            
            isClicking = false;

        }
    }
}
