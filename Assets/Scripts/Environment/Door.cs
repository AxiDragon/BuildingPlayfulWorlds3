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
    bool finalDoor;

    void Start()
    {
        finalDoor = name.Contains("1");
        pivot = GetComponentInChildren<PivotComponent>().gameObject.transform.position;
    }

    void Update()
    {
        if (doorsLocked)
            return;

        if (open && !inRange && !isMoving)
            StartCoroutine(DoorSequence(open ? closeRotation : openRotation));

        switch (finalDoor)
        {
            case true:
                if ((inRange && Input.GetKeyDown(KeyCode.E) && !isMoving && PotionComposition.hasIngredients))
                    StartCoroutine(DoorSequence(open ? closeRotation : openRotation));
                break;
            case false:
                if ((inRange && Input.GetKeyDown(KeyCode.E) && !isMoving))
                    StartCoroutine(DoorSequence(open ? closeRotation : openRotation));
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
            UIPrompts.fadeIn = true;

            string prompt;

            switch (finalDoor)
            {
                case true:
                    if (!PotionPickup.potionPickedUp)
                    {
                        prompt = "Pick up the potion on the table first, please";
                        break;
                    }
                    if (!PotionComposition.hasIngredients)
                    {
                        prompt = "Go get some ingredients from the crates first, please";
                        break;
                    }
                    prompt = "E - Use Door and drink Potion";
                    break;
                case false:
                    prompt = "E - Use Door";
                    break;
            }
            
            //prompt = (name.Contains("1") && PotionPickup.potionPickedUp) ? " and drink Potion" : null;
            UIPrompts.UpdateUIText(prompt);

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

    void OnDestroy()
    {
        doorsLocked = false;    
    }
}
