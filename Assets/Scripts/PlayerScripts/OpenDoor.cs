using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Camera mainCamera;
    public float rotation, rotationTime; //-135, 1
    bool isOpening = false;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 2f) && !isOpening)
        {

            if (hit.collider.tag == "Door")
            {
                isOpening = true;
                StartCoroutine(OpenDoorSequence(hit.collider.gameObject, hit.collider.GetComponentInChildren<PivotComponent>().gameObject.transform.position));
            }
        }
    }

    IEnumerator OpenDoorSequence(GameObject door, Vector3 pivot)
    {
        print("trying!");

        while (door.transform.eulerAngles.y != rotation)
        {
            door.transform.RotateAround(pivot, Vector3.up, rotation * Time.deltaTime / rotationTime);
            yield return null;
        }
    }
}
