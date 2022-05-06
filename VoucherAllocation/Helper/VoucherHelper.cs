using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherModule.Models;

namespace VoucherModule.Helper
{
    static class VoucherHelper
    {
        public static void PrepareVoucherAllocation(List<Transactions> transactions, List<Vouchers> vouchers)
        {
            List<List<int>> subset = new List<List<int>>();
            var voucherAmountArray = vouchers.Select(x => x.Amount).ToList();
            List<VoucherAllocation> mappedVouchers = new List<VoucherAllocation>();
            findSubsets(subset, voucherAmountArray, new List<int>(), 0);

            foreach (var item in transactions)
            {
                List<List<int>> list = GetApplicableVouchers(subset, item);
                list = list.Distinct(ListEqualityComparer<int>.Default).ToList();

                mappedVouchers.AddRange(GetMappedVouchers(transactions, vouchers, list));

            }
            if (mappedVouchers.Any())
            {
                Console.WriteLine($"TransactionId\tTransactionAmount\tVoucherCode\t VoucherAmount");
                foreach (var item in mappedVouchers)
                {
                    Console.WriteLine($"{item.TransactionId}\t\t{item.VoucherAmount}\t\t\t{item.VoucherCode}\t\t{item.VoucherAmount}");
                }
            }
            else
            {
                Console.WriteLine("No data available for voucher allocation");
            }


        }

        private static List<VoucherAllocation> GetMappedVouchers(List<Transactions> transactions, List<Vouchers> vouchers, List<List<int>> list)
        {
            List<VoucherAllocation> allocation = new List<VoucherAllocation>();
            foreach (var item in transactions)
            {

                var requiredVouchers = list.Where(x => x.Sum() == item.Amount);
                foreach (var voucher in requiredVouchers)
                {
                    List<Vouchers> required = new List<Vouchers>();
                    foreach (var voucherValue in voucher)
                    {
                        required.Add(vouchers.Find(x => x.Amount == voucherValue && !x.Used));
                    }
                    if (required.Any() && !required.Any(x => x == null))
                    {
                        required = required.Where(x => x.Expiry > item.CreateDate).ToList();
                        required.ForEach(x => x.Used = !x.Used);
                        allocation.AddRange(required.Select(x => new VoucherAllocation { VoucherCode = x.VoucherCode, TransactionAmount = item.Amount, TransactionId = item.TransactionId, VoucherAmount = x.Amount }));
                    }


                }
            }
            return allocation;


        }



        private static List<List<int>> GetApplicableVouchers(List<List<int>> combinations, Transactions item)
        {
            List<List<int>> list = new();

            foreach (var amountCombo in combinations)
            {
                if (amountCombo.Sum() == item.Amount)
                {

                    list.Add(amountCombo);

                }
            }
            return list;
        }

        public static void findSubsets(List<List<int>> subset, List<int> nums, List<int> output, int index)
        {
            if (index == nums.Count)
            {
                subset.Add(output);
                return;
            }

            findSubsets(subset, nums, new List<int>(output), index + 1);

            output.Add(nums[index]);
            findSubsets(subset, nums, new List<int>(output), index + 1);
        }
    }
}
