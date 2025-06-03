using System.Net;
using System.Net.Sockets;
using ProjOb.ActionType;

namespace ProjOb;

public class Server
{
    private Model _model = null!;
    private int _playerCount;
    private const int MAX_PLAYERS = 9;
    private readonly NetworkStream?[] _streams = new NetworkStream?[MAX_PLAYERS];
    private readonly ServerDisplay _display = ServerDisplay.GetInstance();
    private readonly Mutex _modelMutex = new Mutex();
    
    public async Task Start(int port)
    {
        try
        {
            string instructions = MapBuilderDirector.GenerateBasicMap(new InstructionBuilder()) as string ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
            Map map = MapBuilderDirector.GenerateBasicMap(new MapBuilder()) as Map ??
                      throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
            _model = new Model([], map, instructions);
        
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            _display.WriteInfo($"Server started on port {port}");

            while (_playerCount < MAX_PLAYERS)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _display.WriteInfo($"Client {_playerCount + 1} connected: {client.Client.RemoteEndPoint}");
                _ = HandleClientAsync(client, _playerCount);
                _playerCount++;
            }
        }
        catch (Exception e)
        {
            _display.WriteError("Unexpected exception in listener Task:");
            _display.WriteError(e.ToString());
        }
    }
    
    private async Task HandleClientAsync(TcpClient client, int index)
    {
        try
        {
            await using NetworkStream stream = client.GetStream();
            _display.WriteInfo($"Sending model to client {index + 1}");
            _streams[index] = stream;
        
            Player player = new Player(_model.Map, index);
            lock (_modelMutex)
            {
                _model.Players.Add(player);
            }
            ModelTransfer modelTransfer = new ModelTransfer
            {
                Model = _model,
                Index = index
            };
        
            await NetworkMethods.SendDataAsync(stream, modelTransfer);
            _display.WriteInfo($"Sent model to client {index + 1}");
            IActionType newPlayer = new AddPlayer(index);
            for (int i = 0; i < _streams.Length; i++)
            {
                NetworkStream? networkStream = _streams[i];
                if (networkStream == null || i == index) continue;
                await NetworkMethods.SendDataAsync(networkStream, newPlayer);
            }

            while (true)
            {
                IActionType? message = await NetworkMethods.ReceiveDataAsync<IActionType>(stream);
                if (message == null) break;
                IResultType result;
                lock (_modelMutex)
                {
                    result = message.Execute(_model);
                    _model.Map.UpdateEnemies(player);
                    player.UpdateNearbyEnemy();
                }
                if (result.WasSuccessful)
                { 
                    _display.WriteSuccess($"{index + 1}: {result.Message}");
                    foreach (var networkStream in _streams)
                    {
                        if (networkStream == null) continue;
                        await NetworkMethods.SendDataAsync(networkStream, message);
                    }
                }
                else
                {
                    _display.WriteUnsuccess($"{index + 1}: {result.Message}");
                    await NetworkMethods.SendDataAsync(stream, message);
                }
            }
            _display.WriteInfo($"Connection with client {index + 1} terminated");
            stream.Close();
            client.Close();
            _streams[index] = null;
        }
        catch (IOException)
        {
            _display.WriteInfo($"Connection with client {index + 1} terminated");
            client.Close();
            _streams[index] = null;
        }
        catch (Exception e)
        {
            _display.WriteError("Unexpected exception in client handling Task:");
            _display.WriteError(e.ToString());
            client.Close();
            _streams[index] = null;
        }
    }
}