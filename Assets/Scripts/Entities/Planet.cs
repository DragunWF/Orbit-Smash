using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Planet : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private void Start()
    {

    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Debug.Log("Planet clicked!");
        Destroy(gameObject);
    }
}
