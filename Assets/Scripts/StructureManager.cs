using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour {
    public static StructureManager Instance { get; private set; }

    [SerializeField] private List<Structure> _structures = new List<Structure>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void AddStructure(Structure structure) {
        if (!_structures.Contains(structure)) {
            _structures.Add(structure);
            Debug.Log($"Structure {structure.name} added to StructureManager.");
        }
    }

    public void RemoveStructure(Structure structure) {
        if (_structures.Contains(structure)) {
            _structures.Remove(structure);
            Debug.Log($"Structure {structure.name} removed from StructureManager.");
        }
    }

    public List<Structure> GetAllStructures() {
        return _structures;
    }

    public void ClearStructures() {
        _structures.Clear();
        Debug.Log("All structures cleared from StructureManager.");
    }
}
