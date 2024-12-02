using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PunchClockWS.WorkerService;

namespace PunchClockWS
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string PunchCard(int id)
        {
            WorkerImpl wsi = new WorkerImpl();
            string msg = wsi.PunchCard(id);
            return msg;
        }

        [WebMethod]
        public string PunchCard2(int id, DateTime timeStamp)
        {
            WorkerImpl wsi = new WorkerImpl();
            string msg = wsi.PunchCard(id, timeStamp);
            return msg;
        }

        [WebMethod]
        public List<Worker> ListEmployees()
        {
            WorkerImpl wsi = new WorkerImpl();
            return wsi.GetAllEmployees();
        }


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
