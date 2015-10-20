using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataConnection;

namespace RealChart.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            try
            {
              
                return new string[] { "value1", "value2" };
            }
            catch (Exception e)
            {
                return new string[] { e.Message, e.InnerException.ToString() };
            }

        }

        // GET api/values/5
        public IEnumerable<string> Get(int id)
        {
            try
            {                
                string strSQL = "select Label,ValueData from RealChart where Id=" + id + "";
                string label = string.Empty, valuedata = string.Empty;
                //Open datareader using DBConn object
                DbConn rs1 = new DbConn(strSQL);
                //Fetch all record 
                while (rs1.ReadData.Read())
                {                   
                    label = rs1.ReadData["Label"].ToString();
                    valuedata = rs1.ReadData["ValueData"].ToString();
                }
                // close datareader 
                rs1.ReadData.Close();
                return new string[] { label, valuedata };
            }
            catch (Exception e)
            {
                return new string[] { e.Message, e.InnerException.ToString() };
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}