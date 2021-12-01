using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISway : MonoBehaviour
{
    public float amount;

    public float maxAmount;

    public float smoothing;

    public GameObject bhg;

    private Vector2 startPosition;

    private Vector2 startPosition_2;

    private Quaternion startPosition_3;


    void Start()
    {
        startPosition = transform.localPosition;

        startPosition_2 = transform.localPosition;

        startPosition_3 = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount; // Grabs the mouse movements
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount); //clamps the movement between two points
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector2 finalPosition = new Vector2(movementX, movementY);
        transform.localPosition = Vector2.Lerp(transform.localPosition, finalPosition + startPosition, Time.deltaTime * smoothing);
    }
}
