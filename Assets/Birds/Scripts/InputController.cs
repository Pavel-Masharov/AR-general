using AR.Birds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private CanvasDebuger _canvasDebuger;
    [SerializeField] private Bird _bird;
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<Bird>())
                {
                    GameObject touchedObject = hit.transform.gameObject;
                    _canvasDebuger.SetTextDebug("toch at " + touchedObject.name);
                    touchedObject.GetComponent<Bird>().EatAnimation();
                }
                return;
            }

            Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _canvasDebuger.SetTextDebug("pos Y: " + tapPosition.y);

            _bird.MoveToPosition(tapPosition.y);
        }
    }
}
