﻿using PlanMy.Library;
using PlanMy.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlanMy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class newtask : ContentPage
	{
		public IEnumerable<WordPressPCL.Models.ItemCategory> cats;
		List<string> catnames = new List<string>();
		List<int> catids = new List<int>();
		public newtask (bool isedit,todoobj task)
		{
			InitializeComponent ();
			if (isedit == true && task!=null)
			{
				priorityPicker.IsVisible = true;
				prioritylabel.IsVisible = true;
				Pagetitle.Text = "EDIT TASK";
				titleoftask.Text = task.todo_title;
				detailstask.Text = task.todo_details;
				
			
					}

			NavigationPage.SetHasNavigationBar(this, false);
			Loadcats();
		
			


			backarrow.Clicked += async (object sender, EventArgs e) =>
			{
				Navigation.PopModalAsync();
			};

				Savechanges.Clicked += async (object sender, EventArgs e) =>
			{
				if (isedit == true && task != null)
				{
					edittask(task);	


				}
				else
				{
					addtask();
				}


			};

		}



		public async void Loadcats()
		{

			WordpressService service = new WordpressService();
			//var vendors = service.GetItemCategoriesAsync();
			cats = await service.GetItemCategoriesAsync();
			//VendorsListView.ItemsSource = vendors;


			catPicker.ItemsSource = cats.ToList();

		}

		//function to add task//
		public async void addtask()
		{
			WordPressPCL.Models.ItemCategory catt = (WordPressPCL.Models.ItemCategory)catPicker.SelectedItem;
			using (var cl = new HttpClient())
			{
				var formcontent = new FormUrlEncodedContent(new[]
				{
			new KeyValuePair<string,string>("todo_user","169"),
			new KeyValuePair<string, string>("todo_title",titleoftask.Text),
				new KeyValuePair<string,string>("todo_details",detailstask.Text),
			new KeyValuePair<string, string>("todo_date",Datepickertask.Date.ToString("yyyy-MM-dd")),
			new KeyValuePair<string,string>("todo_read","0"),
			new KeyValuePair<string, string>("todo_category",catt.Id.ToString()),
			new KeyValuePair<string, string>("is_priority","no")
		});

				var request = await cl.PostAsync("https://www.planmy.me/maizonpub-api/todolist.php?action=insert", formcontent);
				request.EnsureSuccessStatusCode();
				var response = await request.Content.ReadAsStringAsync();
				Navigation.PushModalAsync(new Planning());

			}
		}

		//function to edit task//
		public async void edittask(todoobj task)
		{
			WordPressPCL.Models.ItemCategory catt = (WordPressPCL.Models.ItemCategory)catPicker.SelectedItem;
			using (var cl = new HttpClient())
			{
				var formcontent = new FormUrlEncodedContent(new[]
				{
			new KeyValuePair<string,string>("todo_id",task.todo_id),
			new KeyValuePair<string, string>("todo_title",task.todo_title),
				new KeyValuePair<string,string>("todo_details",task.todo_details),
			new KeyValuePair<string, string>("todo_date",task.todo_date),
			new KeyValuePair<string,string>("todo_read","0"),
			new KeyValuePair<string, string>("todo_category",catt.Id.ToString()),
			new KeyValuePair<string, string>("is_priority",priorityPicker.SelectedItem.ToString())
		});


				var request = await cl.PostAsync("https://www.planmy.me/maizonpub-api/todolist.php?action=update", formcontent);

				request.EnsureSuccessStatusCode();

				var response = await request.Content.ReadAsStringAsync();

				Navigation.PushModalAsync(new Planning());

			}
		}
	}
}