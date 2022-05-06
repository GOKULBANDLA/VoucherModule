using VoucherModule.Helper;
using VoucherModule.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VoucherModule
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Transactions> transactions;
            List<Vouchers> vouchers;
            Console.WriteLine("Enter File path for Transaction details");
            string txnFilePath = Console.ReadLine();

            Console.WriteLine("Enter File path for Voucher details");
            string voucherFilePath = Console.ReadLine();
            FileReaderHelper.VerifyFilesAvailabilityAndExtension(txnFilePath, voucherFilePath);
            transactions = FileReaderHelper.ReadTransactionDetailsFromCSV(txnFilePath);
            vouchers = FileReaderHelper.ReadVoucherDetailsFromCSV(voucherFilePath);
            VoucherHelper.PrepareVoucherAllocation(transactions, vouchers);
            Console.WriteLine("Press Key to Exit");
            Console.ReadKey();
        }

    }

}
