using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSBR
{
    public class LobbyUimanager : MonoBehaviour
    {
        public ConnectionPanelController connectionPanelController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
