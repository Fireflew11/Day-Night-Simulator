using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class TileMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private TileBase tile;

    public Vector2 targetPosition;
    public float speed = 1f;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPosition = rb.position;
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        rb.MovePosition(newPosition);
        tile.GetComponent<Transform>().position = newPosition;
    }
    public void MoveTo(Vector2 newPosition)
    {
        targetPosition = newPosition;
    }
}
