using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//connect to EF DB
using COMP2007_S2016_MidTerm_200303640.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016_MidTerm_200303640
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading the page for the first time populate the todos grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID"; //default sort column
                Session["SortDirection"] = "ASC";
                //get the todos data
                this.GetTodos();
            }
        }

        /***
         * <summary>
         * this method gets the todos data 
         * </summary>
         * 
         * @method GetTodos
         * @return {void}
         * **/

        protected void GetTodos()
        {
            //connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                //query the todos table usinf EF and LINQ
                var Todos = (from allTodos in db.Todos
                                   select allTodos);

                //bind the result in GridView
                TodosGridView.DataSource = Todos.AsQueryable().OrderBy(SortString).ToList();
                TodosGridView.DataBind();


            }
        }
        /**
        protected bool GetCheckedValue(object item)
        {
            bool Completed = false;
            if (item == null)
            {
                return false;
            }

            if (!bool.TryParse(item.ToString(),out Completed))
            {
                return false;
            }
            else
            {
                return Completed;
            }
        }
            **/

        /// <summary>
        /// this event handler deletes the todos form the DB using EF
        /// </summary>
        /// @method:TodosGridView_RowDeleting
        /// @param {object} sender
        /// @param {GridViewDeleteEventArgs} e
        /// @returns {void}


        protected void TodosGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row was clicked
            int selectedRow = e.RowIndex;

            //get the selected TodoID using grids data key collection
            int TodoID = Convert.ToInt32(TodosGridView.DataKeys[selectedRow].Values["TodoID"]);

            //use EF to find selected todo in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                //create object of todo class and store the query string inside of it
                Todo deletedTodo = (from todoRecords in db.Todos
                                                where todoRecords.TodoID == TodoID
                                                select todoRecords).FirstOrDefault();

                //remove the selected todo from DB
                db.Todos.Remove(deletedTodo);

                //save my changes to back to DB
                db.SaveChanges();

                //refresh the grid
                this.GetTodos();
            }
        }
        /**
        * <summary>
        * this event handler allows pagination to occure for the toods page
        * </summary>
        * 
        * @method TodosGridView_PageIndexChanging
        * @param {object} sender
        * @param {GridViewPageEventHandlerArgs} e
        * @returns {void}
        * */
        protected void TodosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the new page number
            TodosGridView.PageIndex = e.NewPageIndex;

            //return the grid
            this.GetTodos();
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new page size
            TodosGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //refresh the grid
            this.GetTodos();
        }

        /**
         * <summary>
         * this event handler allows sorting to occure for the todos page
         * </summary>
         * 
         * @method TodosGridView_Sorting
         * @param {object} sender
         * @param {GridViewSortEventHandlerArgs} e
         * @returns {void}
         * */

        protected void TodosGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            //refresh the grid
            this.GetTodos();

            //toggle the diretion 
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        /**
         * <summary>
         * this event handler allows sorting on each row with indication of whether the row is sorted in ASC or DESC order
         *  for the todos page
         * </summary>
         * 
         * @method TodosGridView_RowDataBound
         * @param {object} sender
         * @param {GridViewRowDataBoundEventHandlerArgs} e
         * @returns {void}
         * */

        protected void TodosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) //if header row has been clicked 
                {
                    LinkButton linkButton = new LinkButton();

                    for (int index = 0; index < TodosGridView.Columns.Count - 1; index++)
                    {
                        if (TodosGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkButton.Text = "<i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkButton.Text = "<i class='fa fa-caret-down fa-lg'></i>";
                            }
                            e.Row.Cells[index].Controls.Add(linkButton);
                        }
                    }
                }
            }
        }
    }
}