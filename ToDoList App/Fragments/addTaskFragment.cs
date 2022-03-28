
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList_App.dataModel;

namespace ToDoList_App.Fragments
{
    public class dataSender : EventArgs
    {
        public TaskData taskdata { get; set; }
    }
    public class addTaskFragment : DialogFragment
    {
        public event EventHandler<dataSender> sendData;
        public event EventHandler<dataSender> changeData;
        public TextInputLayout addData;
        public List<TaskData> myList;
        public Button addTaskButton;
        public TaskData newTask;
        public string task;
        public TextView fragmentTitle;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);         
        }
        public addTaskFragment(string taskIn)
        {
            this.task = taskIn;
        }
        public addTaskFragment( )
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view  = inflater.Inflate(Resource.Layout.add_fragment, container, false);
            addTaskButton = view.FindViewById<Button>(Resource.Id.saveButton);
            addData = view.FindViewById<TextInputLayout>(Resource.Id.taskAdditionTextInputLayout);
            fragmentTitle = view.FindViewById<TextView>(Resource.Id.fragmentTitle);

            if (Tag == "ChangeCurrentTask")
            {
                fragmentTitle.Text = "Change this task";
                addData.EditText.Text = task;
                addTaskButton.Click += AddTaskButton_Click1;
                
            }

            addTaskButton.Click += AddTaskButton_Click;
          
            return view;
        }

        private void AddTaskButton_Click1(object sender, EventArgs e)
        {
            newTask = new TaskData();
            newTask.Tasks = addData.EditText.Text;
            changeData?.Invoke(this, new dataSender { taskdata = newTask });
            addTaskButton.Click += AddTaskButton_Click1;
            this.Dismiss();
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            newTask = new TaskData();


            if (addData.EditText.Text!=null)
            newTask.Tasks = addData.EditText.Text;
            sendData?.Invoke(this, new dataSender { taskdata = newTask });

            this.Dismiss();
        }
    }
}