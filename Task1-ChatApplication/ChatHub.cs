using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Task1_ChatApplication
{
    public class ChatHub : Hub
    {
        public static List<string> _users = new List<string>();
        public static List<Messages> _chatList = new List<Messages>();
        public void Send(string name, string message)
        {
            _chatList.Add(new Messages()
            {
                UserName = name,
                Message = message
            });
            BroadCastMessages();
        }


        public void LogIn(string userName)
        {
            var loginMessage = string.Empty;

            if (_users.Count(e => e == userName) <= 0)
            {
                if (_users.Count() < 20)
                {
                    _users.Add(userName);
                    loginMessage = userName;
                }
                else
                {
                    loginMessage = "User Limit reached.";
                }
            }
            LogInSuccess(loginMessage);
            ShowUsersOnLine();
            BroadCastMessages();
        }

        private void LogInSuccess(string userName)
        {
            Clients.Caller.logInSuccess(userName);
        }
        public override Task OnConnected()
        {
            ShowUsersOnLine();
            return base.OnConnected();
        }

        public void Logout(string userName)
        {

            if (_users.Count(e => e == userName) > 0)
            {
                _users.Remove(userName);
            }
            LogoutSuccess();
            ShowUsersOnLine();
        }

        public void LogoutSuccess()
        {
            Clients.Caller.logoutSuccess();
        }

        public void ShowUsersOnLine()
        {
            Clients.All.showUsersOnLine(_users);
        }

        private void BroadCastMessages()
        {
            Clients.All.broadcastMessage(_chatList.Count() > 0 ? _chatList.Take(15) : _chatList);
        }

        public void CheckUserTyping(string userName)
        {
            Clients.All.checkUserTyping(userName + " is typing...");
        }
    }
}