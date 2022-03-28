using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoList_App.dataModel
{
    [Table("Task_Status")]
    public class TaskData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("Tasks")]
        public string Tasks { get; set; }

        [Column("Checked_status")]
        public bool checkedStatus { get; set; }
    
    }
}