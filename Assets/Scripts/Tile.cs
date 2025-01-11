using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    private GameObject _structureOnTile;

    public void Init(bool isOffset) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    public bool CanPlaceStructure() {
        return _structureOnTile == null;
    }

    public void PlaceStructure(GameObject structure) {
        if (CanPlaceStructure()) {
            _structureOnTile = Instantiate(structure, transform.position, Quaternion.identity);
        }
    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }
}
