using UnityEngine;

namespace AR.Birds
{
    public class InputController : MonoBehaviour
    {
        private Bird _bird;

        void Update()
        {
            if (_bird == null)
                return;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider != null)
                    {
                        GameObject touchedObject = hit.transform.gameObject;
                        if (touchedObject.GetComponent<Bird>())                       
                            touchedObject.GetComponent<Bird>().EatAnimation();                     
                        else if (touchedObject.GetComponent<AreaTap>())                       
                            _bird.MoveToPosition(touchedObject.GetComponent<AreaTap>());            
                    }
                }
            }
        }

        public void SetBird(Bird bird)
        {
            _bird = bird;
        }
    }
}
