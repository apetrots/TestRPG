using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class BattleTile : MonoBehaviour
{
    public Status[] statuses;

    Vector2Int gridPos;
    SpriteRenderer rend;
    BattleTileType type;

    bool shouldUpdateComponents;

#region Properties
    public BattleTileType TileType 
    { 
        get { return type; } 
        set { 
            if (type != value) // if not the same type...
                shouldUpdateComponents = true;
            type = value; 
        } 
    }

    public Vector2Int GridPosition
    { 
        get { return gridPos; } 
        set { 
            if (gridPos != value) // if not the same position...
                shouldUpdateComponents = true;
            gridPos = value; 
        } 
    }
#endregion 

#region Functions
    void Start()
    {
        UpdateComponents();
    }

    /* Update the GameObject components based on the current tile type
    
     **DOES NOT change the tile's transform.position based on gridPos, 
        that's done in BattleGrid's functions
    */
    void UpdateComponents()
    {
        if (type == null)
        {
            gameObject.name = gridPos.ToString();
            return;
        }
        
        // set name from tile type name
        gameObject.name = gridPos.ToString() + " " + type.name;

        // this component has a [RequireComponent] tag with SpriteRenderer
        // so this SHOULDN'T error.
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = type.sprite;
    }

    void Update()
    {
        // if tile type was set externally (i.e. by BattleGrid)
        // then update things...
        if (shouldUpdateComponents)
        {
            UpdateComponents();
            shouldUpdateComponents = false;
        }
    }
#endregion

}
