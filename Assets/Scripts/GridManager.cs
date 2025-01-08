using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {
    [SerializeField] private int _width = 10; // Default grid width
    [SerializeField] private int _height = 10; // Default grid height
    [SerializeField] private Tile _tilePrefab; // Tile prefab reference
    [SerializeField] private Transform _cam; // Camera transform
    [SerializeField] private List<Structure> _structures = new List<Structure>(); // List of structures to manage
    [SerializeField] private Text populationText; // UI Text to show population (make sure to link in Inspector)

    private int currentPopulation = 0; // Tracks current population
    private int maxPopulation = 100;   // Maximum population before unlocking additional land
    private List<Vector2> unlockedTiles = new List<Vector2>(); // Keeps track of unlocked tiles
    private Dictionary<Vector2, Tile> _tiles;

    private void Start() {
        GenerateGrid();
        UpdatePopulationUI(); // Ensure population is updated at start
    }

    private void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                // Instantiate the grid tiles
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                // Initialize tile properties
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                // Store tile in dictionary
                _tiles[new Vector2(x, y)] = spawnedTile;

                // Lock all tiles initially (unlocked tiles will be populated later)
                unlockedTiles.Add(new Vector2(x, y));
            }
        }

        // Center the camera on the grid
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    /// <summary>
    /// Adds a structure to the grid.
    /// </summary>
    /// <param name="structure">The structure to place.</param>
    public void AddStructure(Structure structure) {
        if (!_structures.Contains(structure)) {
            _structures.Add(structure);
            Debug.Log($"Added structure: {structure.name}.");
        }
    }

    /// <summary>
    /// Removes a structure from the grid.
    /// </summary>
    /// <param name="structure">The structure to remove.</param>
    public void RemoveStructure(Structure structure) {
        if (_structures.Contains(structure)) {
            _structures.Remove(structure);
            Debug.Log($"Removed structure: {structure.name}.");
        }
    }

    /// <summary>
    /// Checks if a tile has a structure.
    /// </summary>
    /// <param name="position">Grid position to check.</param>
    /// <returns>True if a structure is present, otherwise false.</returns>
    public bool HasStructure(Vector2 position) {
        foreach (var structure in _structures) {
            if (Vector2.Distance(structure.transform.position, position) < 0.1f) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Increases the population and unlocks more land based on population.
    /// </summary>
    /// <param name="amount">Amount to increase population by.</param>
    public void IncreasePopulation(int amount) {
        currentPopulation += amount;
        UpdatePopulationUI();
        UnlockLand();
    }

    /// <summary>
    /// Unlocks additional land based on population.
    /// </summary>
    private void UnlockLand() {
        // Example logic for unlocking land (this can be customized)
        int unlockThreshold = 10; // For every 10 population, unlock 1 new tile
        int unlockCount = currentPopulation / unlockThreshold;

        // Unlock tiles up to the threshold
        for (int i = unlockedTiles.Count; i < unlockCount; i++) {
            // Unlock specific tiles (this can be customized based on the grid layout)
            Vector2 unlockedTile = new Vector2(i % _width, i / _width);
            unlockedTiles.Add(unlockedTile);

            // Optional: Visual update to show the unlocked tiles (e.g. change color)
            _tiles[unlockedTile].UnlockTile();
        }

        Debug.Log($"Current Population: {currentPopulation}, Land Unlocked: {unlockedTiles.Count}");
    }

    /// <summary>
    /// Checks if a tile is unlocked.
    /// </summary>
    /// <param name="position">Position of the tile to check.</param>
    /// <returns>True if the tile is unlocked, false otherwise.</returns>
    public bool IsTileUnlocked(Vector2 position) {
        return unlockedTiles.Contains(position);
    }

    /// <summary>
    /// Updates the UI text to show the current population.
    /// </summary>
    private void UpdatePopulationUI() {
        if (populationText != null) {
            populationText.text = "Population: " + currentPopulation;
        }
    }
}
