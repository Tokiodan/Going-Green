using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    private GameObject _structureOnTile;
    private ResourceManager resourceManager;

    public void Init(bool isOffset) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        resourceManager = GameObject.FindAnyObjectByType<ResourceManager>();
    }

    public bool CanPlaceStructure() {
        return _structureOnTile == null;
    }

    public void PlaceStructure(GameObject structure) {
        if (CanPlaceStructure()) {
            bool canplace = true;
            if(structure.tag == "Food" && resourceManager.totalMoney >= 10)
            {
                resourceManager.totalMoney -= 10;
            }else if (structure.tag == "House" && resourceManager.totalFood >= 10)
            {
                resourceManager.totalFood -= 10;
            }
            else if (structure.tag == "Work" && resourceManager.totalPopulation >= 5)
            {
                resourceManager.totalPopulation -= 5;
            }
            else if (structure.tag == "Mill" && resourceManager.totalPopulation >= 2 && resourceManager.totalFood >= 5)
            {
                resourceManager.totalFood -= 5;
                resourceManager.totalPopulation -= 2;
            }
            else
            {
                canplace = false;
                Destroy(structure);
                Debug.Log("You don't have the resources to build this structure");
                return;
            }
            if (canplace)
            {
                _structureOnTile = Instantiate(structure, transform.position, Quaternion.identity);
                structure.GetComponent<Building>().AddToList();
                resourceManager.UpdateResources();
            }
        }
    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }
}
