using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace Box9.Leds.FcClient
{
    public class FcClientAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WsClientWrapper>()
                .As<IClientWrapper>()
                .WithParameter("serverAddress", new Uri("ws://localhost:7890"))
                .WithParameter("sendMaxBufferLength", 4096)
                .WithParameter("receiveMaxBufferLength", 4096);
        }
    }
}
