using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemData.Instance.itemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 8f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 40f, ForceMode2D.Impulse);
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    //private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        //textMeshPro.SetText("");
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
