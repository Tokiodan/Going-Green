// File: CornFarm.cs
using UnityEngine;

public class CornFarm : MonoBehaviour {
    [SerializeField] private float foodGenerationInterval = 30f; // Time in seconds
    [SerializeField] private int foodPerInterval = 1;

    private void Start() {
        InvokeRepeating(nameof(GenerateFood), foodGenerationInterval, foodGenerationInterval);
    }

    private void GenerateFood() {
        if (ResourceManager.Instance == null) {
            Debug.LogError("ResourceManager.Instance is null! Make sure ResourceManager is in the scene.");
            return;
        }

        ResourceManager.Instance.AddFood(foodPerInterval);
        Debug.Log($"CornFarm generated {foodPerInterval} food.");
    }
}
