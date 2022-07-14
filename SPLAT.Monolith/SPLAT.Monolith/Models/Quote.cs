using SPLAT.Monolith.Common.Enums;

namespace SPLAT.Monolith.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public int ExpectedDistance { get; set; }
        public DistanceUnit ExpectedDistanceUnit { get; set; }
    }
}
