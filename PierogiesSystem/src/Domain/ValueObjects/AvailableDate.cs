namespace CleanArchitecture.Domain.ValueObjects
{
    using System;
    using Common;

    public class AvailableDate
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public AvailableDate() { }

        public AvailableDate(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
        
    }
}