namespace DroneDeliveryService.Models
{
    public class Solution
    {
        public int StartIndex;
        public int EndIndex;
        public decimal Sum;
        public int Length
        {
            get { return EndIndex - StartIndex + 1; }
        }
    }
}
