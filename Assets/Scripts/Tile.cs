using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor, _unlockedColor; // Add unlocked color
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private bool _isUnlocked = false; // Track if tile is unlocked

    public void Init(bool isOffset) {
        // Set the base or offset color based on the isOffset parameter
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    public void UnlockTile() {
        if (!_isUnlocked) {
            _isUnlocked = true;
            _renderer.color = _unlockedColor; // Change the color to indicate the tile is unlocked
            Debug.Log("Tile Unlocked: " + gameObject.name);
        }
    }

    public bool IsUnlocked() {
        return _isUnlocked;
    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }
}
