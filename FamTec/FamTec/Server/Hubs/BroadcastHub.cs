using Microsoft.AspNetCore.SignalR;

namespace FamTec.Server.Hubs
{
    public class BroadcastHub : Hub
    {
        /// <summary>
        /// SIGNALR JOIN GROUP
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public async Task JoinRoomAsync(string userid, string roomName)
        {
            /*
                DB 검증로직
                    - DB의 USERID의 실제권한.
                    - (이 사람이 Role이 VOC 알람을 받는 사람이 맞는지.
             */
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} {roomName} Join Success");
        }

        /// <summary>
        /// SIGNALR REMOVE GROUP
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public async Task RemoveRoomAsync(string userid, string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} {roomName} Remove Success");
        }

        /// <summary>
        /// GROUP MESSAGE
        /// </summary>
        /// <param name="message"></param>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(string message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", $"{message}");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("AlarmSelect", message);
        }


    }
}
