using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class JoinedTransfer
    {
        public int TransferId { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"Transfer ID: {TransferId} - From: {FromUser} - To: {ToUser} - Type: {Type} Status: {Status} - Aamount: ${Amount} ";
        }

    }
}
