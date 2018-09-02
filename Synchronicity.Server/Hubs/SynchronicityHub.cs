using Autofac;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Synchronicity.Common.Model;
using Synchronicity.Server.Interfaces.Data;
using System;

namespace Synchronicity.Server.Hubs
{
    [HubName("SynchronicityHub")]
    public sealed class SynchronicityHub : Hub
    {
        readonly IHistoryRepository historyRepository;

        public SynchronicityHub()
        {
            historyRepository = GlobalHost.DependencyResolver.Resolve<IHistoryRepository>();
        }

        public void Connect(int virtualMachineId, string userName, string description)
        {
            historyRepository.Put(new History
            {
                VirtualMachineId = virtualMachineId,
                CreationUser = userName,
                Description = description,
                CreationDate = DateTime.Now
            });

            Clients.All.refresh();
        }

        public void Disconnect(int Id)
        {
            historyRepository.Put(new History
            {
                Id = Id,
                LogOffDate = DateTime.Now
            });

            Clients.All.refresh();
        }

        public void Refresh()
        {
            Clients.All.refresh();
        }
    }
}