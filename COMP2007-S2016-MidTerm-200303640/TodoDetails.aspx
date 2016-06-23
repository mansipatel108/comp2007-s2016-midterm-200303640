<%@ Page Title="Todo Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoDetails.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200303640.TodoDetails" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
<%--
File Name: TodoDetails.aspx
Author Name: Mansi Patel(200303640) 
Website Name: http://comp2007-s2016-midterm-200303640.azurewebsites.net/Default.aspx
Description: When user want to modify details for the Todos This page will allow them to change the data .
 @date: June 23, 2016
 @version: 0.0.1  --%>    
      
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Toodo Details</h1>
                   <br />
                    <div class="form-group">
                        <label class="control-label" for="TodoName">Todo Name</label>
                        <asp:TextBox  runat="server" Cssclass="form-control" ID="TodoName" placeholder="Todo Name" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="TodoNotes">Todo Notes</label>
                        <asp:TextBox  runat="server" Cssclass="form-control" ID="TodoNotes" placeholder="Todo Notes" required="true"></asp:TextBox>
                    </div>
                <div class="form-group">
                   <asp:CheckBox runat="server" CssClass="checkbox" ID="Compeleted" Text="Completed"/>    
                      </div>
                    <div class="text-right">
                        <asp:Button Text="Cancel" ID="CancelButton" CssClass="btn btn-warning btn-lg" runat="server" UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                        <asp:Button Text="Save" ID="SaveButton" CssClass="btn btn-primary btn-lg" runat="server" OnClick="SaveButton_Click"  />
                    </div>
            </div>
        </div>
    </div>  
</asp:Content>
