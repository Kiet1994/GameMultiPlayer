using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private const int MaxPrefabCount = 50;
    [SerializeField]
    private int OriginalFood = 30;
    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += SpawnFoodStart;
    }

    private void SpawnFoodStart() // ban đầu spawn trước ra 30 cái
    {
        NetworkManager.Singleton.OnServerStarted -= SpawnFoodStart; // chạy 1 lần khi server khởi động
        NetworkObjectPool.Singleton.InitializePool(); //  Khởi tạo nhóm
        for (int i = 0; i < OriginalFood; ++i)
        {
            SpawnFood();
        }
        StartCoroutine(SpawnOverTime()); // 2s spawn food
    }

    private void SpawnFood()
    {
        NetworkObject obj = NetworkObjectPool.Singleton.GetNetworkObject(prefab,
        GetRandomPositionOnMap(), Quaternion.identity);
        obj.GetComponent<Food>().prefab = prefab;
        if(!obj.IsSpawned) obj.Spawn(true); //bật lên
    }

    private Vector3 GetRandomPositionOnMap()
    {
        return new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), 1);
    }

    private IEnumerator SpawnOverTime()
    {
        while (NetworkManager.Singleton.ConnectedClients.Count > 0)
        { 
            yield return new WaitForSeconds(2f);
            if (NetworkObjectPool.Singleton.GetCurrentPrefabCount(prefab) < MaxPrefabCount)
            SpawnFood();
        }
    }
}
