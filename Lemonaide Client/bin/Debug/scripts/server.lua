game:GetService("RunService"):Run()game:GetService("NetworkServer"):Start(Port,20)function onJoined(a)a:LoadCharacter()while true do wait(0.001)if a.Character.Humanoid.Health==0 then wait(5)a:LoadCharacter()elseif a.Character.Parent==nil then wait(5)a:LoadCharacter()end end end game.Players.PlayerAdded:connect(onJoined)