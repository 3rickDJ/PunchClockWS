using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PunchClockWS.WorkerService
{

    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Worker(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"%Employee{{}} Id: ${Id}, Name: ${Name}";
        }
    }
}