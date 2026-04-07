using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // This will hold a reference to the player's position
    private Transform playerTransform;

    // This is a VARIABLE — it stores a value we can change
    public float tileSize = 20f; // Size of the background tile

    void Start()
    {
        // Find the player GameObject by its tag and get its Transform component
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's current position
        Vector2 playerPos = playerTransform.position;

        // Calculate the new position for the background based on the player's position
        float newX = Mathf.Round(playerPos.x / tileSize) * tileSize;
        float newY = Mathf.Round(playerPos.y / tileSize) * tileSize;

        transform.position = new Vector3(newX, newY, 0f);
    }
}
