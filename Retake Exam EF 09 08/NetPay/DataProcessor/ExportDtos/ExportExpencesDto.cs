using NetPay.Data.Models;
using System.Xml.Serialization;

namespace NetPay.DataProcessor.ExportDtos
{
    [XmlType(nameof(Expense))]
    public class ExportExpencesDto
    {
        [XmlElement("ExpenseName")]
        public string ExpenseName { get; set; } = null!;

        [XmlElement("Amount")]
        public decimal Amount { get; set; }

        [XmlElement("PaymentDate")]
        public string PaymentDate { get; set; } = null!;

        [XmlElement("ServiceName")]
        public string ServiceName { get; set; } = null!;
    }

    //<Expenses>
    //  <Expense>
    //    <ExpenseName>Water Home</ExpenseName>
    //    <Amount>50.50</Amount>
    //    <PaymentDate>2024-08-20</PaymentDate>
    //    <ServiceName>Water</ServiceName>
}
