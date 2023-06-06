using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GrabObject grabObject;
    [SerializeField] CameraFollow _camera;
    [SerializeField] GameObject textGrab;
    [SerializeField] GameObject textLaunch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tutorial1();
    }

    private void tutorial1()
    {
        if (grabObject.isHolding && textGrab.activeSelf)
        {
            textGrab.SetActive(false);
            textLaunch.SetActive(true);
            _camera.isFollow = true;
            _camera.size = 23;
        }
        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
        {
            textLaunch.SetActive(false);
        }
        return;
    }
}
