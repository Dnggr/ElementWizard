using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    // Drag your background sprite here in the Inspector
    public Sprite backgroundSprite;

    // How big is one background tile in Unity units?
    // If your image is 2000px and PPU is 100, this is 20
    public float tileSize = 20f;

    // We'll store all 9 tile GameObjects here
    private GameObject[,] tiles = new GameObject[3, 3];

    // We need to know where the player is
    private Transform playerTransform;

    // Where was the player last frame?
    // We use this to know when to reposition tiles
    private Vector2 lastPlayerPos;

    void Start()
    {
        // Find the player
        playerTransform = GameObject.FindWithTag("Player").transform;
        lastPlayerPos = playerTransform.position;

        // Create all 9 tiles in a 3x3 grid
        // Grid positions: (-1,-1) (0,-1) (1,-1)
        //                 (-1, 0) (0, 0) (1, 0)
        //                 (-1, 1) (0, 1) (1, 1)
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                // Create a new empty GameObject
                GameObject tile = new GameObject("BackgroundTile_" + x + "_" + y);

                // Add a SpriteRenderer to it so it can display an image
                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
                sr.sprite = backgroundSprite;

                // Put it behind everything else
                sr.sortingOrder = -10;

                // Position it in the grid
                // (x-1) and (y-1) shifts it so: 0,0,0 becomes -1,-1 / 0,0 / 1,1
                float posX = playerTransform.position.x + (x - 1) * tileSize;
                float posY = playerTransform.position.y + (y - 1) * tileSize;
                tile.transform.position = new Vector3(posX, posY, 0f);

                // Make the tile the same size as tileSize
                // SpriteRenderer size depends on sprite, so we scale it
                tile.transform.localScale = Vector3.one;

                // Store it in our grid array
                tiles[x, y] = tile;
            }
        }
    }

    void Update()
    {
        RepositionTiles();
    }

    void RepositionTiles()
    {
        // Check each of the 9 tiles
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                GameObject tile = tiles[x, y];
                Vector3 tilePos = tile.transform.position;
                Vector3 playerPos = playerTransform.position;

                // How far is this tile from the player?
                float distX = tilePos.x - playerPos.x;
                float distY = tilePos.y - playerPos.y;

                // If the tile is MORE than 1.5 tiles away from the player,
                // it's completely off screen — move it to the other side!
                // 1.5 * tileSize means it just left the 3x3 grid boundary

                if (distX > tileSize * 1.5f)
                {
                    // Tile is too far RIGHT — move it to the LEFT
                    tile.transform.position += new Vector3(-tileSize * 3, 0, 0);
                }
                else if (distX < -tileSize * 1.5f)
                {
                    // Tile is too far LEFT — move it to the RIGHT
                    tile.transform.position += new Vector3(tileSize * 3, 0, 0);
                }

                if (distY > tileSize * 1.5f)
                {
                    // Tile is too far UP — move it DOWN
                    tile.transform.position += new Vector3(0, -tileSize * 3, 0);
                }
                else if (distY < -tileSize * 1.5f)
                {
                    // Tile is too far DOWN — move it UP
                    tile.transform.position += new Vector3(0, tileSize * 3, 0);
                }
            }
        }
    }
}