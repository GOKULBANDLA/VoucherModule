using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherModule.Models;

namespace VoucherModule.Helper
{
   static  class FileReaderHelper
    {
        public static List<Transactions> ReadTransactionDetailsFromCSV(string txnFilePath)
        {
            return File.ReadAllLines(txnFilePath)
                                            .Skip(1)
                                            .Select(line => Transactions.FromCsv(line))
                                            .ToList<Transactions>();
        }

        public static List<Vouchers> ReadVoucherDetailsFromCSV(string voucherFilePath)
        {
            return File.ReadAllLines(voucherFilePath)
                                            .Skip(1)
                                            .Select(line => Vouchers.FromCsv(line))
                                            .ToList<Vouchers>();
        }

        public static void VerifyFilesAvailabilityAndExtension(string txnFilePath, string voucherFilePath)
        {
            FileInfo fi;
            if (!File.Exists(txnFilePath))
            {
                Console.WriteLine("Transaction Details file is not available");
            }
            else
            {
                fi = new FileInfo(txnFilePath);
                if (fi.Extension != ".csv")
                {
                    Console.WriteLine("Transaction Details file Format should be CSV");
                }
            }
            if (!File.Exists(voucherFilePath))
            {
                Console.WriteLine("Voucher Details file is not available");
            }
            else
            {
                fi = new FileInfo(voucherFilePath);
                if (fi.Extension != ".csv")
                {
                    Console.WriteLine("Voucher Details file Format should be CSV");
                }
            }


        }
    }
}
