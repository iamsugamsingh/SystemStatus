using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Data;
using System.Text;
using Aws;
using System.Data.OleDb;
using System.Configuration;
using System.Drawing;
using System.Diagnostics;

namespace SystemStatus
{
    public partial class _Default : System.Web.UI.Page
    {
        int amt_ping;
        DataTable dataTable = new DataTable();
        List<string> ipAddressLists = new List<string>();
        OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        static int pingcount=1;

        protected void Page_Load(object sender, EventArgs e)
        {
            amt_ping = 10;           

            dataTable.Columns.Add("IP Address");
            dataTable.Columns.Add("System Name");
            dataTable.Columns.Add("Company Unit");
            dataTable.Columns.Add("Floor");
            dataTable.Columns.Add("System Location");
            dataTable.Columns.Add("Packet Loss");
            dataTable.Columns.Add("Connectivity Status");

            ping_hosts();
        }

        private void ping_hosts()
        {
            try
            {
                string host=null;
                string connectivityStatus = null;

                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from SystemStatus Order By CompanyUnit, IpAddress", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        host = dr["IpAddress"].ToString();
                        double loss = get_loss(host, amt_ping);

                        if (loss == 100.0)
                        {
                            if (pingcount > 1)
                            {
                                connectivityStatus = "Disconnected";
                                dataTable.Rows.Add(dr["IpAddress"].ToString(), dr["SystemName"].ToString(), dr["CompanyUnit"].ToString(), dr["Floor"].ToString(), dr["SystemLocation"].ToString(), Convert.ToString(loss) + "%", connectivityStatus);

                                if (dr["CompanyUnit"].ToString() == "AWS-1")
                                {
                                    SendEmail mail = new SendEmail();
                                    mail.sendMail("smtp.gmail.com", "it2@anugroup.net", "awsit2020", "it2@anugroup.net", "System Status", "Dear sir, <br/><br/>Work Entry system has disconnected from network whose <b>AWS-1 IP Address is: </b>" + host);
                                }

                                if (dr["CompanyUnit"].ToString() == "AWS-2")
                                {
                                    SendEmail mail = new SendEmail();
                                    mail.sendMail("smtp.gmail.com", "it2@anugroup.net", "awsit2020", "it2@anugroup.net", "System Status", "Dear sir, <br/><br/>Work Entry system has disconnected from network whose <b>IP Address is: </b>" + host);
                                }
                            }
                            else
                            {
                                connectivityStatus = "Not Sure";
                                dataTable.Rows.Add(dr["IpAddress"].ToString(), dr["SystemName"].ToString(), dr["CompanyUnit"].ToString(), dr["Floor"].ToString(), dr["SystemLocation"].ToString(), Convert.ToString(loss) + "%", connectivityStatus);
                            }

                            pingcount++;
                        }
                        else
                        {
                            connectivityStatus = "Connected";
                            dataTable.Rows.Add(dr["IpAddress"].ToString(), dr["SystemName"].ToString(), dr["CompanyUnit"].ToString(), dr["Floor"].ToString(), dr["SystemLocation"].ToString(), Convert.ToString(loss) + "%", connectivityStatus);
                        }
                    }
                }
                con.Close();
            }
            catch(Exception ex)
            {
                
            }

            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void onRowBound(object sender, GridViewRowEventArgs e)
        { 
            foreach(GridViewRow row in GridView1.Rows)
            {
                if (row.Cells[6].Text.Contains("Connected"))
                {
                    row.Cells[6].BackColor = Color.GreenYellow;
                }
                else if (row.Cells[6].Text.Contains("Disconnected"))
                {
                    row.Cells[6].BackColor = Color.Tomato;
                    row.Cells[6].Attributes.Add("Style", "font-weight:bold;");
                }
                else
                {
                    row.Cells[6].BackColor = Color.Yellow;
                    row.Cells[6].Attributes.Add("Style", "text-decoration:italic;");
                }
            }
        }

        private double get_loss(String host, int pingAmount)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            int failed = 0;
            for (int i = 0; i < pingAmount; i++)
            {
                PingReply reply = pingSender.Send(host, timeout, buffer, options);
                if (reply.Status != IPStatus.Success)
                {
                    failed += 1;
                }
            }
            double percent = (failed / pingAmount) * 100;
            return percent;
        }
    }
}