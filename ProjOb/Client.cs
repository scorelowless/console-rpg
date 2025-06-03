using System.Net.Sockets;

namespace ProjOb;

public class Client
{
    private volatile bool _isAlive = true;
    public async Task Start(string address, int port)
    {
        try
        {
            using TcpClient client = new TcpClient();
            await client.ConnectAsync(address, port);
            await using NetworkStream stream = client.GetStream();
            ModelTransfer? modelTransfer = await NetworkMethods.ReceiveDataAsync<ModelTransfer>(stream);
            if (modelTransfer == null) return;
            Model model = modelTransfer.Model;
            int index = modelTransfer.Index;
            Player player = model.Players[index];
            Display display = Display.GetInstance(model.Map, player, model.Instructions);
            IKeyControl keyControl =
                MapBuilderDirector.GenerateBasicMap(new KeyControlBuilder(index, display)) as IKeyControl ??
                throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
            display.Log("You can start playing now!");

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    // ReSharper disable once AccessToDisposedClosure
                    IActionType? message = await NetworkMethods.ReceiveDataAsync<IActionType>(stream);
                    if (message == null) break;
                    var result = message.Execute(model);
                    var enemyresult = model.Map.UpdateEnemies(player);
                    if (player.IsDead)
                    {
                        _isAlive = false;
                        break;
                    }
                    player.UpdateNearbyEnemy();
                    if (result.WasSuccessful || enemyresult.Count > 0)
                    {
                        display.Update();
                    }

                    bool wasPlayerAttacked = false;
                    foreach (IResultType resultType in enemyresult)
                    {
                        if(resultType.WasAttack && ((ResultType.Attack)resultType).Target == player)
                        {
                            if (player.IsDead)
                            {
                                _isAlive = false;
                                break;
                            }
                            display.Log(resultType.Message);
                            wasPlayerAttacked = true;
                            break;
                        }
                    }
                    if (!wasPlayerAttacked && message.PlayerIndex == index)
                    {
                        display.Log(result.Message);
                    }
                    
                }
            });

            while (_isAlive)
            {
                if (display.IsKeyAvailable())
                {
                    var key = display.ReadKey();
                    IActionType resultAction = keyControl.Check(key);
                    await NetworkMethods.SendDataAsync(stream, resultAction);
                }
                else
                {
                    await Task.Delay(50);
                }
            }

            display.GameOver();
        }
        catch (IOException)
        {
            Display d = Display.GetInstance();
            d.WriteError("Server closed unexpectedly");
        }
        catch (SocketException)
        {
            Display d = Display.GetInstance();
            d.WriteError("Server is probably down");
        }
        catch (Exception e)
        {
            Display d = Display.GetInstance();
            d.WriteError($"Unexpected exception:\n{e}");
        }
    }
}