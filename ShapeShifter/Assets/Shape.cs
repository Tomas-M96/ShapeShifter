using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 1f;

    Rigidbody2D rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        DestroyShape();
    }
    private void DestroyShape()
    {
        //If the shape is lower than -6 on the Y axis
        if (gameObject.transform.position.y <= -6f)
        {
            //Destroy it
            Destroy(gameObject);
        }
    }
}
