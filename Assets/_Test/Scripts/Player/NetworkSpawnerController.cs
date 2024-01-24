using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TPSBR
{
    public class NetworkSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
    {
        [SerializeField] private NetworkPrefabRef playerPrefab;
        private NetworkManager networkManager;
        [SerializeField] private TMP_Text roomCodeText;
        [SerializeField, Range(0f, 5f)]
        private float randomSpawnPositions = 5f;
        private Dictionary<PlayerRef, NetworkObject> Players = new Dictionary<PlayerRef, NetworkObject>();

        private void Start()
        {
            networkManager = FindAnyObjectByType<NetworkManager>();
        }
       
        public void PlayerJoined(PlayerRef player)
        {
            roomCodeText.text = "Room Code: " + networkManager.roomCode;
            if (Runner.IsServer)
            {
                SpawnPlayer(player);
            }
        }
        public void SpawnPlayer(PlayerRef player)
        {
            var randomPos = new Vector3(Random.Range(-randomSpawnPositions,randomSpawnPositions),2f, Random.Range(-randomSpawnPositions,randomSpawnPositions));
            Debug.Log(randomPos);
            var playerNetworkObj = Runner.Spawn(playerPrefab, randomPos, Quaternion.identity, player);
            Players.Add(player, playerNetworkObj);
        }

        public void PlayerLeft(PlayerRef player)
        {

        }
    }
}
