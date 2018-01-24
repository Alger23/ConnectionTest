﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ConnectionTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new TestModel {Ip = IpHelper.IPv4 };

            return View(model);
        }

        [HttpPost]
        public ActionResult Query(QueryModel query)
        {
            try
            {
                using (var conn = new SqlConnection(query.ConnectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = query.Sql;
                    cmd.CommandTimeout = 5;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    var ds = new DataSet();
                    adapter.Fill(ds, "table");

                    return PartialView("QueryResult", ds.Tables[0]);
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;

                return PartialView("QueryResult", new DataTable());
            }
        }
    }
}