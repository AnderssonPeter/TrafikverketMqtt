using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;

namespace TrafikverketMQTT
{
    public class MqttSettings
    {
        public string HostAddress
        { get; set; }

        public int? Port
        { get; set; }

        public string ClientId
        { get; set; }

        public string UserName
        { get; set; }

        public string Password
        { get; set; }

        public string BaseTopic
        { get; set; }

        public MqttQualityOfService? QualityOfService
        { get; set; }

        public bool? RetainLastMessageOnServer
        { get; set; }
    }
}
