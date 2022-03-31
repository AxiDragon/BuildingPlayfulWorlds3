using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickup : MonoBehaviour
{
    public static bool potionPickedUp = false;
    Camera mainCamera;
    float pickupTime = .7f;
    GameObject hitPotion;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        bool potionHit = false;
        RaycastHit[] hits = Physics.RaycastAll(mainCamera.transform.position, mainCamera.transform.forward, 1f);

        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Potion")
            {
                UIPrompts.fadeIn = true;
                UIPrompts.UpdateUIText("E - Grab Potion");
                potionHit = true;
                hitPotion = hit.collider.gameObject;
            }
        }

        if (potionHit)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(PickUpPotion(hitPotion));
                hitPotion.GetComponent<Collider>().enabled = false;
            }
        }
        else if (UIPrompts.texts[0].text == "E - Grab Potion")
            UIPrompts.fadeIn = false;
    }

    IEnumerator PickUpPotion(GameObject potion)
    {
        for(float t = 0f; t < pickupTime; t += Time.deltaTime)
        {
            potion.transform.position = Vector3.Lerp(potion.transform.position, transform.position + transform.forward * .15f + Vector3.down * .38f, t / pickupTime); ;
            yield return null;
        }

        potion.transform.parent = transform.parent;
        potionPickedUp = true;
        
    }
}
