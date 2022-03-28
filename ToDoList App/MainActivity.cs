using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.FloatingActionButton;
using System;
using System.Collections.Generic;
using ToDoList_App.Adapters;
using ToDoList_App.dataModel;
using ToDoList_App.Fragments;

namespace ToDoList_App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public database db;
        public TaskData tobeAdded;
        public RecyclerView tasksRecyclerView;
        public taskAdapters taskadapter;
        public List<TaskData> listOfTasks;
        public FloatingActionButton fab;
        public addTaskFragment addTassk;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
           
            db = new database(this);
            db.createtable();
            connectViews();
            fab = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton);
            fab.Click += Fab_Click;
           
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            addTassk = new addTaskFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            addTassk.Show(trans, "AddNewTask");
            addTassk.sendData += AddTassk_sendData;
        }

        private void AddTassk_sendData(object sender, dataSender e)
        {
             listOfTasks.Add(e.taskdata);
           // listOfTasks.Insert(0, e.taskdata);   
            taskadapter.NotifyItemInserted(listOfTasks.Count);
            tobeAdded = e.taskdata;
            db.insert(tobeAdded);
        }

        private void connectViews()
        {
            tasksRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            // recyclerView.addItemDecoration(new DividerItemDecoration(recyclerView.getContext(), DividerItemDecoration.VERTICAL));

            tasksRecyclerView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            tasksRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            addData();

          var  sqlFetchedList = db.alldata();
            if (db.alldata() == null)
            {
                return;
            }
            else
            {
                 sqlFetchedList = db.alldata();
            }
            listOfTasks.AddRange(sqlFetchedList);
            taskadapter = new taskAdapters(listOfTasks, this);    
           
            tasksRecyclerView.SetAdapter(taskadapter );
            taskadapter.deleteItemClick += Taskadapter_deleteItemClick;
            taskadapter.checkBox += Taskadapter_checkBox;
            taskadapter.ItemLongClick += Taskadapter_ItemLongClick;
        }

        private void Taskadapter_ItemLongClick(object sender, taskAdaptersClickEventArgs e)
        {
            int index = e.Position;
            addTassk = new addTaskFragment(listOfTasks[e.Position].Tasks);
            var trans = SupportFragmentManager.BeginTransaction();
            addTassk.Show(trans, "ChangeCurrentTask");
            addTassk.changeData += (s,f) =>
            {
                listOfTasks.RemoveAt(index);
                taskadapter.NotifyItemRemoved(index);
                listOfTasks.Insert(index, f.taskdata);
                taskadapter.NotifyItemInserted(index);

                var boolean = db.updateData(f.taskdata);
               // tobeAdded = f.taskdata;
                //db.insert(tobeAdded);


            };                     
        }

     

        private void Taskadapter_checkBox(object sender, taskAdaptersClickEventArgs e)
        {       
           // Toast.MakeText(this,"position is "+e.Position.ToString(), ToastLength.Short).Show();
            //e.holder.recyclerLayout.SetBackgroundDrawable(this.GetDrawable(Resource.Drawable.cardViewBackground));
        }

        private void Taskadapter_deleteItemClick(object sender, taskAdaptersClickEventArgs e)
        {
            string task = listOfTasks[e.Position].Tasks;
            AndroidX.AppCompat.App.AlertDialog.Builder deleteAlert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            deleteAlert.SetTitle("Delete Alert");
            deleteAlert.SetMessage("Do you want to delete task " + task);
            deleteAlert.SetPositiveButton("delete", (alert, args) =>
            {
                listOfTasks.RemoveAt(e.Position);
                taskadapter.NotifyItemRemoved(e.Position);
            });
            deleteAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                deleteAlert.Dispose();
            });
            deleteAlert.Show();
        }

        private void addData()
        {
            listOfTasks = new List<TaskData>();
            //{
            //    //new TaskData{ Tasks = "Physics HomeWork"},
            //    //new TaskData{ Tasks = "Maths HomeWork"},
            //    //new TaskData{ Tasks = "C# HomeWork"},
            //    //new TaskData{ Tasks = "Visual Studio HomeWork"},
            //    //new TaskData{ Tasks = "Fluter HomeWork"}
               
            //};
           
        }
    }
}