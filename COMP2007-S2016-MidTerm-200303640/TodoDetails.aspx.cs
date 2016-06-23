using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using statements required for EF to DB access
using COMP2007_S2016_MidTerm_200303640.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm_200303640
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodos();
            }

        }

        protected void GetTodos()
        {
            //populated the form existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            //connect to the EF DB
            using(TodoConnection db = new TodoConnection())
            {
                //populate a todo object instance with the TodoID from the url Parameter
                Todo updatedTodo = (from todo in db.Todos
                                                where todo.TodoID == TodoID
                                    select todo).FirstOrDefault();
                //map the todo properties to the form controls
                if(updatedTodo != null)
                {
                    TodoName.Text = updatedTodo.TodoName;
                    TodoNotes.Text = updatedTodo.TodoNotes;
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //redirect back to todos page
            Response.Redirect("~/Todolist.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //Ef to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                //use the Tood models to create a new Todo object and 
                //save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) //our URL has a TodoID in it 
                {
                    //get the id from the url
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    //get the current todo from EF DB
                    newTodo = (from todo in db.Todos
                                     where todo.TodoID == TodoID
                                     select todo).FirstOrDefault();
                }

                //add data to the new todo record
                newTodo.TodoName = TodoName.Text;
                newTodo.TodoNotes = TodoNotes.Text;
                

                //use LINQ to ADO.NET to add/insert todos in to DB
                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }
                //save our changes also updates the DB
                db.SaveChanges();

                //redirect back to updated TodoList page
                Response.Redirect("~/TodoList.aspx");
            }
        }

    }
}