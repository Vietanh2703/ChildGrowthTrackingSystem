using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class PaymentRepository
    {
        private ChildGrowthTrackingSystemDBContext _context = new();

        public List<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public Payment? GetPaymentById(int paymentId)
        {
            return _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
        }

        public int GetNextPaymentID()
        {
            return _context.Payments.Any() ? _context.Payments.Max(p => p.PaymentId) + 1 : 1;
        }

        public bool AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            return _context.SaveChanges() > 0;
        }
    }
}
