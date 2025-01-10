using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {
    [SerializeField] protected string structureName;
    [SerializeField] protected Sprite structureSprite;
    [SerializeField] protected int resourceCost;

    public virtual void OnPlaced(Tile tile) {
        
        Debug.Log($"Placed {structureName} on tile {tile.name}");
    }
}
