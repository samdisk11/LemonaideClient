function onPlayerAdded(a)end pcall(function()game:SetPlaceID(-1,false)end)local h=tick()local e=false local c=false local i=false local f=true local b=0 local a=0 local a=0 settings()['Game Options'].CollisionSoundEnabled=true pcall(function()settings().Rendering.EnableFRM=true end)pcall(function()settings().Physics.Is30FpsThrottleEnabled=false end)pcall(function()settings()['Task Scheduler'].PriorityMethod=Enum.PriorityMethod.AccumulatedError end)pcall(function()settings().Physics.PhysicsEnvironmentalThrottle=Enum.EnviromentalPhysicsThrottle.DefaultAuto end)local g=...if g==nil then g=15 end local d=true local a=game.Close:connect(function()if 0 then if not e then local a=tick()-h elseif(not c)or(not i)then local a=tick()-h if not c then c=true end if not i then i=true end elseif not f then f=true end end end)game:GetService('ChangeHistoryService'):SetEnabled(false)game:GetService('ContentProvider'):SetThreadPool(16)pcall(function()game:SetCreatorID(0,Enum.CreatorType.User)end)pcall(function()game:GetService('Players'):SetChatStyle(Enum.ChatStyle.ClassicAndBubble)end)local c=false pcall(function()if settings().Network.MtuOverride==0 then settings().Network.MtuOverride=1400 end end)client=game:GetService('NetworkClient')visit=game:GetService('Visit')function setMessage(a)if not false then game:SetMessage(a)else game:SetMessage('Teleporting ...')end end function showErrorWindow(b,a,a)game:SetMessage(b)end function onDisconnection(a,a)if a then showErrorWindow('You have lost connection','LostConnection','LostConnection')else showErrorWindow('This game has been shutdown','Kick','Kick')end end function requestCharacter(a)local d d=player.Changed:connect(function(a)if a=='Character'then game:ClearMessage()c=false d:disconnect()if 0 then if not i then local a=tick()-h i=true b=tick()f=false end end end end)setMessage('Requesting character')local a,a=pcall(function()a:RequestCharacter()setMessage('waiting for scrub')c=true end)end function onConnectionAccepted(a,b)e=true local c=true local b,a=pcall(function()if not d then visit:SetPing('',300)end if not false then game:SetMessageBrickCount()else setMessage('Teleporting ...')end b.Disconnection:connect(onDisconnection)local a=b:SendMarker()a.Received:connect(function()c=false requestCharacter(b)end)end)if not b then return end while c do workspace:ZoomToExtents()wait(0.5)end end function onConnectionFailed(a,a)showErrorWindow('Failed to connect to the Game. (ID='..a..')','ID'..a,'Other')end function onConnectionRejected()connectionFailed:disconnect()showErrorWindow('This game is not available. Please try another','WrongVersion','WrongVersion')end pcall(function()settings().Diagnostics:LegacyScriptMode()end)local a,a=pcall(function()game:SetRemoteBuildMode(true)setMessage('Connecting to Server')client.ConnectionAccepted:connect(onConnectionAccepted)client.ConnectionRejected:connect(onConnectionRejected)connectionFailed=client.ConnectionFailed:connect(onConnectionFailed)client.Ticket=''playerConnectSucces,player=pcall(function()return client:PlayerConnect(0,'localhost',53640,0,g)end)player:SetSuperSafeChat(false)pcall(function()player:SetUnder13(false)end)pcall(function()player:SetMembershipType(Enum.MembershipType.BuildersClub)end)pcall(function()player:SetAccountAge(365)end)player.Idled:connect(onPlayerIdled)onPlayerAdded(player)if not d then visit:SetUploadUrl('')end end)