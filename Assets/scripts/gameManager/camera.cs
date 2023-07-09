
using UnityEngine;

public class camera : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 previousMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            previousMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePosition;
            Vector3 newPosition = transform.position - deltaMousePosition * 0.02f;
            if (newPosition.y < 0)
            {
                newPosition.y = 0;
            }
            else if (newPosition.y > 3)
            {
                newPosition.y = 3;
            }
            if (newPosition.x <= -1)
            {
                newPosition.x = -1;

            }
            else if (newPosition.x >=27)
            {
                newPosition.x = 27;
            }
            transform.position = newPosition;
        }

        previousMousePosition = Input.mousePosition;
    }
}
