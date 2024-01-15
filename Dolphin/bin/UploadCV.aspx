<%@ page title="" language="C#" masterpagefile="~/MasterPage/DashBoard.master" autoeventwireup="true" inherits="UploadCV, App_Web_hhgunqug" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 

             <div class="products-bottom2">
       <div class="container">
         <div class="col-md-7">  
       <div style="margin-top:30px;">
           <asp:Label runat="server" id="lbluserprofileid" Visible="false"></asp:Label>
             </div>
               <div class="edit-row">
             <div class="pull-left">
            <i class="fa fa-file"></i>Upload Resume
             
             </div>
            
             <div class="clearfix"></div>
             </div>
                   <table class="table table-hover">
            <tr ><td class="active"><asp:RadioButton ID="rdbown" runat="server" GroupName="a" 
                    Text="Your Own Resume" AutoPostBack="True" Checked="True" /> &nbsp;&nbsp;&nbsp; 
                <asp:RadioButton ID="rdbfriend" runat="server" GroupName="a" 
                    Text="Friend Resume" AutoPostBack="True" /></td><td class="active"></td></tr>
             <tr runat="server" id="tr2"><td class="active" style="font-family:Verdana; font-size:10pt;">Select Friends List</td><td class="active"></td></tr>
            <tr runat="server" id="tr1"><td class="active"><asp:DropDownList runat="server" CssClass="row-padding" ID="ddlfriends" ></asp:DropDownList></td><td class="active"></td></tr>
             <tr runat="server" id="tr3"><td class="active" style="font-family:Verdana; font-size:10pt;">File in Input (File Extension only 
                 doc or docx)</td><td class="active"></td></tr>
             <tr runat="server" id="tr4"><td class="active"><asp:FileUpload runat="server" ID="FileUpload" /></td><td class="active"></td></tr>
              <tr runat="server" id="tr5"><td class="active">
                  <asp:Button ID="btnPost" 
                      Width="200px" runat="server" CssClass="btn btn-primary" Text="Upload" 
                      Height="40px" onclick="btnPost_Click1" /> &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblMess" ></asp:Label> </td><td class="active"></td></tr>
                                                                   
                                                                  
            </table>
            
            <div>
          <table class="table table-hover">
          <tr><td class="active">   
          <asp:GridView ID="grddetails" ShowHeader="False" CssClass="footable" runat="server" AutoGenerateColumns="False"
   >
    <Columns>
      <asp:BoundField DataField="Id" Visible="false" />        
        <asp:BoundField DataField="Name" HeaderText="Name" >        
        <ItemStyle ForeColor="Black" Font-Size="Medium"/>
        </asp:BoundField>
        <asp:TemplateField HeaderText="View Resume" Visible="False">
        <ItemTemplate>
      <asp:ImageButton ID="lnkview"  ImageUrl="~/images/view.png" OnClick="lnkview_click" CommandName='<%#Eval("UploadedDate")%>' CommandArgument='<%#Eval("ResumePath")%>' ToolTip="View Resume" runat="server"></asp:ImageButton>
        </ItemTemplate>          
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Remove" >
        <ItemTemplate>
        <asp:ImageButton ID="lnkremove"  ImageUrl="~/images/deletefile.png" OnClick="lnkremove_click" OnClientClick="return confirm('Are you sure you want to Remove ?' )  " CommandName='<%#Eval("userid")%>'  CommandArgument='<%#Eval("ResumePath")%>' ToolTip="Remove Resume" runat="server"></asp:ImageButton>
        </ItemTemplate>           
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Farword">
        <ItemTemplate>
          <asp:ImageButton ID="lnkfarword"  ImageUrl="~/images/download1.png" OnClick="lnkfarword_click" CommandName='<%#Eval("userid")%>' CommandArgument='<%#Eval("ResumePath")%>' ToolTip="DownLoad Resume" runat="server"></asp:ImageButton>
        </ItemTemplate>         
        </asp:TemplateField>
        
    </Columns>
</asp:GridView>
 <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=grddetails]').footable();

        });
    </script></td></tr>
         </table>
            </div>

           <div class="panel panel-success" runat="server" id="pnl" visible="false">
    <div class="panel-heading" style="font-size:14pt; font-family:Verdana;"> 
     <div style="margin-left:580px;"> <asp:ImageButton runat="server" ImageUrl="~/images/closepnl.png" OnClick="imgclose_click" id="imgclose"/></div>
     <samp> <asp:Label runat="server" ID="lblheading" Font-Bold="true"></asp:Label></samp>
    
     </div>

    <div class="panel-body" style="text-align:justify; font-size:12pt;font-family:Verdana;">
  
     <asp:TextBox ID="lblresumecontent" runat="server"  Height="722px" width="600px"  TextMode="MultiLine" Enabled="False"></asp:TextBox>
     </div>
    
     
     </div>
  
</div> 
          
              
       <div class="row">
</div>
</div>
</div>
          
            </ContentTemplate>   
               <Triggers>
                <asp:PostBackTrigger ControlID="btnPost" />          
            </Triggers>      
        </asp:UpdatePanel>
           
                          
    </div>
    
    
</asp:Content>