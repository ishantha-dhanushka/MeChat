using MeChatDTO;
using MeChatRepository.DbModels;
using MeChatRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeChatHub
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, Connecteduser> Users
        = new ConcurrentDictionary<string, Connecteduser>();

        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        //public void Connect(string userId)
        //{
        //    string connectionId = Context.ConnectionId;
        //    var user = Users.GetOrAdd(userId, _ => new Connecteduser
        //    {
        //        Id = userId,
        //        ConnectionIds = new HashSet<string>()
        //    });
        //    lock (user.ConnectionIds)
        //    {
        //        user.ConnectionIds.Add(connectionId);
        //    }
        //}

        //public void Dicconnect(string userId)
        //{
        //    string userName = Context.User.Identity.Name;
        //    string connectionId = Context.ConnectionId;

        //    Connecteduser user;
        //    Users.TryGetValue(userName, out user);

        //    if (user != null)
        //    {
        //        lock (user.ConnectionIds)
        //        {
        //            user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

        //            if (!user.ConnectionIds.Any())
        //            {
        //                Connecteduser removedUser;
        //                Users.TryRemove(userName, out removedUser);

        //                // You might want to only broadcast this info if this 
        //                // is the last connection of the user and the user actual is 
        //                // now disconnected from all connections.
        //                //Clients.Others.userDisconnected(userName);
        //            }
        //        }
        //    }
        //}


        //public void Send(string name, string message)
        //{
        //    //Clients.All.SendAsync("Send", message);
        //    //return Clients.User(user).SendAsync("ReceiveMessage", message);

        //    Connecteduser receiver;

        //    if (Users.TryGetValue(name, out receiver))
        //    {

        //        Connecteduser sender = GetUser(Context.User.Identity.Name);

        //        IEnumerable<string> allReceivers;
        //        lock (receiver.ConnectionIds)
        //        {
        //            lock (sender.ConnectionIds)
        //            {

        //                allReceivers = receiver.ConnectionIds.Concat(
        //                    sender.ConnectionIds);
        //            }
        //        }

        //        foreach (var cid in allReceivers)
        //        {
        //            Clients.Client(cid).SendAsync("Send", message);
        //        }
        //    }
        //}

        //private Connecteduser GetUser(string username)
        //{
        //    Connecteduser user;
        //    Users.TryGetValue(username, out user);

        //    return user;
        //}

        public void Send(MessageDTO messageObj)
        {

            Message message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                ChatId = messageObj.ChatId,
                CreatedDate = DateTime.Now,
                MessageText = messageObj.MessageText,
                UserId = messageObj.UserId
            };

            var m = this._messageRepository.Create(message);
            Clients.All.SendAsync("message", message);
        }

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            string userName = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
