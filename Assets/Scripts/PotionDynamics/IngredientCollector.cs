using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCollector : MonoBehaviour
{
    Camera mainCamera;
    GameObject hitCrate;

    void Start() => mainCamera = GetComponent<Camera>();

    void Update()
    {
        if (!PotionPickup.potionPickedUp)
            return;

        bool crateHit = false;

        float crateDistance = Mathf.Infinity;
        List<GameObject> hitCrates = new List<GameObject>();
        RaycastHit[] hits = Physics.RaycastAll(mainCamera.transform.position, mainCamera.transform.forward, 4f);

        //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //if (Physics.RaycastAll(ray, out hits, 4f))
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Crate")
            {
                hitCrates.Add(hit.collider.gameObject);
                crateHit = true;
            }
        }

        foreach (GameObject crate in hitCrates)
        {
            if (Vector3.Magnitude(crate.transform.position - transform.position) < crateDistance)
            {
                crateDistance = Vector3.Magnitude(crate.transform.position - transform.position);
                hitCrate = crate;
            }
        }

        if (crateHit)
        {
            UIPrompts.fadeIn = true;
            UIPrompts.UpdateUIText("E - Add " + hitCrate.name);
            //StartCoroutine(UIPrompts.UIFade(true, "E - Add " + hit.collider.name));

            if (Input.GetKeyDown(KeyCode.E))
            {
                PotionComposition.AddIngredient(hitCrate.name);
                StartCoroutine(PickUpFlower(hitCrate.gameObject.GetComponentInChildren<Rigidbody>()));
            }
        }
        else if (UIPrompts.texts[0].text.Contains("E - Add "))
            UIPrompts.fadeIn = false;

        //StartCoroutine(UIPrompts.UIFade(false, null));
    }

    IEnumerator PickUpFlower(Rigidbody rb)
    {
        rb.AddTorque(Vector3.one * Random.Range(-10f, 10f));
        
        for (float t = 0f; t < 1f; t += Time.deltaTime)
        {
            rb.MovePosition(rb.position + Vector3.up / 100f * (1f - t)); 
            yield return null;
        }
    }
}
