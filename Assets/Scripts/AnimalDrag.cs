using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDrag : MonoBehaviour
{
    public bool dragging = false;

    [SerializeField] float gridSnapSize = 5;
    [SerializeField] float fixedHieght = 5;

    private void Update()
    {
        if (dragging)//_drag == null &&
        {
            Transform draggingObject = transform;

            Plane plane = new Plane(Vector3.up, Vector3.up * fixedHieght); // ground plane

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float distance; // the distance from the ray origin to the ray intersection of the plane
            if (plane.Raycast(ray, out distance))
            {
                Vector3 rayPoint = ray.GetPoint(distance);
                Vector3 snappedRayPoint = rayPoint;
                snappedRayPoint.x = (Mathf.RoundToInt(rayPoint.x / gridSnapSize) * gridSnapSize);
                snappedRayPoint.z = (Mathf.RoundToInt(rayPoint.z / gridSnapSize) * gridSnapSize);
                draggingObject.position = snappedRayPoint;
            }
        }
    }
    void OnMouseDown()
    {
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;

        if (isTouchingAnotherAnimal())
            return;

        if (!IsGrounded())
        {
            Destroy(gameObject);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 0.1f, GameManager.instance.groundMask);
    }

    private bool isTouchingAnotherAnimal()
    {
        var touchingObjects = Physics.OverlapSphere(transform.position, transform.localScale.z);

        foreach(var obj in touchingObjects)
        {
            var animal = obj.GetComponent<Animal>();
            var thisAnimal = GetComponent<Animal>();

            if(animal && thisAnimal)
            {
                if (animal != thisAnimal && animal.AnimalLevel == thisAnimal.AnimalLevel)
                {
                    Instantiate(AnimalSpawner.instance.GetAnimal(thisAnimal.AnimalLevel++), obj.transform.position, default);

                    Destroy(obj.gameObject);
                    Destroy(gameObject);

                    return true;
                }
            }
        }

        return false;
    }
}
