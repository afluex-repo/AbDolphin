<%@ page title="" language="C#" masterpagefile="~/MasterPage/DashBoard.master" autoeventwireup="true" inherits="ViewLinkedFrendsList, App_Web_hhgunqug" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div >
 <script type="text/javascript">



     function openPopup1(strOpen) {
         open(strOpen, "Info",
         'status=1, width=380, height=400, top=100, left=250,toolbar=no resizable=no,menubar=no, scrollbars=no');
     }

  </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <table class="table table-hover">
          <tr><td class="active"> 
                                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                         
                                         Width="100%" CssClass="footable" HeaderStyle-ForeColor="Black" 
                                     HeaderStyle-BackColor="#CCFFCC" HeaderStyle-Height="40px" 
                                            AllowPaging="True" EmptyDataText="No Data Found" 
                                            onrowdatabound="GridView1_RowDataBound" 
                                            onrowcommand="GridView1_RowCommand" 
                                            onrowediting="GridView1_RowEditing">
                                            <Columns>
                                             <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="sno" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="name" HeaderText="Linked Friends Name">
                                               
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="fname" HeaderText="Father Name">
                                             
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Skill" HeaderText=" Skills Name">
                                              
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/edit.png" CommandName="edit" CommandArgument='<%# Eval("UserId") %>' runat="server" />
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Create Seprate Login">
                                                <ItemTemplate>
                                                   
                                                     <asp:ImageButton ID="imglogin" ImageUrl="~/images/unlocked-32.png"  ToolTip='<%# Eval("loginstatus") %>' OnClientClick="return confirm('Are you sure you want to give login credentials ?' )  "  CommandName="Login1" CommandArgument='<%# Eval("UserId") %>' runat="server" />
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/Delete.png"  OnClientClick="return confirm('Are you sure you want to Delete ?' )  "  CommandName="Login" CommandArgument='<%# Eval("UserId") %>' runat="server" />
                                                </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Justify" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Large" ForeColor="#FF3300" />
                                         
                                            <HeaderStyle BackColor="#CCFFCC" ForeColor="Black" Height="40px" />
                                         
                                        </asp:GridView>
                                          <link href="css/footable.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=GridView1]').footable();

        });
    </script>   
                                        </td>
                    </tr>                
                  
                </table>
            </ContentTemplate>          
            </asp:UpdatePanel>
            </div>         
        
                        
                        
</asp:Content>

