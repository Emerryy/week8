using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int TransferTypeId { get; set; }
        public int TransferStatusId { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal DollarAmount { get; set; }

        public override string ToString()
        {
            return ($"Transfer ID: {TransferId} - Transfer Type ID: {TransferTypeId} - Account From: {AccountFrom} - Account To: {AccountTo} - Dollar Amount: ${DollarAmount}");
        }

    }
}
