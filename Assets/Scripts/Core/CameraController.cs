using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = Vector2.zero;

        if (Input.touchCount > 0)
        {
            Debug.Log(Input.GetTouch(0).deltaPosition);
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                delta = Input.GetTouch(0).deltaPosition;
                delta.y *= 0.05f;
            }
        }

        else if (Input.GetMouseButton(0))
        {
            delta.y = Input.GetAxis("Mouse Y") * 0.1f;
        }

        transform.Translate(0, -delta.y, 0);

        Camera.main.orthographicSize = 14 * (float)Screen.height / (float)Screen.width * 0.5f;
    }
}
