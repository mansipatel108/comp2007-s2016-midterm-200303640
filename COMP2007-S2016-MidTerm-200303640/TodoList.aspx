<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200303640.TodoList" %>

<%--
File Name: TodoList.aspx
Author Name: Mansi Patel(200303640) 
Website Name: http://comp2007-s2016-midterm-200303640.azurewebsites.net/Default.aspx
Description: This Page will Display the Todo details in gridview.
 @date: June 23, 2016
 @version: 0.0.1  --%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8"></div>
            <h1>Todos List</h1>
            <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Todos</a>

            <div>
                <label for="PageSizeDropDownList">Records per Page: </label>
                <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                    AutoPostBack="true" CssClass="btn btn-default btn-sm dropdown-toggle" OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="All" Value="1000" />
                </asp:DropDownList>
            </div>

            <asp:GridView runat="server" CssClass="table table-bordered table-stripped table-hover" ID="TodosGridView" AutoGenerateColumns="false"
                DataKeyNames="TodoID" OnRowDeleting="TodosGridView_RowDeleting" AllowPaging="true" PageSize="3"
                OnPageIndexChanging="TodosGridView_PageIndexChanging"
                AllowSorting="true" OnSorting="TodosGridView_Sorting" OnRowDataBound="TodosGridView_RowDataBound"
                PagerStyle-CssClass="pagination-ys">
                <Columns>
                    <asp:BoundField DataField="TodoID" HeaderText="Todo ID" Visible="true" SortExpression="TodoID" />
                    <asp:BoundField DataField="TodoName" HeaderText="Todo Name" Visible="true" SortExpression="TodoName" />
                    <asp:BoundField DataField="TodoNotes" HeaderText="Todo Notes" Visible="true"
                        SortExpression="TodoNotes" />
                    
                     <asp:TemplateField HeaderText="Completed" Visible="true" SortExpression="Completed">
                <ItemTemplate>
                    <asp:CheckBox ID="Completed" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

                    <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit"
                        NavigateUrl="~/TodoDetails.aspx" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                        DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}" />
                    <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true" ButtonType="Link"
                        ControlStyle-CssClass="btn btn-danger btn-sm" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
