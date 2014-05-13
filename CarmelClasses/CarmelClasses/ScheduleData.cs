using System;
namespace CarmelClasses
{
    public class ScheduleData
    {
        public int ScheduleId { get; set; }
        public DayOfWeek Day{ get; set; }
        public int ClassHour { get; set; }
        public int StartingHour { get; set; }
        public int StartingMinutes { get; set; }
        public int Duration { get; set; }
    }
}
