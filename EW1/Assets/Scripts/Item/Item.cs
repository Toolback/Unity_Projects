using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescription] // Draw in the UI the custom Item Detail fetched in the script ItemCodeDescriptionDrawer.cs
    [SerializeField]
    private int _itemCode;

    private SpriteRenderer spriteRenderer;

    public int ItemCode { get { return _itemCode; } set { _itemCode = value; } }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        // Check for an existing Item Code, if yes Init the item 
        if (ItemCode != 0)
        {
            Init(ItemCode);
        }
    }

    public void Init (int itemCodeParam)
    {
        if (itemCodeParam != 0)
        {
            ItemCode = itemCodeParam;

            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(ItemCode);
            // Check if item sprite equal item sprite stored in db 
            spriteRenderer.sprite = itemDetails.itemSprite;

            // If item type is reapable then add nudgeable component (wooble effect)
            if (itemDetails.itemType == ItemType.Reapable_scenary)
            {
                gameObject.AddComponent<ItemNudge>();
            }
        }
    }
}

