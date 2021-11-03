using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyle_Miami : Tyle_Initializator
{
    [SerializeField] private float elements_differenece_x = 3.57f;

    public override void InitializateSetting(Vector2 settingsElement_coordinates)
    {
        base.InitializateSetting(settingsElement_coordinates);

        SpawnElements(new Vector2(settingsElement_coordinates.x - elements_differenece_x, settingsElement_coordinates.y));
        SpawnElements(new Vector2(settingsElement_coordinates.x + elements_differenece_x, settingsElement_coordinates.y));
    }

    private void SpawnElements(Vector2 coordinates)
    {
        GameObject newElement = Instantiate(ElementsForThisSetting[Random.Range(0, ElementsForThisSetting.Length)]);
        newElement.transform.position = coordinates;
        newElement.transform.parent = this.transform;
    }
}
