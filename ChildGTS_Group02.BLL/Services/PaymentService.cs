using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
    public class PaymentService
    {
        private PaymentRepository _paymentRepository = new();

        public List<Payment> GetAllPayments()
        {
            return _paymentRepository.GetAllPayments();
        }
        public Payment? GetPaymentById(int paymentId)
        {
            return _paymentRepository.GetPaymentById(paymentId);
        }
        public int GetNextPaymentID()
        {
            return _paymentRepository.GetNextPaymentID();
        }

        public bool AddPayment(Payment payment)
        {
            return _paymentRepository.AddPayment(payment);
        }
    }
}
