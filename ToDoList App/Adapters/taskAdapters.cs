
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using ToDoList_App.dataModel;

namespace ToDoList_App.Adapters
{

    public class taskAdapters : RecyclerView.Adapter
    {
        public event EventHandler<taskAdaptersClickEventArgs> ItemClick;
        public event EventHandler<taskAdaptersClickEventArgs> ItemLongClick;
        public event EventHandler<taskAdaptersClickEventArgs> deleteItemClick;
        public event EventHandler<taskAdaptersClickEventArgs> checkBox;
        //public taskAdaptersViewHolder holder;
        Context context;
        List<TaskData> items;
        public taskAdapters(List<TaskData> listOfTasksIn, Context context)
        {
            this.context = context;
            items = listOfTasksIn;
        }
        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.task_row, parent, false);


            var vh = new taskAdaptersViewHolder(itemView, OnClick, OnLongClick, onDeleteItemClick, checkBoxChecked);
            return vh;
        }
        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as taskAdaptersViewHolder;

            //holder.TextView.Text = items[position];
            holder.tasks.Text = item.Tasks;
            holder.tasksCheckbox.Click += (s, e) =>
            {
                if (!holder.tasksCheckbox.Checked)
                {

                    holder.recyclerLayout.SetBackgroundDrawable(null);
                }
                else
                {
                    holder.recyclerLayout.SetBackgroundDrawable(context.GetDrawable(Resource.Drawable.cardViewBackground));

                }

            };
        }



        public void checkedBackgroundChanger(taskAdaptersViewHolder holder)
        {
            holder.recyclerLayout.SetBackgroundDrawable(context.GetDrawable(Resource.Drawable.cardViewBackground));
        }

        public override int ItemCount => items.Count;

        void OnClick(taskAdaptersClickEventArgs args) => ItemClick?.Invoke(this, args);
        void checkBoxChecked(taskAdaptersClickEventArgs args) => checkBox?.Invoke(this, args);
        void onDeleteItemClick(taskAdaptersClickEventArgs args)
        {
            deleteItemClick?.Invoke(this, args);
        }
        void OnLongClick(taskAdaptersClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class taskAdaptersViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public CheckBox tasksCheckbox;
        public TextView tasks;
        public ImageView deleteIcon;
        public RelativeLayout recyclerLayout;


        public taskAdaptersViewHolder(View itemView, Action<taskAdaptersClickEventArgs> clickListener,

                            Action<taskAdaptersClickEventArgs> longClickListener, Action<taskAdaptersClickEventArgs> onDeleteItemClickListener,
                            Action<taskAdaptersClickEventArgs> checkBoxCheckedListener) : base(itemView)
        {
            //TextView = v;
            recyclerLayout = (RelativeLayout)itemView.FindViewById(Resource.Id.relativeLayoutt);
            tasks = (TextView)itemView.FindViewById(Resource.Id.taskTextView);
            deleteIcon = (ImageView)itemView.FindViewById(Resource.Id.deleteIcon);
            tasksCheckbox = (CheckBox)itemView.FindViewById(Resource.Id.taskDoneCheckBox);



            tasksCheckbox.Click += (sender, e) => checkBoxCheckedListener(new taskAdaptersClickEventArgs { View = itemView, Position = AdapterPosition });
            deleteIcon.Click += (sender, e) => onDeleteItemClickListener(new taskAdaptersClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.Click += (sender, e) => clickListener(new taskAdaptersClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new taskAdaptersClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
    public class taskAdaptersClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public taskAdaptersViewHolder holder { get; set; }
    }
}

