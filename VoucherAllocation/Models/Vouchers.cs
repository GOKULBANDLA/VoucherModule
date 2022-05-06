using System;

namespace VoucherModule.Models
{
    public class Vouchers
    {
        public string VoucherCode { get; set; }
        public int Amount { get; set; }
        public DateTime Expiry { get; set; }

        public bool Used { get; set; }

        public static Vouchers FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Vouchers voucher = new();
            voucher.VoucherCode = values[0];
            voucher.Amount = Convert.ToInt32(values[1]);
            voucher.Expiry = Convert.ToDateTime(values[2]);
            voucher.Used = false;
            return voucher;
        }
    }

    public class VoucherAllocation
    {
        public string VoucherCode { get; set; }
        public int VoucherAmount { get; set; }
        public int TransactionId { get; set; }
        public int TransactionAmount { get; set; }
    }
}
