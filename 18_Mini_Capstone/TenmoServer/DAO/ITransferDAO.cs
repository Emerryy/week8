using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        public List<Transfer> GetTransfers(); //int accountFrom

        public List<JoinedTransfer> GetTransfersByUserId(/*int userId*/);

        public Transfer TransferDetails(int transferId);

        bool AddTransfer(Transfer transfer);

    }
}
