using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoDigital.Models
{
    public class Cloud
    {
        net.azurewebsites.sgoweb.SQL _ws;

        public net.azurewebsites.sgoweb.SQL Ws
        {
            get
            {
                if (_ws == null)
                {
                    _ws = new net.azurewebsites.sgoweb.SQL();
                }
                return _ws;
            }
        }

        public Cloud()
        {

        }
    }
}