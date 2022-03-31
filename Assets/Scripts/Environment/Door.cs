using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float closeRotation, openRotation, rotationSpeed;
    float rotateLeeway = 15f;
    bool isMoving, inRange, open = false;
    public static bool doorsLocked = false;
    Vector3 pivot;

    void Start() => pivot = GetComponentInChildren<PivotComponent>().gameObject.transform.position;

    void Update()
    {
        if (doorsLocked)
            return;

        if (open && !inRange && !isMoving)
            StartCoroutine(DoorSequence(open ? closeRotation : openRotation));

        if (inRange && Input.GetKeyDown(KeyCode.E) && !isMoving)
            StartCoroutine(DoorSequence(open ? closeRotation : openRotation));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
            UIPrompts.fadeIn = true;

            string promptAdd = (name.Contains("1") && PotionPickup.potionPickedUp) ? " and drink Potion" : null;
            UIPrompts.UpdateUIText("E - Use Door" + promptAdd);

            //StartCoroutine(UIPrompts.UIFade(true, "E - Use Door"));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            UIPrompts.fadeIn = false;
            //StartCoroutine(UIPrompts.UIFade(false, null));
        }
    }

    IEnumerator DoorSequence(float targetRotation)
    {
        open = !open;
        isMoving = true;

        float rotationDirection = (Mathf.Abs(targetRotation - 360f) > Mathf.Abs(targetRotation)) ? 1f : -1f;

        while(transform.eulerAngles.y < targetRotation - rotateLeeway || transform.eulerAngles.y > targetRotation + rotateLeeway)
        {
            transform.RotateAround(pivot, Vector3.up, Time.deltaTime * rotationSpeed * rotationDirection);
            yield return null;
        }

        isMoving = false;
    }
}
