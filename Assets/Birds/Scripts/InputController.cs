using AR.Birds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private CanvasDebuger _canvasDebuger;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<Bird>())
                {
                    GameObject touchedObject = hit.transform.gameObject;
                    _canvasDebuger.SetTextDebug("toch at " + touchedObject.name);

                    touchedObject.GetComponent<Bird>().EatAnimation();
                }
            }
        }
    }
}
