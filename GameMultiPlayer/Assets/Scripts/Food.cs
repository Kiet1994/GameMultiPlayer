using Unity.Netcode;
using UnityEngine;


public class Food : NetworkBehaviour
{
    public GameObject prefab;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        Debug.Log(!NetworkManager.Singleton.IsServer);
        if(!NetworkManager.Singleton.IsServer) return;// nếu không phải sv thì dừng.

        if(col.TryGetComponent(out PlayerLength playerLength)) // daa
        {
            playerLength.AddLength();
        }
        else if (col.TryGetComponent(out Tail tail))
        {
            tail.networkedOwner.GetComponent<PlayerLength>().AddLength();
        }
        //NetworkObjectPool.Singleton.ReturnNetworkObject(NetworkObject, prefab);

        if (NetworkObject.IsSpawned) 
        NetworkObject.Despawn(); // ẩn đi
    }
}
