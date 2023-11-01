using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BattleGrid : MonoBehaviour
{
    [SerializeField]
    int Rows = 3, Columns = 6;
    [SerializeField]
    float VerticalSpacing = 3.0f, HorizontalSpacing = 6.5f;

    [SerializeField]
    BattleTile prefabTile;
    
    [SerializeField]
    BattleTileType[] tiles = new BattleTileType[18];

    List<BattleTile> tileObjects = new List<BattleTile>();

    bool shouldUpdateTiles = false;

    // Create or delete tiles based on current size while setting
    // tile types for each BattleTile
    void UpdateTiles()
    {
        if (tiles.Length > Rows*Columns)
        {
            Debug.LogWarning("Too many tiles in tileTypes compared to the amount specified in rows and column count, ignoring");
        }
        if (tiles.Length < Rows*Columns)
        {
            Debug.LogWarning("Too little tiles in tileTypes compared to the amount specified in rows and column count, adding empty");
        }

        while (tileObjects.Count < Rows*Columns)
        {
            // set it to default position and rotation for now, 
            // will align all to grid and set other BattleTile
            // variables later in this function
            GameObject newObj = Instantiate(prefabTile.gameObject, Vector2.zero, Quaternion.identity, transform);

            BattleTile newTile = newObj.GetComponent<BattleTile>();

            tileObjects.Add(newTile);
        }

        while (tileObjects.Count > Rows*Columns)
        {
            // Because they are a GameObject that we're "managing"
            // with this BattleGrid class, we need to destroy it
            // from the Unity scene... 
            Destroy(tileObjects[tileObjects.Count - 1].gameObject);
            tileObjects.RemoveAt(tileObjects.Count - 1);
        }
        
        // re-position to grid
        for (int i = 0; i < tileObjects.Count; i++)
        {
            // grid position 
            Vector2Int gridPos = new Vector2Int(
                i % Columns, 
                Mathf.FloorToInt(i / Columns)
            );
            // relative to BattleGrid's origin
            Vector3 relPos = new Vector3(
                ((float)gridPos.x - (Columns-1) * 0.5f) * HorizontalSpacing,
                ((float)gridPos.y - (Rows-1) * 0.5f) * VerticalSpacing,
                0.0f
            );

            tileObjects[i].transform.localPosition = relPos;
            tileObjects[i].GridPosition = gridPos;
            // if tile type specified in public "tiles" array 
            if (i < tiles.Length)
                tileObjects[i].TileType = tiles[i]; 
        }
    }

    void Start()
    {
        UpdateTiles();
    }

    void Update()
    {
        if (shouldUpdateTiles)
        {
            UpdateTiles();
            shouldUpdateTiles = false;
        }
    }

    // "Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector." 
    void OnValidate()
    {
        shouldUpdateTiles = true;
    }
}
