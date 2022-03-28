using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ToDoList_App.dataModel;
using Environment = System.Environment;

namespace ToDoList_App
{
    public class database
    {
        Context _context;
        public static string DBname = "SQLite.db4";
        public static string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DBname);

        SQLiteConnection sqliteConnection;
        public database(Context context)
        {
            _context = context;
            sqliteConnection = new SQLiteConnection(DBPath);
            Console.WriteLine("Succefully Database Created!....");
        }
        public void createtable()
        {
            try
            {
                var created = sqliteConnection.CreateTable<TaskData>();
                Console.WriteLine("Table created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception causede" + ex.Message);
            }
        }
        public bool insert(TaskData student)
        {
            long res = sqliteConnection.Insert(student);
            if (res == -1)
            {
                Toast.MakeText(_context, res.ToString(), ToastLength.Short).Show();
                return false;
            }
            else
            {
                Toast.MakeText(_context, "Data inserted", ToastLength.Short).Show();
                return true;
            }
        }
        public bool updateData(TaskData student)
        {
            long result = sqliteConnection.Update(student);

            if(result==1)
            {
                Toast.MakeText(_context, "Updated success", ToastLength.Short).Show();
                return true;
            }
            else
            {
                Toast.MakeText(_context, "Updated faileed", ToastLength.Short).Show();
                return false;
            }
        }
        public List<TaskData> alldata()
        {
            try
            {
                var list = sqliteConnection.Table<TaskData>().ToList();
                return list;
            }
            catch (Exception ex)
            {
                Toast.MakeText(_context, ex.Message, ToastLength.Short).Show();
                return null;
            }
        }

        public TaskData GetByRollno(int roll)
        {
            var userId = sqliteConnection.Table<TaskData>().Where(u => u.Id == roll).FirstOrDefault();

            return userId;

        }

    }
}