using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsTester : MonoBehaviour
{
    public GameObject[] fruits;
    public float rotationSpeed = 1.0f;

    void Update()
    {
        for (int i = 0; i < fruits.Length; i++)
        {
            if (fruits[i] != null)
            {
                float direction = (i % 2 == 0) ? 1 : -1; // Alternate directions
                fruits[i].transform.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
