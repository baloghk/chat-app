using Microsoft.AspNetCore.SignalR;

namespace ChatAppAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChatRoom()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "ChatRoom");
        }

        public async Task LeaveChatRoom()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "ChatRoom");
        }
    }
}
