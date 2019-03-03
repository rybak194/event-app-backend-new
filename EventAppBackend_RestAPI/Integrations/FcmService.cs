using FirebaseNet.Messaging;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace EventAppBackend_RestAPI.Integrations
{
    public class FcmService
    {
        private readonly IConfiguration config;
        private readonly FCMClient fcmClient;

        public FcmService(IConfiguration config)
        {
            this.config = config;
            var apiKey = config.GetValue<string>("FCMKey");
            this.fcmClient = new FCMClient(apiKey);
        }

        public void SendPushNotification(string title, string body)
        {            
            var notifications = new INotification[]
            {
                GetIosNotification(title, body)
            };

            var messages = notifications.Select(GetMessage).ToList();

            foreach (var message in messages)
            {
                dynamic resposne = fcmClient.SendMessageAsync(message).Result;
                var error = resposne?.Error;
                if (error != null)
                {
                    throw new Exception($"FCM Error: {error}");
                }
            }
        }

        private Message GetMessage(INotification notification)
        {
            return new Message()
            {
                To = config.GetValue<string>("FCMTopic"),
                Notification = notification
            };
        }

        private AndroidNotification GetAndroidNotification(string title, string body)
        {
            return new AndroidNotification()
            {
                Title = title,
                Body = body,
            };
        }

        private IOSNotification GetIosNotification(string title, string body)
        {
            return new IOSNotification()
            {
                Title = title,
                Body = body,
            };
        }
    }
}
