using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PunchClockWS.WorkerService
{
    public class WorkerImpl
    {
        public string IsWorking()
        {
            DataBase db = new DataBase();
            Record rec = db.GetLastTimeRecord(1);
            return rec.ToString();
        }

        public string PunchCard(int employeeId)
        {
            try
            {
                DataBase db = new DataBase();
                DateTime now = DateTime.Now;
                Record lastRecord = db.GetLastTimeRecord(employeeId);
                string recordType = DetermineRecordType(lastRecord, now);
                if (string.IsNullOrEmpty(recordType))
                {
                    return "No se registro ningun otro evento porqye ya esta marcado correctamente";
                }
                db.InsertTimeRecord(employeeId, recordType, now);
                return recordType;
            } catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public string PunchCard(int employeeId, DateTime timeStamp)
        {
            try
            {
                DataBase db = new DataBase();
                DateTime now = timeStamp;
                Record lastRecord = db.GetLastTimeRecord(employeeId);
                string recordType = DetermineRecordType(lastRecord, now);
                Console.WriteLine($"El tipo de record:  {recordType}");
                if (string.IsNullOrEmpty(recordType))
                {
                    return "No se registro ningun otro evento porqye ya esta marcado correctamente";
                }
                db.InsertTimeRecord(employeeId, recordType, now);
                return recordType;
            } catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public List<Worker> GetAllEmployees()
        {
            DataBase db = new DataBase();
            List<Worker> employees = db.GetAllEmployees();
            return employees;
        }


        private string DetermineRecordType(Record lastRecord, DateTime timeStamp)
        {
            TimeSpan startWork = new TimeSpan(7, 0, 0);
            TimeSpan lunchStart = new TimeSpan(11, 0, 0);
            TimeSpan lunchEnd = new TimeSpan(12, 0, 0);
            TimeSpan endWork = new TimeSpan(15, 0, 0);
            TimeSpan now = timeStamp.TimeOfDay;
            string lastType;
            if (lastRecord == null)
            {
                lastType = "";
            } else
            {
             lastType = lastRecord.Type;
            }

            if (now >= startWork && now < lunchStart)
            {
                return lastType == "Entrada" ? null : "Entrada";
            }
            else if (now >= lunchStart && now <= lunchEnd)
            {
                return (lastType == "Inicio comida") ? "Fin comida" : lastType == "Fin comida" ? "" : "Inicio comida";
            }
            else if (now > lunchEnd && now <= endWork)
            {
                return lastType == "Salida" ? null : "Salida";
            }
            return "";
        }
    }

}