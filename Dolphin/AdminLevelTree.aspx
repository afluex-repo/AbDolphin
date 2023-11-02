<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLevelTree.aspx.cs" Inherits="Dolphin.AdminLevelTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="hdf">
    <!-- Required meta tags -->

    <title>Dolphin</title>
    <link rel="stylesheet" type="text/css" href="../../files/bower_components/bootstrap/css/bootstrap.min.css" />
        <script type="text/javascript" src="../../files/bower_components/bootstrap/js/bootstrap.min.js"></script>

    <style>
        #trvBroker tr td .trvBroker_1{
               border: 1px solid #000000!important;
    padding: 0px 17px!important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
            <div class="page-content">
                <div class="items mb-3">
                    <div class="card">
                        <div class="card-body row">
                            <div class="col-md-3 mb-3">
                                <div class="form-group">
                                    <label class="float-label d-block mb-2"> Login Id </label>
                                    <asp:TextBox ID="txtloginid" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                            </div>
                            <div class="col-sm-3 mb-3  mb-3 ">
                                <label class="float-label d-block  mb-2">&nbsp;</label>
                                <asp:Button ID="btnsearch" runat="server" Text="Get Details" CssClass="btn btn-success" OnClick="btnsearch_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        <div id="BrokerTree">
            <asp:TreeView ID="trvBroker" runat="server" ExpandDepth="1" ImageSet="Simple" ShowLines="True">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <NodeStyle CssClass="gridViewToolTip" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>


        </div>
    </form>


</body>
</html>
