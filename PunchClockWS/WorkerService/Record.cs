using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PunchClockWS.WorkerService
{
    public class Record
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public string Type { set; get; }
        public DateTime TimeStamp { set; get; }
        public Record(int employeeId, string type, DateTime timestamp) 
        {
            EmployeeId = employeeId;
            Type = type;
            TimeStamp = timestamp;
        }

        public override string ToString()
        {
            return $"EmployeeId: {EmployeeId}, RecordType: {Type}, Timestamp: {TimeStamp}";
        }
    }
}