using UnityEngine;
using Photon.Pun;
using TMPro;

public class ItemWorld : MonoBehaviourPun
{
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
        photonView.RPC("RPCDestroyOnMaster", RpcTarget.MasterClient, gameObject.GetComponent<PhotonView>().ViewID);
    }

    [PunRPC]
    public void RPCDestroyOnMaster(int id)
    {
        //if (PhotonNetwork.IsMasterClient)
        //    PhotonNetwork.Destroy(PhotonView.Find(id));
        Destroy(PhotonView.Find(id));
    }
}
