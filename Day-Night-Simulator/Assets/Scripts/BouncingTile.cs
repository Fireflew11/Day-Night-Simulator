using UnityEngine;
using UnityEngine.Tilemaps;

public class BouncingTile : MonoBehaviour
{

    [SerializeField]
    public Tilemap addToTilemap;
    [SerializeField]
    public Tilemap removeFromTilemap;
    [SerializeField]
    public TileBase tileBase;
    private Rigidbody2D rb;

    Vector3 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(15f, 15f));
        // Set the initial velocity based on the specified direction and speed
        //rb.velocity = initialDirection.normalized * speed;
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * speed;

        foreach(var contact in collision.contacts)
        {
            Vector3 worldPoint = contact.point;
            Vector3Int gridPosition = addToTilemap.WorldToCell(worldPoint);
            addToTilemap.SetTile(gridPosition, tileBase);
            removeFromTilemap.SetTile(gridPosition, null);
        }

    }
}
