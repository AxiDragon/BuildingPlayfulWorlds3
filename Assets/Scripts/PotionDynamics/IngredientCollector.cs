using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCollector : MonoBehaviour
{
    public GameObject[] flowers;
    Camera mainCamera;
    bool lookingAtCrate = false;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);



        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.collider.tag == "Crate")
            {
                if (!lookingAtCrate)
                {
                    lookingAtCrate = true;
                    StartCoroutine(UIPrompts.UIFade(true, "E - Add " + hit.collider.name));
                }
                else
                    UIPrompts.UpdateUIText("E - Add " + hit.collider.name);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    print("yes");
                    PotionComposition.AddIngredient(hit.collider.name);
                    hit.collider.gameObject.GetComponentInChildren<Rigidbody>()
                        .AddForce(Vector3.one * Random.Range(3f, 10f));
                }
            }
            else
                lookingAtCrate = false;
        }

        if (!lookingAtCrate && UIPrompts.texts[0].text.Contains("E - Add "))
            StartCoroutine(UIPrompts.UIFade(false, null));
    }
}
