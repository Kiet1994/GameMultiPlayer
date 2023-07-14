using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartNetwork : MonoBehaviour
{
    [SerializeField] private Button Host;
    [SerializeField] private Button Server;
    [SerializeField] private Button Client;

    private void OnEnable()
    {
        Host.onClick.AddListener(StartHost);
        Server.onClick.AddListener(StartServer);
        Client.onClick.AddListener(StartClient);
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
}
