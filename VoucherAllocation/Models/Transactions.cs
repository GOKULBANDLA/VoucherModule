using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherModule.Models
{
    public class Transactions
    {
        public int TransactionId { get; set; }
        public int Amount { get; set; }
        public DateTime CreateDate { get; set; }

        public static Transactions FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Transactions transaction = new();
            transaction.TransactionId = Convert.ToInt32(values[0]);
            transaction.Amount = Convert.ToInt32(values[1]);
            transaction.CreateDate = Convert.ToDateTime(values[2]);
            return transaction;
        }
    }
}
