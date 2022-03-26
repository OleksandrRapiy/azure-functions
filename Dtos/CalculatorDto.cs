using System;

namespace CalculatorFunc.Dtos
{
    public class CalculatorDto
    {
        public CalculatorDto()
        {

        }

        public CalculatorDto(int sum, string date, string ip)
        {
            Sum = sum;
            Date = date;
            Ip = ip;
        }

        public int Sum { get; set; }
        public string Date { get; set; }
        public string Ip { get; set; }
    }
}
