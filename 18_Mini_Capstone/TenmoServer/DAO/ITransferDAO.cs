using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        public List<Transfer> SentTransfers(); //int accountFrom

        public List<Transfer> ReceivedTransfers(); //int accountTo

        public Transfer TransferDetails(int transferId);

        bool AddTransfer(Transfer transfer);

    }
}
