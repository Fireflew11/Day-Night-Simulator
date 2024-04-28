using Unity.Mathematics;
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
    [SerializeField]
    public float offset;
    [SerializeField]
    Vector3[] directions = null;
    [SerializeField]
    GameObject movingTilePrefab;
    private Rigidbody2D rb;

    Vector3 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(100f, 90f));
    }

    void Update()
    {
        lastVelocity = rb.velocity;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        for (int i = 0; i < collision.contacts.Length; i++)
        {
            //if (gameObject.name == "BouncingGrayTile")
            //    Debug.Log(i);
            if (i != 0 && (math.abs(collision.contacts[i - 1].point.x - collision.contacts[i].point.x) < 0.01f
                       ||  math.abs(collision.contacts[i - 1].point.y - collision.contacts[i].point.y) < 0.01f))
                continue;
            var magni = rb.velocity.magnitude;
            
            var speed = lastVelocity.magnitude;
            Vector2 v;
            if (math.abs(collision.contacts[i].normal.x) > math.abs(collision.contacts[i].normal.y))
                v = new Vector2(collision.contacts[i].normal.x, 0).normalized;
            else
                v = new Vector2(0, collision.contacts[i].normal.y).normalized;
            var newDirection = Vector3.Reflect(lastVelocity.normalized, v);
            if (gameObject.name == "BouncingGrayTile")
                Debug.Log(lastVelocity.normalized + "   " + collision.contacts[i].normal + "    " + newDirection);
            rb.velocity = newDirection * speed;
            lastVelocity = rb.velocity;
        }
        //rb.velocity = rb.velocity.normalized * ;
       
        var contact = collision.contacts[0];
        Vector3 worldPoint = contact.point;
        Vector3Int gridPosition = Vector3Int.zero;
        foreach (var direction in directions)
        {
            gridPosition = removeFromTilemap.WorldToCell(worldPoint + direction.normalized * offset);
            if (removeFromTilemap.GetTile(gridPosition) != null)
                break;
        }
        addToTilemap.SetTile(gridPosition, tileBase);
        removeFromTilemap.SetTile(gridPosition, null);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (gameObject.name == "BouncingGrayTile")
            Debug.Log(rb.velocity.magnitude);
        var speed = lastVelocity.magnitude;
        if (rb.velocity.magnitude < 26 )
        {
            Vector2 v;
            if (math.abs(collision.contacts[0].normal.x) > math.abs(collision.contacts[0].normal.y))
                v = new Vector2(collision.contacts[0].normal.x, 0).normalized;
            else
                v = new Vector2(0, collision.contacts[0].normal.y).normalized;
            var newDirection = Vector3.Reflect(lastVelocity.normalized, v);
            rb.velocity = newDirection * speed;
            lastVelocity = rb.velocity;
        }
    }
}
