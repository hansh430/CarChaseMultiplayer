using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSBR
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance => _instance;
        private static Manager _instance;
        public NetworkManager networkManager;
        public LobbyUimanager lobbyUimanager;
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
