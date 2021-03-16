using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Net;

namespace SystemStatus
{
    public partial class add_system : System.Web.UI.Page
    {
        OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {  
            if (!IsPostBack)
            {
                addSystem.Text = "Add System";
                getSystemData();
                addSystem.Attributes.Add("style", "width:100%");
            }
        }

        public void getSystemData()
        {
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM SystemStatus", con);
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                DataTable data = new DataTable();
                adp.Fill(data);

                GridView1.DataSource = data;
                GridView1.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alertify.alert('OOPS!',`<b>Exception occured during fetching data by UID!..</b><br/><br/>" + ex.Message + "`);", true);
            }
        }

        protected void addSystem_click(object sender, EventArgs e)
        {
            if (addSystem.Text == "Add System")
            {
                try
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO SystemStatus(IpAddress, SystemName, CompanyUnit, Floor, SystemLocation) VALUES ('" + ipAddressTxtBox.Text + "','" + systemNameTxtBox.Text + "','" + companyUnitDDL.SelectedItem.Text + "','"+floorDDL.SelectedItem.Text+"','" + systemLocationTxtBox.Text + "')", con);
                    int row = cmd.ExecuteNonQuery();

                    if (row > 0)
                    {
                        setFieldsAsEmpty();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alertify.success('A new system has been added successfully!..');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alertify.success('Something went wrong!.. can't save system details!.. try again!...')", true);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alertify.alert('OOPS!',`<b>Exception occured during fetching data by UID!..</b><br/><br/>" + ex.Message + "`);", true);
                }

                addSystem.Attributes.Add("Style", "width:100%");
                getSystemData();
            }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------




            if (addSystem.Text == "Update System")
            {
                try
                {
                    string systemId = null;
                    if (ViewState["systemid"].ToString() != null)
                    {
                        systemId = ViewState["systemid"].ToString();
                    }

                    if (systemId != null)
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("Update SystemStatus set IpAddress='" + ipAddressTxtBox.Text + "', SystemName = '" + systemNameTxtBox.Text + "', CompanyUnit= '" + companyUnitDDL.SelectedItem.Text + "', Floor='"+floorDDL.SelectedItem.Text+"', SystemLocation = '" + systemLocationTxtBox.Text + "' where ID=" + systemId, con);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            addSystem.Text = "Add System";
                            setFieldsAsEmpty();
                            addSystem.Attributes.Add("style", "width:100%");
                            cancelBtn.Visible = false;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alertify.success('System details has been updated successfully!...')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alertify.error(`Something went wrong!.. can't update system details!.. try again!...`)", true);
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alertify.alert('OOPS!',`<b>Exception occured during fetching data by UID!..</b><br/><br/>" + ex.Message + "`);", true);
                }
            }

            getSystemData();
        }


        protected void onCancel_click(object sender, EventArgs e)
        {
            setFieldsAsEmpty();
            addSystem.Attributes.Add("style", "width:100%");
            cancelBtn.Visible = false;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string actionType = e.CommandName;
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = GridView1.Rows[rowIndex];
            Label systemid = (Label)row.FindControl("systemid");
            Label ipAddress = (Label)row.FindControl("ipaddress");
            Label systemname = (Label)row.FindControl("systemname");
            Label companyunit = (Label)row.FindControl("companyunit");
            Label floor = (Label)row.FindControl("floor");
            Label systemlocation = (Label)row.FindControl("systemlocation");
            

            if (actionType == "editrow")
            {
                ViewState["systemid"] = systemid.Text;
                ipAddressTxtBox.Text = ipAddress.Text;
                systemNameTxtBox.Text = systemname.Text;
                companyUnitDDL.SelectedValue = companyunit.Text.Split('-').GetValue(1).ToString();
                floorDDL.SelectedValue = (Convert.ToInt16(floor.Text)+1).ToString();
                systemLocationTxtBox.Text = systemlocation.Text;

                addSystem.Text = "Update System";
                cancelBtn.Visible=true;
                addSystem.Attributes.Remove("style");
            }

            if (actionType == "deleterow")
            {
                try
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Delete from SystemStatus where ID = " + systemid.Text, con);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alertify.success(`System has been removed!...`)", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alertify.success(`System can't be removed!... Try again!..`)", true);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alertify.alert('OOPS!',`<b>Exception occured during fetching data by UID!..</b><br/><br/>" + ex.Message + "`);", true);
                }

                getSystemData();                
            }
        }
        
        public void setFieldsAsEmpty()
        {
            ipAddressTxtBox.Text = null;
            systemNameTxtBox.Text = null;
            companyUnitDDL.SelectedValue = "0";
            floorDDL.SelectedValue = "0";
            systemLocationTxtBox.Text = null;
            cancelBtn.Visible = false;
        }

        protected void ipAddressTxtChanged(object sender, EventArgs e)
        {            
           systemNameTxtBox.Text = GetSystemName(ipAddressTxtBox.Text);
           cancelBtn.Visible = true;
           addSystem.Attributes.Remove("style");
        }

        private string GetSystemName(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);
                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alertify.error(`No System connected with this IP Address!...`);", true);
            }
            return machineName;
        }
    }
}