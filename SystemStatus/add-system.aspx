<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-system.aspx.cs" Inherits="SystemStatus.add_system" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container-fluid">

        <div>
            <h4 style="text-align:center; margin-top:25px;">
                Add New System
            </h4>
        </div>
        
        <div class="row">
            <div class="col-lg-3">
                <div class="form-group">
                    <asp:Label Text="IP Address" runat="server" ID="ipAddressLbl"/>
                    <asp:TextBox ID="ipAddressTxtBox" runat="server" class="form-control" OnTextChanged="ipAddressTxtChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="systemNameLbl" Text="System Name" runat="server" />
                    <asp:TextBox ID="systemNameTxtBox" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label ID="companyUnitLbl" Text="Company Unit" runat="server" />
                    
                    <asp:DropDownList ID="companyUnitDDL" runat="server" class="form-control">
                        <asp:ListItem Value="0" Text="--------Select Company Unit--------"></asp:ListItem>
                        <asp:ListItem Value="1" Text="AWS-1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="AWS-2"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:Label ID="floorLbl" Text="Floor" runat="server" />
                    <asp:DropDownList ID="floorDDL" runat="server" class="form-control">
                        <asp:ListItem Value="0" Text="--------------Select Floor--------------"></asp:ListItem>
                        <asp:ListItem Value="1" Text="0"></asp:ListItem>
                        <asp:ListItem Value="2" Text="1"></asp:ListItem>
                        <asp:ListItem Value="3" Text="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:Label ID="systemLocationLbl" Text="System Location" runat="server" />
                    <asp:TextBox ID="systemLocationTxtBox" runat="server" class="form-control"></asp:TextBox>
                </div>
                <br />
                <center>
                    <asp:Button ID="cancelBtn" runat="server" class="btn btn-primary" Text="Cancel" OnClick="onCancel_click" Visible="false"/>
                    <asp:Button ID="addSystem" runat="server" class="btn btn-success" OnClick="addSystem_click"/>
                </center>
                
            </div>
            <div class="col-lg-9">
                <div style="float:right;">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-sm text-center" AutoGenerateColumns="false" Width="100%" onrowcommand="GridView1_RowCommand" >
                        <Columns>
                            <asp:TemplateField HeaderText="SystemID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="systemid" Text='<% #Eval("ID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SNo.">
                                <ItemTemplate>
                                    <asp:Label Text='<% #Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IP Address">
                                <ItemTemplate>
                                    <asp:Label ID="ipaddress" Text='<% #Eval("IpAddress") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="System Name">
                                <ItemTemplate>
                                    <asp:Label ID="systemname" Text='<% #Eval("SystemName") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Company Unit">
                                <ItemTemplate>
                                    <asp:Label ID="companyunit" Text='<% #Eval("CompanyUnit") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Floor">
                                <ItemTemplate>
                                    <asp:Label ID="floor" Text='<% #Eval("Floor") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="System Location">
                                <ItemTemplate>
                                    <asp:Label ID="systemlocation" Text='<% #Eval("SystemLocation") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit System">
                                <ItemTemplate>
                                    <asp:LinkButton ID="editlinkbtn" runat="server" CommandName="editrow" CommandArgument='<% #Container.DataItemIndex %>'>Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete System">
                                <ItemTemplate>
                                    <asp:LinkButton ID="deletelinkbtn" runat="server" CommandName="deleterow" CommandArgument='<% #Container.DataItemIndex %>' OnClientClick="return confirm('Are you sure to remove the system?..')">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
