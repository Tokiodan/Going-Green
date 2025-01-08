using UnityEngine;

[CreateAssetMenu(fileName = "New Structure", menuName = "Structure/Structure Data")]
public class StructureData : ScriptableObject
{
    public GameObject prefab;        // The prefab of the structure
    public string structureName;     // Name of the structure (e.g., Corn Farm, House)
    public int cost;                 // Cost to place the structure
}
