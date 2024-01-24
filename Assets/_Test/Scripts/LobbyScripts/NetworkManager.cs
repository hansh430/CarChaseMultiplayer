using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public string roomCode { get; private set; }
    private NetworkRunner networkRunner;
    [SerializeField] private NetworkRunner networkRunnerPrefab;
    private readonly int gameSceneIndex = 1;
    public void OnConnectedToServer(NetworkRunner runner)
    {
       
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public async Task StartGame(GameMode mode, string sessionCode, CancellationToken cancellationToken)
    {
        try
        {
            roomCode = sessionCode;
            if (networkRunner == null)
            {
                networkRunner = Instantiate(networkRunnerPrefab, transform);
                networkRunner.AddCallbacks(this);
                networkRunner.ProvideInput = true;
            }
            var tryingHost = mode == GameMode.Host;
            Debug.Log(tryingHost ? $"Trying to Host {roomCode}" : $"Trying to Join {roomCode}");
            var result = await networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = roomCode,
                Scene = gameSceneIndex,
                SceneManager = networkRunner.GetComponent<NetworkSceneManagerDefault>()
            });

            if (result.Ok)
            {
                Debug.Log(tryingHost ? "Game started sucessfully" : "Game joined sucessfully");
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

                // Unload the current scene asynchronously
                SceneManager.UnloadSceneAsync(currentSceneIndex);
            }
            else
            {
                Debug.LogError("Error in staring the game");

            }
        }
        catch (OperationCanceledException)
        {
            print("Start game operation was cancelled");
        }
        catch (Exception e)
        {
            print("An error occurred during start game " + e.Message);
        }


    }
    private void OnStartCancelled()
    {
        if (networkRunner)
        {
            networkRunner.Shutdown();
            networkRunner = null;
        }
    }

}
